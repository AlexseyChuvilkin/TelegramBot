using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;
using TelegramBot;
using WebApplication.Models.Commands;
using System.Threading.Tasks;
using Telegram.Bot;

namespace WebApplication.Controllers
{
    public class MessageController : ApiController
    {
        [Route(@"api/message/update")]
        public async Task<OkResult> Update([FromBody]Update update)
        {
            List<Command> commands = Bot.Commands.ToList();
            Message message = update.Message;
            TelegramBotClient client = await Bot.Get();

            Command command = commands.First();
            command.Execute(message, client);

            //foreach (Command command in commands)
            //{
            //    if(command.Contains(message.Text))
            //    {
            //        command.Execute(message, client);
            //        break;
            //    }
            //}
           return Ok();
        }
    }
}
