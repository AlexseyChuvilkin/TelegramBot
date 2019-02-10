using System;
using Database.Data.Model;
using Telegram.Bot.Types;
using WebCoreApplication.Services;

namespace WebCoreApplication.Models
{
    public class UserModel
    {
        private readonly Database.Data.Model.User _data;
        private readonly Telegram.Bot.Types.User _user;

        public UserModel(Database.Data.Model.User data, Telegram.Bot.Types.User user)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

        public Database.Data.Model.User Data => _data;
        public Telegram.Bot.Types.User User => _user;
        public Request LastRequest { get; set; }

    }
}
