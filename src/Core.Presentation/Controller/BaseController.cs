using Core.Domain.Notifications;
using CrossCutting.Core.Identity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Presentation.Controller
{

    public class BaseController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        

        protected BaseController(IDomainNotificationHandler<DomainNotification> notifications, IUser user)
        {
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected async Task<bool> IsValidOperation()
        {
            return (!await _notifications.HasNotification());
        }
    }
}
