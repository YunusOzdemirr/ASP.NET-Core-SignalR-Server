using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRApi.Hubs
{
    public class MyHub : Hub
    {
        public static List<string> Names = new List<string>();
        public async Task SendRandomCharacter(string name)
        {
            Names.Add(name);
            Random random = new Random();
            await Clients.All.SendAsync("ReceiveMessage", random.Next(10000, 99999),name);
        }
        public async Task GetChar()
        {
            await Clients.All.SendAsync("ReceiveNames", Names);
        }
        public async Task Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("broadcastMessage", name, message);
        }
    }
}

