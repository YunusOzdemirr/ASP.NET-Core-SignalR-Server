using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRApi.Entities;
using SignalRApi.Hubs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private IHubContext<NotificationHub> _hubContext { get; set; }
        private StockCaller _caller;
        public NotificationController(IHubContext<NotificationHub> hubcontext, StockCaller caller)
        {
            _hubContext = hubcontext;
            _caller = caller;
        }
        // GET: api/values

        // POST api/values
        [HttpPost]
        public IActionResult SendNotificationToClient(Notification notification)
        {
            _hubContext.Clients.All.SendAsync("sendNotification", notification.Title,notification.Message,notification.SendDate);
            return Ok();
        }

        [HttpGet("get")]
        public IActionResult getvalues()
        {
          //  _hubContext.Clients.All.SendAsync("sendNotification", _caller.GetValues());
            return Ok();
        }
    }
}

