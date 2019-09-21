using Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Notifications
{
    public interface IDomainNotificationHandler<T> : IHandler<T> where T : Message
    {
        Task<bool> HasNotification();
        List<T> GetNotifications();
    }
}
