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
                var result = _stock.GetValues();
                await Clients.All.SendAsync("receiveMessage", result);
                Thread.Sleep(200);
                //foreach (var item in result)
                //{
                //    await Clients.All.SendAsync("receiveMessage", item);
                //    Thread.Sleep(200);
                //}
            }
        }
        
    }
}

