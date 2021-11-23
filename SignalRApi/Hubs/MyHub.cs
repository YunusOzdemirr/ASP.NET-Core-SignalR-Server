using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRApi.Hubs
{
    public class MyHub : Hub
    {
        public async Task<int> SendRandomCharacter()
        {
            //TcpServer can be useable
            while (true)
            {
                Random random = new Random();
                var abc = random.Next(10000, 99999);
                await Clients.All.SendAsync("receiveMessage", "FromYunus", abc);
                Thread.Sleep(300);
                return abc;
            }
        }
        public async Task GetChar()
        {
            // await Clients.All.SendAsync("ReceiveNames", Names);
        }
        public async Task Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("broadcastMessage2", name, message);
        }
    }
}

