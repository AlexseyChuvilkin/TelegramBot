using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;

namespace WebCoreApplication.Services
{
    public static class KeyboardService
    {
        private enum Keyboard { None, Backward, StartMenu, CreateGroup, GroupMenu}
        static private Dictionary<Keyboard, ReplyMarkupBase> _keyboards;

        static public void Initialize()
        {
            _keyboards = new Dictionary<Keyboard, ReplyMarkupBase>
            {
                { Keyboard.None, new ReplyKeyboardRemove() { Selective = true } }
            };

            ReplyKeyboardMarkup backward = new ReplyKeyboardMarkup
            {
                Keyboard = new KeyboardButton[][]
                    {
                        new KeyboardButton[]
                        {
                            new KeyboardButton(RequestService.GetMessageByRequest(Request.Backward)),
                        },
                    }
            };
            backward.ResizeKeyboard = true;
            _keyboards.Add(Keyboard.Backward, backward);
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
            startMenu.ResizeKeyboard = true;
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
            createGroupMenu.ResizeKeyboard = true;
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
                        },
                          new KeyboardButton[]
                        {
                            new KeyboardButton(RequestService.GetMessageByRequest(Request.WatchScheduleOnTomorrow)),
                        },
                           new KeyboardButton[]
                        {
                            new KeyboardButton(RequestService.GetMessageByRequest(Request.WatchScheduleOnToday)),
                        },
                    }
            };
            groupMenu.ResizeKeyboard = true;
            _keyboards.Add(Keyboard.GroupMenu, groupMenu);
        }

        static public ReplyMarkupBase GetKeyboardByRequest(Request request)
        {
            switch (request)
            {
                case Request.Startup:
                    return _keyboards.GetValueOrDefault(Keyboard.StartMenu);
                case Request.Backward:
                    return _keyboards.GetValueOrDefault(Keyboard.Backward);
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
