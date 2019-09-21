using Core.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Products
{
    public class Product : Entity< Product>
    {
        //ef contructor
        private Product(){}

        public string Description { get; private set; }
        public string InternalCode { get; private set; }
        public string BarCode { get; private set; }
        public decimal CostPrice { get; private set; }
        public decimal SalePrice { get; private set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int? ProductTypeId { get; set; }
        public virtual ProductType Type { get; set; }


        public override bool IsValid()
        {
            Validate();
            //return ValidationResult.IsValid;
            return ValidationResult.IsValid;
        }

        public override void Validate()
        {
            DescriptionValidate();
        }
        private void DescriptionValidate()
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("A descrição precisa ser fornecida")
                .Length(2, 500).WithMessage("A descrição precisa ter entre 2 e 150 caracteres");
        }


        public static class ProductFactory
        {
            public static Product CreateNewProduct(int id, string description, string internalCode,
            string barCode, decimal costPrice, decimal salePrice, int? categoryId, int? productTypeId)
            {
                var prod = new Product();
                prod.Id = id;
                prod.Description = description;
                prod.InternalCode = internalCode;
                prod.BarCode = barCode;
                prod.CostPrice = costPrice;
                prod.SalePrice = salePrice;
                prod.CategoryId = categoryId;
                prod.ProductTypeId = productTypeId;
                return prod;
            }
        }
    }
}
