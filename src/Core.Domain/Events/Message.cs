using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Events
{
    public abstract class Message : INotification
    {
        public Message()
        {
            MessageType = GetType().Name;
        }
        public string MessageType { get; protected set; }
        public int AggregateId { get; protected set; }
    }
}
