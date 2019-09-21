using Core.Domain.Events;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infra.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        public void SaveEvent<T>(T theEvent) where T : Event
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
