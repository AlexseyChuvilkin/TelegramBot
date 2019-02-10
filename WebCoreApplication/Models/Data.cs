using Database;
using Database.Data;
using Database.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebCoreApplication.Models
{
    public static class Data
    {
        private const int _timeDifferent = 0;
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
            _subjectCalls = _dataContext.SubjectCalls.ToList() ;
            _subjectCalls.Sort();
        }

        static public UserModel GetUserModel(Telegram.Bot.Types.User user)
        {
            if (_userModels.TryGetValue(user.Id, out UserModel userModel))
                return userModel;

            if (!_dataContext.GetUserByTelegramId(user.Id, out User data))
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
        static public bool GetFullShedule(UserModel userModel, out string schedule) => ScheduleView.GetSchedule(userModel.Data.Group, out schedule);
        static public bool GetSheduleOnTomorrow(UserModel userModel, out string schedule) => ScheduleView.GetSchedule(userModel.Data.Group, CorrectedDateTime.AddDays(1), _subjectCalls, out schedule);
        static public bool GetSheduleOnToday(UserModel userModel, out string schedule) => ScheduleView.GetSchedule(userModel.Data.Group, CorrectedDateTime, _subjectCalls, out schedule);
    }
}
