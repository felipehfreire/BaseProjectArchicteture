using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.Events;
using MediatR;

namespace Core.Domain.Notifications
{
    public class DomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        public List<DomainNotification> _notifications { get; set; }
        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public async Task  Handle(DomainNotification message, CancellationToken cancellationToken)
        {
            await Task.Factory.StartNew(() => _notifications.Add(message));
        }

        public async Task<bool> HasNotification()
        {
            return await Task.FromResult(_notifications.Any());
            
        }

        public void Dispose()
        {
            _notifications = new List<DomainNotification>();
        }


    }
}
