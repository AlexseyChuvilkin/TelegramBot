using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using WebApplication.Models.Commands;

namespace TelegramBot
{
    public static class Bot
    {
        private static TelegramBotClient _telegramBotClient;
        private static List<Command> _commandsList;

        public static IReadOnlyList<Command> Commands => _commandsList.AsReadOnly();

        public static async Task<TelegramBotClient> Get()
        {
            if (_telegramBotClient != null)
                return _telegramBotClient;

            _commandsList = new List<Command>
            {
                new HelloCommand()
            };
            _telegramBotClient = new TelegramBotClient(AppSettings.Key);
            string hook = string.Format(AppSettings.Url, "api/message/update");
            await _telegramBotClient.SetWebhookAsync(hook);
            return _telegramBotClient;
        }

        public static void Initialize() => Get();
    }
}
