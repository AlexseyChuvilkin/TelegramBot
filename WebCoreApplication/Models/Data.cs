using Database.Data;
using Database.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebCoreApplication.Models
{
    public static class Data
    {
        private const int _weekdayCount = 7;

        static private DataContext _dataContext;
        static private Dictionary<int, UserModel> _userModels;
        static private Dictionary<UserModel, string> _creationGroup;
        static private List<SubjectCall> _subjectCalls;

        static public void Initialize()
        {
            _dataContext = new DataContext();
            _userModels = new Dictionary<int, UserModel>();
            _creationGroup = new Dictionary<UserModel, string>();
            _subjectCalls = _dataContext.SubjectCalls.ToList() ;
            _subjectCalls.Sort();
        }

        static private string GetWeekDayString(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Friday:
                    return "Пятница";
                case DayOfWeek.Monday:
                    return "Понедельник";
                case DayOfWeek.Saturday:
                    return "Суббота";
                case DayOfWeek.Sunday:
                    return "Воскресенье";
                case DayOfWeek.Thursday:
                    return "Четверг";
                case DayOfWeek.Tuesday:
                    return "Вторник";
                case DayOfWeek.Wednesday:
                    return "Среда";
                default:
                    return string.Empty;
            }
        }
        static private string GetSubjectTypeString(SubjectType subjectType)
        {
            switch (subjectType)
            {
                case SubjectType.Lecture:
                    return "Лекция";
                case SubjectType.Laboratory:
                    return "Лабораторная";
                case SubjectType.Seminar:
                    return "Семинар";
                case SubjectType.Exercise:
                    return "Упражнение";
                case SubjectType.Coursework:
                    return "Курсовая";
                case SubjectType.ScientificResearch:
                    return "Научная работа";
                default:
                    return string.Empty;
            }
        }

        static public UserModel GetUserModel(Telegram.Bot.Types.User user)
        {
            if (_userModels.TryGetValue(user.Id, out UserModel userModel))
                return userModel;

            if (!_dataContext.GetUserByTelegramId(user.Id, out Database.Data.Model.User data))
                _dataContext.CreateUser(user.Id, out data);
            UserModel result;
            _userModels.Add(user.Id, result = new UserModel(data, user));
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
        static public bool GetFullShedule(UserModel userModel, out string shedule)
        {
            shedule = string.Empty;
            if (userModel.Data.Group == null)
                return false;
            if (userModel.Data.Group.ScheduleSubjects == null)
                return false;
            List<ScheduleField> scheduleFields = userModel.Data.Group.ScheduleSubjects.ToList();
            for (int day = 0; day < _weekdayCount; day++)
            {
                List<ScheduleField> dayShedule = scheduleFields.Where(x => x.DayOfWeek == (DayOfWeek)day).ToList();
                if (dayShedule.Count == 0)
                    continue;
                shedule += "*" + GetWeekDayString((DayOfWeek)day) + "*\n\n";
                dayShedule.Sort(new Comparison<ScheduleField>((x, y) => x.Order.CompareTo(y.Order)));
                int daySheduleCount = dayShedule.Count;
                for (int sheduleField = 0; sheduleField < daySheduleCount; sheduleField++)
                {
                    if (dayShedule[sheduleField] is ParityDependentScheduleSubject subject)
                    {
                        string subjectOrder = (subject.Order + 1).ToString();
                        shedule += @"`    " + 
                        subjectOrder + ". (Ч) " + subject.ParitySubjectInstance.Subject.Name + " " + subject.ParitySubjectInstance.Audience + " " + subject.ParitySubjectInstance.Teacher + " "
                        + GetSubjectTypeString(subject.ParitySubjectInstance.SubjectType) + "\n" + 
                        subjectOrder + ". (З) " + subject.NotParitySubjectInstance.Subject.Name + " " + subject.NotParitySubjectInstance.Audience + " " + subject.NotParitySubjectInstance.Teacher + " "
                        + GetSubjectTypeString(subject.NotParitySubjectInstance.SubjectType) + "`\n\n";
                    }
                    else
                        shedule += @"`    " + (dayShedule[sheduleField].Order + 1).ToString() + ". " + dayShedule[sheduleField].SubjectInstance.Subject.Name + " " + dayShedule[sheduleField].SubjectInstance.Audience + " " + dayShedule[sheduleField].SubjectInstance.Teacher + " "
                        + GetSubjectTypeString(dayShedule[sheduleField].SubjectInstance.SubjectType) + "`\n\n";
                }
            }
            return true;
        }
        static public bool GetSheduleOnTomorrow(UserModel userModel, out string shedule)
        {
            shedule = string.Empty;
            if (userModel.Data.Group == null)
                return false;
            if (userModel.Data.Group.ScheduleSubjects == null)
                return false;
            shedule += "*" + GetWeekDayString(DateTime.Now.AddDays(1).DayOfWeek) + "*\n\n";
            List<ScheduleField> dayShedule = userModel.Data.Group.ScheduleSubjects.Where(x => (int)x.DayOfWeek == ((int)DateTime.Now.AddDays(1).DayOfWeek)).ToList();
            if (dayShedule.Count == 0)
                return true;

            dayShedule.Sort(new Comparison<ScheduleField>((x, y) => x.Order.CompareTo(y.Order)));
            int daySheduleCount = dayShedule.Count;
            for (int sheduleField = 0; sheduleField < daySheduleCount; sheduleField++)
            {
                shedule += 
                "`" + (dayShedule[sheduleField].Order + 1).ToString() + "." + dayShedule[sheduleField].SubjectInstance.Subject.Name + "`\n" +
                "▸ `" + GetSubjectTypeString(dayShedule[sheduleField].SubjectInstance.SubjectType) + "`\n" +
                "▸ `" + dayShedule[sheduleField].SubjectInstance.Audience + "`\n" +
                "▸ `" + dayShedule[sheduleField].SubjectInstance.Teacher + "`\n"+
                "▸ `" + _subjectCalls[dayShedule[sheduleField].Order].StartLesson + " - " + _subjectCalls[dayShedule[sheduleField].Order].EndLesson + "`\n\n";
            }
            return true;
        }
    }
}
