using Database;
using Database.Data;
using Database.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication.Services;

namespace WebCoreApplication.Models
{
    public static class Data
    {
        private const int _timeDifferent = 3;
        static private DataContext _dataContext;
        static private Dictionary<int, UserModel> _userModels;
        static private Dictionary<UserModel, string> _creationGroup;
        static private List<SubjectCall> _subjectCalls;

        static public DateTime CorrectedDateTime => DateTime.Now.AddHours(_timeDifferent);
        static public DataContext DataContext => _dataContext;
        static public List<SubjectCall> SubjectCalls => _subjectCalls;

        static public void Initialize()
        {
            _dataContext = new DataContext();
            _userModels = new Dictionary<int, UserModel>();
            _creationGroup = new Dictionary<UserModel, string>();
            _subjectCalls = _dataContext.SubjectCalls.ToList();
            _subjectCalls.Sort();
        }

        static public void AddLog(string log) => _dataContext.AddLog(log);
        static public UserModel GetUserModel(Telegram.Bot.Types.Message message)
        {
            if (_userModels.TryGetValue(message.From.Id, out UserModel userModel))
                return userModel;

            if (!_dataContext.GetUserByTelegramId(message.From.Id, out User data))
                _dataContext.CreateUser(message.From.Id, message.Chat.Id, message.From.Username + " (" + message.From.FirstName + " " + message.From.LastName + ")", out data);
            UserModel result;
            _userModels.Add(message.From.Id, result = new UserModel(data, message.From));
            _dataContext.SaveChanges();
            return result;
        }
        static public void PartOnCreateGroup(UserModel userModel, string name)
        {
            _creationGroup.Remove(userModel);
            _creationGroup.Add(userModel, name);
        }
        static public void PartOnCreateGroup(UserModel userModel, DateTime startEducation)
        {
            if (_creationGroup.TryGetValue(userModel, out string name))
            {
                _dataContext.CreateGroup(name, startEducation, userModel.Data, out Database.Data.Model.Group group);
                userModel.Data.Group = group;
                _dataContext.SaveChanges();
                _creationGroup.Remove(userModel);
            }
        }
        static public bool TryToJoinGroup(UserModel userModel, int groupId)
        {
            Database.Data.Model.Group group = _dataContext.Groups.FirstOrDefault(x => x.ID == groupId);
            if (group == null)
                return false;
            userModel.Data.Group = group;
            _dataContext.SaveChanges();
            return true;
        }
        static public void LeaveGroup(UserModel userModel)
        {
            userModel.Data.Group.Users.Remove(userModel.Data);
            userModel.Data.Group = null;
            _dataContext.SaveChanges();
        }
        static public bool GetFullShedule(UserModel userModel, out string schedule) => ScheduleView.GetSchedule(userModel.Data.Group, out schedule);
        static public bool GetSheduleOnTomorrow(UserModel userModel, out string schedule) => ScheduleView.GetSchedule(userModel.Data.Group, CorrectedDateTime.AddDays(1), _subjectCalls, out schedule);
        static public bool GetSheduleOnToday(UserModel userModel, out string schedule) => ScheduleView.GetSchedule(userModel.Data.Group, CorrectedDateTime, _subjectCalls, out schedule);

        static public void StartSubjectDateTimeNotificate(int order)
        {
            foreach (Group group in _dataContext.Groups)
            {
                List<ScheduleViewItem> schedule = ScheduleView.GetSchedule(group, CorrectedDateTime.DayOfWeek, group.Parity(CorrectedDateTime));
                if (schedule.Select(x => x.Order).Min() == order)
                {
                    ScheduleViewItem subject = schedule.First(x => x.Order == order);
                    foreach (User user in group.Users)
                        UpdateService.BotService.Client.SendTextMessageAsync(user.ChatID, "`Первая пара " + subject.SubjectInstance.Subject.Name + " " + ScheduleView.GetSubjectTypeString(subject.SubjectInstance.SubjectType) + " в " + subject.SubjectInstance.Audience + " аудитории. Ведёт " + subject.SubjectInstance.Teacher + "`.", Telegram.Bot.Types.Enums.ParseMode.Markdown);
                }
            }
        }
        static public void EndSubjectDateTimeNotificate(int order)
        {
            foreach (Group group in _dataContext.Groups)
            {
                List<ScheduleViewItem> schedule = ScheduleView.GetSchedule(group, CorrectedDateTime.DayOfWeek, group.Parity(CorrectedDateTime));
                if (schedule.Select(x => x.Order).Contains(order))
                {
                    ScheduleViewItem subject = schedule.First(x => x.Order == order);
                    foreach (User user in group.Users)
                    {
                        ScheduleViewItem? nextSubject = schedule.FirstOrDefault(z => z.Order == order + 1);
                        if (nextSubject.HasValue)
                            UpdateService.BotService.Client.SendTextMessageAsync(user.ChatID, "`Пара " + subject.SubjectInstance.Subject.Name + " закончилась. Следующая пара " + nextSubject.Value.SubjectInstance.Subject.Name + " " + ScheduleView.GetSubjectTypeString(nextSubject.Value.SubjectInstance.SubjectType) + " в " + nextSubject.Value.SubjectInstance.Audience + " аудитории. Ведёт " + nextSubject.Value.SubjectInstance.Teacher + "`.", Telegram.Bot.Types.Enums.ParseMode.Markdown);
                        else
                            UpdateService.BotService.Client.SendTextMessageAsync(user.ChatID, "`Пара " + subject.SubjectInstance.Subject.Name + " закончилась. Теперь домой!`", Telegram.Bot.Types.Enums.ParseMode.Markdown);
                    }
                }
            }
        }
    }
}
