using Core.Domain.Bus;
using Core.Domain.Commands;
using Core.Domain.Events;
using Core.Domain.Notifications;
using System;
using System.Threading.Tasks;

namespace CrossCutting.Core.Bus
{
    public class CoreMemoryBus : IMediatorHandler
    {
        /// <summary>
        /// Metodo de acesso ao containre de injecção de independencia
        /// </summary>
        public static Func<IServiceProvider> ContainerAccessor { get; set; }
        private static IServiceProvider Container => ContainerAccessor();
        public CoreMemoryBus()
        {

        }
        public async Task RaiseEvent<T>(T TEvent) where T : Event
        {
            await Publish(TEvent);
        }

        public async  Task SendCommand<T>(T TCommand) where T : Command
        {
            await Publish(TCommand);
        }
        //
        private async static Task Publish<T>(T message) where T : Message
        {
            if (Container == null) return;
            var x = message.MessageType.Equals("DomainNotification")
                ? typeof(IDomainNotificationHandler<T>)//when Domain handler
                : typeof(IHandler<T>);//whne another
            var obj = Container.GetService(message.MessageType.Equals("DomainNotification")
                ? typeof(IDomainNotificationHandler<T>)//when Domain handler
                : typeof(IHandler<T>));//whne another handle

            await ((IHandler<T>)obj).Handle(message);//get type of obj thas DI resolve and handle
        }
    }
}
