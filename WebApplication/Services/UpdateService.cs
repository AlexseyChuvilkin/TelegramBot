using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace WebApplication.Services
{
    public class UpdateService : IUpdateService
    {
        private readonly IBotService _botService;

        public UpdateService(IBotService botService) => _botService = botService;

        public async Task EchoAsync(Update update)
        {
            if (update.Type != UpdateType.Message)
                return;
            Message message = update.Message;

            if(message.Type == MessageType.Text)
            {
                await _botService.Client.SendTextMessageAsync(message.Chat.Id, message.Text);
            }
        }
    }
}