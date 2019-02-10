using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace WebApplication.Services
{
    public interface IBotService
    {
        TelegramBotClient Client { get; }
    }
}
