using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using Telegram.Bot;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;
using Database.Data;

namespace WebApplication.Controllers
{
    [Route("api/message/update")]
    public class UpdateController : Controller
    {
        private readonly IUpdateService _updateService;
        public UpdateController(IUpdateService updateService) => _updateService = updateService;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Update update)
        {
            await _updateService.EchoAsync(update);
            return Ok();
        }
    }
}