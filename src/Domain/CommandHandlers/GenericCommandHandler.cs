using Core.Domain.Bus;
using Core.Domain.Notifications;
using Domain.Interfaces;
using FluentValidation.Results;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Domain.CommandHandlers
{
    public abstract class GenericCommandHandler
    {
        private readonly IUnityOfWork _uow;
        private readonly IMediatorHandler _mediator;
        private readonly IDomainNotificationHandler<DomainNotification> _notification;

        public GenericCommandHandler(IUnityOfWork uow, IMediatorHandler mediator, IDomainNotificationHandler<DomainNotification> notification)
        {
            _uow = uow;
            _mediator = mediator;
            _notification = notification;
        }

        protected void NotificationValidationErros(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
                _mediator.RaiseEvent(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }
        }


     
        protected  async  Task<bool> Commit()
        {

            if (await _notification.HasNotification()) return false;
            if (await _uow.Commit()) return true;

           await _mediator.RaiseEvent(new DomainNotification(MethodInfo.GetCurrentMethod().Name, "Erro ao salvar dados no banco"));
            return false;

        }
    }
}
