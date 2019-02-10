using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace WebCoreApplication.Services
{
    public static class KeyboardService
    {
        private enum Keyboard { None, StartMenu, CreateGroup, GroupMenu}
        static private Dictionary<Keyboard, ReplyMarkupBase> _keyboards;

        static public void Initialize()
        {
            _keyboards = new Dictionary<Keyboard, ReplyMarkupBase>
            {
                { Keyboard.None, new ReplyKeyboardRemove() { Selective = true } }
            };

            ReplyKeyboardMarkup startMenu = new ReplyKeyboardMarkup
            {
                Keyboard = new KeyboardButton[][]
                    {
                        new KeyboardButton[]
                        {
                            new KeyboardButton(RequestService.GetMessageByRequest(Request.CreateGroup)),
                        },

                        new KeyboardButton[]
                        {
                            new KeyboardButton(RequestService.GetMessageByRequest(Request.JoinGroup))
                        },
                    }
            };
            _keyboards.Add(Keyboard.StartMenu, startMenu);
            ReplyKeyboardMarkup createGroupMenu = new ReplyKeyboardMarkup
            {
                Keyboard = new KeyboardButton[][]
                    {
                        new KeyboardButton[]
                        {
                            new KeyboardButton(RequestService.GetMessageByRequest(Request.CreateGroup)),
                        },
                    }
            };
            _keyboards.Add(Keyboard.CreateGroup, createGroupMenu);
            ReplyKeyboardMarkup  groupMenu = new ReplyKeyboardMarkup
            {
                Keyboard = new KeyboardButton[][]
                    {
                        new KeyboardButton[]
                        {
                            new KeyboardButton(RequestService.GetMessageByRequest(Request.LeaveGroup)),
                        },

                        new KeyboardButton[]
                        {
                            new KeyboardButton(RequestService.GetMessageByRequest(Request.WatchFullSchedule)),
                            new KeyboardButton(RequestService.GetMessageByRequest(Request.WatchOnTomorrow))
                        },
                    }
            };
            _keyboards.Add(Keyboard.GroupMenu, groupMenu);
        }

        static public ReplyMarkupBase GetKeyboardByRequest(Request request)
        {
            switch (request)
            {
                case Request.Startup:
                    return _keyboards.GetValueOrDefault(Keyboard.StartMenu);
                case Request.CreateGroup:
                    return _keyboards.GetValueOrDefault(Keyboard.None);
                case Request.JoinGroup:
                    return _keyboards.GetValueOrDefault(Keyboard.None);
                case Request.GroupMenu:
                    return _keyboards.GetValueOrDefault(Keyboard.GroupMenu);
                default:
                    return _keyboards.GetValueOrDefault(Keyboard.None);
            }
        }
    }
}
