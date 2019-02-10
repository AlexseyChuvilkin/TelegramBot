using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WebCoreApplication.Models;
using WebCoreApplication.Services;

namespace WebApplication.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotService _botService;

        public UpdateService(IBotService botService) => _botService = botService;

        public async Task EchoAsync(Update update)
        {
            if (update.Type != UpdateType.Message)
                return;

            if (update.Message.Type == MessageType.Text)
                if (RequestService.GetRequestByMessage(update.Message.Text, out Request request))
                {
                    UserModel userModel = Data.GetUserModel(update.Message.From);
                    Responce responce = RequestHandler.Handle(update, request);
                    await _botService.Client.SendTextMessageAsync(update.Message.Chat.Id, responce.TextMessage, ParseMode.Markdown, replyMarkup: responce.Keyboard);
                }
                else
                {
                    UserModel userModel = Data.GetUserModel(update.Message.From);
                    switch (userModel.LastRequest)
                    {
                        case Request.None:
                            break;
                        case Request.Startup:
                            break;
                        case Request.CreateGroup:
                            Data.PartOnCreateGroup(userModel, update.Message.Text);
                            userModel.LastRequest = Request.InputGroupName;
                            await _botService.Client.SendTextMessageAsync(update.Message.Chat.Id, "Введите дату по форме 27.5.2019 .");
                            break;
                        case Request.InputGroupName:
                            if (DateTime.TryParse(update.Message.Text, out DateTime dateTime))
                            {
                                userModel.LastRequest = Request.None;
                                Data.PartOnCreateGroup(userModel, dateTime);
                                await _botService.Client.SendTextMessageAsync(update.Message.Chat.Id, "Поздравляем. Группа успешно создана.");
                                Responce responce = RequestHandler.Handle(update, Request.GroupMenu);
                                await _botService.Client.SendTextMessageAsync(update.Message.Chat.Id, responce.TextMessage, replyMarkup: responce.Keyboard);
                            }
                            else
                                await _botService.Client.SendTextMessageAsync(update.Message.Chat.Id, "Дату нормально введи.");
                            break;
                        case Request.JoinGroup:
                            int.TryParse(update.Message.Text, out int groupId);
                            if(Data.TryToJoinGroup(userModel, groupId))
                            {
                                userModel.LastRequest = Request.None;
                                await _botService.Client.SendTextMessageAsync(update.Message.Chat.Id, "Поздравляем. Вы успешно присоединились к группе.");
                                Responce responce = RequestHandler.Handle(update, Request.GroupMenu);
                                await _botService.Client.SendTextMessageAsync(update.Message.Chat.Id, responce.TextMessage, replyMarkup: responce.Keyboard);
                            }
                            else
                                await _botService.Client.SendTextMessageAsync(update.Message.Chat.Id, "Не получилось присоединиться к группе.");
                            break;
                        default:
                            break;
                    }
                }
        }
    }
}