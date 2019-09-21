using Core.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Products.Events
{
    public class ProductEventHandler : INotificationHandler<ProductCreatedEvent>
        //IHandler<ProductCreatedEvent>
    {

        public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Registrado com sucesso");
            return Task.CompletedTask;
        }
    }
}
