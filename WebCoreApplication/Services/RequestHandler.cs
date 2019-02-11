using System;
using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using WebCoreApplication.Models;

namespace WebCoreApplication.Services
{
    public static class RequestHandler
    {
        private static string GetGroupInfo(UserModel userModel)
        {
            string parity = userModel.Data.Group.Parity(Data.CorrectedDateTime) ? "числитель" : "знаменатель";
            DateTime startEducation = userModel.Data.Group.StartEducation;
            int weekCount = ((Data.CorrectedDateTime - startEducation).Days + ((int)startEducation.DayOfWeek - 1)) / 7 + 1;
            string result =
            "*Информация о группе:*\n" +
            "`Название: " + userModel.Data.Group.Name + ".\n" +
            "Начало семестра: " + userModel.Data.Group.StartEducation.ToShortDateString() + ".\n" +
            "Номер для приглашения: " + userModel.Data.Group.ID + ".\n" +
            weekCount + " неделя " + parity + ".`";
            return result;
        }

        public static Responce Handle(Update update, Request request)
        {
            UserModel userModel = Data.GetUserModel(update.Message);
            Responce responce;
            switch (request)
            {
                case Request.Startup:
                {
                    responce = userModel.Data.Group == null
                        ?  new Responce(KeyboardService.GetKeyboardByRequest(request), "Стартовое меню.")
                        : new Responce(KeyboardService.GetKeyboardByRequest(Request.GroupMenu), GetGroupInfo(userModel));
                    break;
                }
                case Request.Backward:
                {
                    return Handle(update, userModel.LastRequest[userModel.LastRequest.Count - 2]);
                }
                case Request.CreateGroup:
                {
                    userModel.LastRequest.Add(Request.CreateGroup);
                    responce = new Responce(KeyboardService.GetKeyboardByRequest(request), "Введите название группы: .");
                    break;
                }
                case Request.JoinGroup:
                {
                    userModel.LastRequest.Add(Request.JoinGroup);
                    responce = new Responce(KeyboardService.GetKeyboardByRequest(request), "Введите номер группы: .");
                    break;
                }
                case Request.GroupMenu:
                    responce = new Responce(KeyboardService.GetKeyboardByRequest(request), GetGroupInfo(userModel));
                    break;
                case Request.LeaveGroup:
                {
                    Data.LeaveGroup(userModel);
                    responce = new Responce(KeyboardService.GetKeyboardByRequest(Request.Startup), "Стартовое меню.");
                    break;
                }
                case Request.WatchFullSchedule:
                {
                    responce = Data.GetFullShedule(userModel, out string schedule)
                        ? new Responce(KeyboardService.GetKeyboardByRequest(Request.Backward), schedule)
                        : new Responce(KeyboardService.GetKeyboardByRequest(Request.Backward), "Расписание не достпуно.");
                    break;
                }
                case Request.WatchScheduleOnTomorrow:
                {
                    responce = Data.GetSheduleOnTomorrow(userModel, out string schedule)
                        ? new Responce(KeyboardService.GetKeyboardByRequest(Request.Backward), schedule)
                        : new Responce(KeyboardService.GetKeyboardByRequest(Request.Backward), "Расписание не достпуно.");
                    break;
                }
                case Request.WatchScheduleOnToday:
                {
                    responce = Data.GetSheduleOnToday(userModel, out string schedule)
                        ? new Responce(KeyboardService.GetKeyboardByRequest(Request.Backward), schedule)
                        : new Responce(KeyboardService.GetKeyboardByRequest(Request.Backward), "Расписание не достпуно.");
                    break;
                }
                default:
                    responce = new Responce(KeyboardService.GetKeyboardByRequest(Request.None), "Введите /start");
                    break;
            }
            userModel.LastRequest.Add(request);
            return responce;
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
