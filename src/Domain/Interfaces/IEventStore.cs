using Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IEventStore
    {
        void SaveEvent<T>(T theEvent) where T : Event;
    }
}
