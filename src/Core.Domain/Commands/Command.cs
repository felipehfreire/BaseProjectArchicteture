using Core.Domain.Events;
using MediatR;
using System;

namespace Core.Domain.Commands
{
    public class Command : Message, IRequest
    {
        public DateTime TimeStamp { get; set; }

        public Command()
        {
            this.TimeStamp = DateTime.Now;
        }
    }
}
