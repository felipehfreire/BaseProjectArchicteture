using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.ViewModels.Product;
using Core.Domain.Bus;
using Core.Domain.Notifications;
using Core.WebApi.Controllers;
using CrossCutting.Core.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Presentation.Controller
{
    [Route("api/[controller]")]
    
    public class ProductController : BaseController
    {
        private IProductAppService _productAppService;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public ProductController(INotificationHandler<DomainNotification> notifications,
            IUser user,
            IProductAppService productAppService, 
            IMapper mapper,
            IMediatorHandler mediator
            ) :base(notifications, user, mediator)
        {
            _productAppService = productAppService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async  Task<IEnumerable<ProductViewModel>> Get()
        {
            return await _productAppService.GetAll();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProductViewModel productViewModel)
        {
            var x = await _productAppService.Add(productViewModel);
            ;

            return await Task<OkObjectResult>.Factory.StartNew(() =>
                (Ok(new{ success = true, data = x}))
               );
           
        }
    }
}
