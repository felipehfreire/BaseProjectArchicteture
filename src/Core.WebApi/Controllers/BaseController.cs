using Core.Domain.Bus;
using Core.Domain.Notifications;
using CrossCutting.Core.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Core.WebApi.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;

        protected BaseController(INotificationHandler<DomainNotification> notifications, 
            IUser user,
            IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;

        }

        protected async Task<bool> IsValidOperation()
        {
            return (!await _notifications.HasNotification());
        }

        protected async new Task<IActionResult> Response(object result = null)
        {
            if (await IsValidOperation())
            {

                return await Task<IActionResult>.Factory.StartNew(() =>
                   Ok(new { success = true,data = result}));

               
            }

            return await Task<IActionResult>.Factory.StartNew(() =>
                BadRequest(new
                {
                    success = false,
                    errors = _notifications.GetNotifications().Select(n => n.Value)
                })
               );
        }


        protected void NotifyErrorInvalidModel()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.RaiseEvent(new DomainNotification(code, message));
        }

        protected void AddErrrosIdentity(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                NotifyError(result.ToString(), error.Description);
            }
        }
    }
}
