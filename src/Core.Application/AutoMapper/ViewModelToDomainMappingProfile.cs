using AutoMapper;
using Core.Application.ViewModels.Product;
using Domain.Products.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.AutoMapper
{
    class ViewModelToDomainMappingProfile: Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductViewModel, CreateNewProductCommand>()
                .ConstructUsing(p => new CreateNewProductCommand(p.Id,p.Description,p.InternalCode,
                    p.BarCode,p.CostPrice,p.SalePrice, p.categoryId,p.productTypeId));
        }

    }
}
