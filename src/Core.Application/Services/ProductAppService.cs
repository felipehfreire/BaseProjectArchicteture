using AutoMapper;
using Core.Application.Interfaces;
using Core.Application.ViewModels.Product;
using Core.Domain.Bus;
using Domain.Interfaces;
using Domain.Products.Commands;
using Domain.Products.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class ProductAppService : IProductAppService
    {

        private readonly IMediatorHandler _mediator;
        private readonly IUnityOfWork _uow;
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductAppService(IMediatorHandler mediator, IUnityOfWork uow, IProductRepository productRepository,  IMapper mapper)
        {
            _mediator = mediator;
            _uow = uow;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductViewModel> Add(ProductViewModel prodVM)
        {
            var createCommand = _mapper.Map<CreateNewProductCommand>(prodVM);
            await _mediator.SendCommand(createCommand);
            //TODO
            return null;
        }

        public void Dispose()
        {
            _productRepository.Dispose();
            
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            return  _mapper.Map<List<ProductViewModel>>(await _productRepository.GetAll());
            
        }

        public async Task<ProductViewModel> GetById(int id)
        {
            return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
        }

        public async  Task Remove(int id)
        {
            //_bus.SendCommand(new ExcluirEventoCommand(id));
            //await 
            await _productRepository.Remove(id);
        }

        public async Task<ProductViewModel> Update(ProductViewModel entity)
        {
            //TODO
            return null;
        }
    }
}
