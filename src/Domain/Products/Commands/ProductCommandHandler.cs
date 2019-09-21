using Core.Domain.Bus;
using Core.Domain.Events;
using Core.Domain.Notifications;
using Domain.CommandHandlers;
using Domain.Interfaces;
using Domain.Products.Events;
using Domain.Products.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Products.Commands
{
    public class ProductCommandHandler : GenericCommandHandler,
      IRequestHandler<CreateNewProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _mediator;
        public ProductCommandHandler(IProductRepository productRepository, IUnityOfWork uow, IMediatorHandler mediator , IDomainNotificationHandler<DomainNotification> notification) 
            :base(uow, mediator, notification)
        {
            _mediator = mediator;
            _productRepository = productRepository;
        }

        //public async  Task  Handle(CreateNewProductCommand message)
        //{
        //    var product = Product.ProductFactory.CreateNewProduct(message.Id,
        //        message.Description, message.InternalCode,message.BarCode,
        //        message.CostPrice,message.SalePrice, message.CategoryId, message.ProductTypeId);
        //    if (!IsValid(product)) return;

        //    // TODO:
        //    // validate business rules!
        //   await  _productRepository.Add(product);
        //    if (await Commit())
        //    {
        //        await _mediator.RaiseEvent(new ProductCreatedEvent(product.Id,product.Description, product.InternalCode,
        //            product.BarCode, product.CostPrice, product.SalePrice, product.CategoryId, product.ProductTypeId));
        //    }
        //}

        public async Task<Unit> Handle(CreateNewProductCommand message, CancellationToken cancellationToken)
        {
            var product = Product.ProductFactory.CreateNewProduct(message.Id,
                message.Description, message.InternalCode, message.BarCode,
                message.CostPrice, message.SalePrice, message.CategoryId, message.ProductTypeId);

            if (!IsValid(product)) return await Task<Unit>.Factory.StartNew(() =>new Unit());

            // TODO:
            //    // validate business rules!

            await _productRepository.Add(product);
            if (await Commit())
            {
                await _mediator.RaiseEvent(new ProductCreatedEvent(product.Id, product.Description, product.InternalCode,
                    product.BarCode, product.CostPrice, product.SalePrice, product.CategoryId, product.ProductTypeId));
            }


            return await Task<Unit>.Factory.StartNew(() =>
              new Unit()
               );
        }

        private bool IsValid(Product product)
        {
            if(product.IsValid()) return true;

            NotificationValidationErros(product.ValidationResult);
            return false;
        }
    }
}

