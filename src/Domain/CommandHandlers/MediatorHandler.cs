using Core.Domain.Bus;
using Core.Domain.Commands;
using Core.Domain.Events;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
    public  class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public MediatorHandler(IMediator mediator, IEventStore eventStore)
        {
            _mediator = mediator;
            _eventStore = eventStore;
        }

        public async Task RaiseEvent<T>(T theEvent) where T : Event
        {
            if (!theEvent.MessageType.Equals("DomainNotification"))
                _eventStore?.SaveEvent(theEvent);

            await _mediator.Publish(theEvent);
        }

        public async Task SendCommand<T>(T theCommand) where T : Command
        {
            await _mediator.Send(theCommand);
        }
    }
}
