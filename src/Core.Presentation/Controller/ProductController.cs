using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.Services;
using Core.Application.ViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductAppService _productAppService;
        private readonly IMapper _mapper;
        public ProductController(IProductAppService productAppService,  IMapper mapper)
        {
            _productAppService = productAppService;
            _mapper = mapper;
        }

        [HttpGet]
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
