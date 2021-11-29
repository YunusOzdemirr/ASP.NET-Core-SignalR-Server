using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.SignalR;
using SignalRApi.Entities;

namespace SignalRApi.Hubs
{
    public class MyHub : Hub
    {
        public static int i = 0;

        public readonly StockCaller _stock;

        public MyHub(StockCaller stock)
        {
            _stock = stock;
        }

        public async Task<bool> SendRandomCharacter()
        {
            while (true)
            {
               // var result = _stock.GetValues();
                var result2 = _stock.GetValues2();
               // await Clients.All.SendAsync("receiveMessage", result);
                await Clients.All.SendAsync("receiveMessage", result2);
                Thread.Sleep(200);
            }
            //_stock.AddValue();
        }

        public async Task Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("broadcastMessage2", name, message);
        }
        
    }
}

