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
        private IHubContext<MyHub> _hubContext { get; set; }
        private readonly MyHub MyHub;
        private StockCaller _caller;
        public NotificationController(IHubContext<MyHub> hubcontext, StockCaller caller, MyHub myHub)
        {
            _hubContext = hubcontext;
            _caller = caller;
            MyHub = myHub;
        }

        [HttpGet]
        public IActionResult SendNotificationToClient(Notification notification)
        {
            //_hubContext.Clients.All.SendAsync("sendNotification", notification.Title, notification.Message, notification.SendDate);
            //var abc=MyHub.DelayCounter(500);
            return Ok();
        }

        [HttpGet("get")]
        public IActionResult getvalues()
        {
           // var abc = MyHub.DelayCounter(500);
            return Ok();
        }
    }
}

