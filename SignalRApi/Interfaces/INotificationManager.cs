using System;
using System.Threading.Tasks;
using SignalRApi.Entities;

namespace SignalRApi.Interfaces
{
    public interface INotificationManager
    {
        public Task<Notification> SendNotification(Notification notification);
    }
}

