using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNetCore.SignalR;

namespace SignalRApi.Hubs
{
    public class MyHub : Hub
    {
        public readonly StockCaller _stock;

        public MyHub(StockCaller stock)
        {
            _stock = stock;
        }

        public async Task SendRandomCharacter()
        {
            //TcpServer can be useable
            while (true)
            {
                _stock.AddValue();
               // await Clients.All.SendAsync("receiveMessage", "FromYunus", abc);
                var result = _stock.GetValues();
                await Clients.All.SendAsync("receiveMessage", result);
                Thread.Sleep(500);
            }
            //_stock.AddValue();

        }
        public async Task Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("broadcastMessage2", name, message);
        }
        private void TimerCallback(Object o, ElapsedEventArgs e)
        {
            // Display the date/time when this method got called.
            _stock.ClearStock();
        }
    }
}

