using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WebCoreApplication.Models;

namespace WebCoreApplication.Services
{
    public static class RequestHandler
    {
        private static string GetGroupInfo(UserModel userModel)
        {
            string parity = userModel.Data.Group.Parity ? "числитель" : "знаменатель";
            DateTime startEducation = userModel.Data.Group.StartEducation;
            int weekCount = (DateTime.Now - startEducation).Days + ((int)startEducation.DayOfWeek - 1) / 7 + 1;
            string result =
            "*Информация о группе:*\n" +
            "`Название: " + userModel.Data.Group.Name + ".\n" +
            "Начало семестра: " + userModel.Data.Group.StartEducation.ToShortDateString() + ".\n" +
            "Номер для приглашения: " + userModel.Data.Group.ID + ".\n" +
            weekCount + " неделя " + parity + "`.";
            return result;
        }

        public static Responce Handle(Update update, Request request)
        {
            UserModel userModel = Data.GetUserModel(update.Message.From);

            switch (request)
            {
                case Request.Startup:
                    return userModel.Data.Group == null
                        ? new Responce(KeyboardService.GetKeyboardByRequest(request), "Стартовое меню.")
                        : new Responce(KeyboardService.GetKeyboardByRequest(Request.GroupMenu), GetGroupInfo(userModel));

                case Request.CreateGroup:
                {
                    userModel.LastRequest = Request.CreateGroup;
                    return new Responce(KeyboardService.GetKeyboardByRequest(request), "Введите название группы: .");
                }
                case Request.JoinGroup:
                {
                    userModel.LastRequest = Request.JoinGroup;
                    return new Responce(KeyboardService.GetKeyboardByRequest(request), "Введите номер группы: .");
                }
                case Request.GroupMenu:
                    return new Responce(KeyboardService.GetKeyboardByRequest(request), GetGroupInfo(userModel));
                case Request.LeaveGroup:
                {
                    Data.LeaveGroup(userModel);
                    return new Responce(KeyboardService.GetKeyboardByRequest(Request.Startup), "Стартовое меню.");
                }
                case Request.WatchFullSchedule:
                {
                    return Data.GetFullShedule(userModel, out string schedule)
                        ? new Responce(KeyboardService.GetKeyboardByRequest(Request.None), schedule)
                        : new Responce(KeyboardService.GetKeyboardByRequest(Request.None), "Расписание не достпуно.");
                }
                case Request.WatchOnTomorrow:
                {
                    return Data.GetSheduleOnTomorrow(userModel, out string schedule)
                        ? new Responce(KeyboardService.GetKeyboardByRequest(Request.None), schedule)
                        : new Responce(KeyboardService.GetKeyboardByRequest(Request.None), "Расписание не достпуно.");
                }
                default:
                    return new Responce(KeyboardService.GetKeyboardByRequest(Request.None), "Введите /start");
            }
        }
    }
    public struct Responce
    {
        public ReplyMarkupBase Keyboard;
        public string TextMessage;

        public Responce(ReplyMarkupBase keyboard, string textMessage)
        {
            Keyboard = keyboard ?? throw new ArgumentNullException(nameof(keyboard));
            TextMessage = textMessage ?? throw new ArgumentNullException(nameof(textMessage));
        }
    }
}
