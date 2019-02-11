using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telegram.Bot;

namespace WebApplication.Services
{
    public class BotService : IBotService
    {
        private readonly BotConfiguration _botConfiguration;
        private readonly TelegramBotClient _telegramBotClient;

        public BotService(BotConfiguration botConfiguration)
        {
            _botConfiguration = botConfiguration;
            _telegramBotClient = string.IsNullOrEmpty(_botConfiguration.Socks5Host) ? new TelegramBotClient(_botConfiguration.BotToken) : throw new NotImplementedException();
        }

        public TelegramBotClient Client => _telegramBotClient;
    }
}