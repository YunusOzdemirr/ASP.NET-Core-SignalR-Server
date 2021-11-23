using System;
using Microsoft.AspNetCore.SignalR;
using SignalRApi.Entities;
using SignalRApi.Interfaces;

namespace SignalRApi.Hubs
{
    public class NotificationHub:Hub<INotificationManager>
    {
        public async void SendNotification(Notification notification)
        {
            await Clients.All.SendNotification(notification);
        }
    }
}

