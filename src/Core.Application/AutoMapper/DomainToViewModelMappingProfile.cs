using AutoMapper;
using Core.Application.ViewModels.Product;
using Domain.Products;

namespace Core.Application.AutoMapper
{
    class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Product, ProductViewModel>();
        }
    }
}
