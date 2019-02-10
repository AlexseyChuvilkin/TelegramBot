using System;
using System.Collections.Generic;
using System.Linq;

namespace WebCoreApplication.Services
{
    public static class RequestService
    {
        private struct RequestItem
        {
            public Request Request;
            public string Message;

            public RequestItem(Request request, string message)
            {
                Request = request;
                Message = message ?? throw new ArgumentNullException(nameof(message));
            }
        }

        static private Dictionary<string, RequestItem> _requests;

        static public void Initialize()
        {
            List<RequestItem> requestItems = new List<RequestItem>
            {
                new RequestItem(Request.Startup, "/start"),
                new RequestItem(Request.Backward, "Назад."),
                new RequestItem(Request.CreateGroup, "Создать расписание для группы."),
                new RequestItem(Request.JoinGroup, "Присоединиться к группе."),
                new RequestItem(Request.LeaveGroup, "Покинуть группу."),
                new RequestItem(Request.WatchFullSchedule, "Всё расписание."),
                new RequestItem(Request.WatchScheduleOnTomorrow, "Расписание на завтра."),
                new RequestItem(Request.WatchScheduleOnToday, "Расписание на сегодня."),
            };

            _requests = new Dictionary<string, RequestItem>();
            foreach (RequestItem requestItem in requestItems)
                _requests.Add(requestItem.Message, requestItem);
        }
        static public bool GetRequestByMessage(string message, out Request request)
        {
            request = Request.None;
            if (!_requests.TryGetValue(message, out RequestItem requestItem))
                return false;
            request = requestItem.Request;
            return true;
        }
        static public string GetMessageByRequest(Request request) => _requests.Values.First(x => x.Request == request).Message;
    }
}