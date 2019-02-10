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
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [Route("api/[cntroller]")]
    public class UpdateController : ApiController
    {
        private readonly IUpdateService _updateService;

        public UpdateController(IUpdateService updateService) => _updateService = updateService;

        [HttpPost]
        public async Task<OkResult> Post([FromBody]Update update)
        {
            await _updateService.EchoAsync(update);
            return Ok();
        }
    }
}