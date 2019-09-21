using Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Products.Events
{
    public class BaseProductEvent : Event
    {
      

        public int Id { get; protected set; }
        public string Description { get; protected set; }
        public string InternalCode { get; protected set; }
        public string BarCode { get; protected set; }
        public decimal CostPrice { get; protected set; }
        public decimal SalePrice { get; protected set; }

        public int? CategoryId { get; protected set; }
        public int? ProductTypeId { get; protected set; }
    }
}
