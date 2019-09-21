using MediatR;
using System;

namespace Core.Domain.Events
{
    public class Event : Message, INotification
    {
        public Event()
        {
            TimeStamp = DateTime.Now;
        }

        public DateTime TimeStamp { get; set; }
    }
}
