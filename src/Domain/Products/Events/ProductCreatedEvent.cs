using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Products.Events
{
    public class ProductCreatedEvent : BaseProductEvent
    {
        public ProductCreatedEvent(int id, string description, string internalCode, string barCode, 
            decimal costPrice, decimal salePrice, int? categoryId, int? productTypeId)
        {
            Id = id;
            Description = description;
            InternalCode = internalCode;
            BarCode = barCode;
            CostPrice = costPrice;
            SalePrice = salePrice;
            CategoryId = categoryId;
            ProductTypeId = productTypeId;
        }
    }
}
