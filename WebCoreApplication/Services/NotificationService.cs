using Database.Data.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using WebCoreApplication.Models;

namespace WebCoreApplication.Services
{
    static public class NotificationService 
    {
        private struct NotificationData
        {
            public DateTime DateTime;
            public int Order;

            public NotificationData(DateTime dateTime, int order)
            {
                DateTime = dateTime;
                Order = order;
            }
        }

        static private List<SubjectCall> _subjectCalls;
        static private TimeSpan _beforehandTime;
        static private TimeSpan _updateTimer;
        static private TimeSpan _deltaTime;
        static private List<NotificationData> _startSubjectDateTimeNotifications;
        static private List<NotificationData> _endSubjectDateTimeNotifications;
        static private DateTime _startToday;

        static public void Initialize() => new Thread(Notificate).Start();

        static private void Notificate()
        {
            _startSubjectDateTimeNotifications = new List<NotificationData>();
            _endSubjectDateTimeNotifications = new List<NotificationData>();
            _beforehandTime = new TimeSpan(0, 5, 0);
            _updateTimer = new TimeSpan(0, 0, 40);
            _deltaTime = new TimeSpan(0, 1, 0);

            for (; ; )
            {
                if ((Data.CorrectedDateTime - _startToday).Days > 1)
                    UpdateDateTimeNotifications();

                for (int i = _startSubjectDateTimeNotifications.Count - 1; i >= 0; i--)
                {
                    if ((Data.CorrectedDateTime - _startSubjectDateTimeNotifications[i].DateTime) < _deltaTime)
                        continue;
                    Data.StartSubjectDateTimeNotificate(_startSubjectDateTimeNotifications[i].Order);
                    _startSubjectDateTimeNotifications.RemoveAt(i);
                }
                for (int i = _endSubjectDateTimeNotifications.Count - 1; i >= 0; i--)
                {
                    if ((Data.CorrectedDateTime - _endSubjectDateTimeNotifications[i].DateTime) < _deltaTime)
                        continue;
                    Data.EndSubjectDateTimeNotificate(_endSubjectDateTimeNotifications[i].Order);
                    _endSubjectDateTimeNotifications.RemoveAt(i);
                }
                Thread.Sleep(_updateTimer);
            }
        }
        static private void UpdateDateTimeNotifications()
        {
            _startToday = new DateTime(Data.CorrectedDateTime.Year, Data.CorrectedDateTime.Month, Data.CorrectedDateTime.Day);

            _startSubjectDateTimeNotifications.Clear();
            _endSubjectDateTimeNotifications.Clear();
            _subjectCalls = new List<SubjectCall>(Data.DataContext.SubjectCalls);

            foreach (SubjectCall call in _subjectCalls)
            {
                _startSubjectDateTimeNotifications.Add(new NotificationData(_startToday.Add(call.StartLesson - _beforehandTime), call.Order));
                _endSubjectDateTimeNotifications.Add(new NotificationData(_startToday.Add(call.EndLesson), call.Order));
            }
        }

    }
}
