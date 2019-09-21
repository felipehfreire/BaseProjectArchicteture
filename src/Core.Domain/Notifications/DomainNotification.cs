using Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Notifications
{
    public class DomainNotification : Event
    {
        public Guid DomainNotificationID { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }


        public DomainNotification(string key, string value)
        {
            DomainNotificationID = new Guid();
            this.Key = key;
            this.Value = value;
            Version = 1;
        }
    }
}
