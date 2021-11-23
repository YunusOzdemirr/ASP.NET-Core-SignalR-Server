using System;
namespace SignalRApi.Entities
{
    public class Notification
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime SendDate = DateTime.Now;

    }
}

