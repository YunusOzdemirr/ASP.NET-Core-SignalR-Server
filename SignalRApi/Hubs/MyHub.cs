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
        public static int i = 0;

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
               int value= await add();
                // await Clients.All.SendAsync("receiveMessage", "FromYunus", abc);
                var result =await _stock.GetValues();
                await Clients.All.SendAsync("receiveMessage", result,value);
            }
            //_stock.AddValue();

        }
        public async Task<int> add()
        {
            i++;
            await _stock.AddValue();
            return await Task.FromResult(i);
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

