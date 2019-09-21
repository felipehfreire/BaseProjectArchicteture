using Core.Domain.Models;
using System.Collections.Generic;

namespace Domain.Products
{
    public class Category : Entity<Category>
    {


        // EF navigation property
        public virtual ICollection<Product> Products { get; set; }
        public string Name { get; private set; }

        public override bool IsValid()
        {
            Validate();
            //return ValidationResult.IsValid;
            return false;
        }

        public override void Validate()
        {
            throw new System.NotImplementedException();
        }
    }
}