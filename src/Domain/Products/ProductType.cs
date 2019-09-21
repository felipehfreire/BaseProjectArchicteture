using Core.Domain.Models;

namespace Domain.Products
{
    public class ProductType : Entity< ProductType>
    {
        public ProductType()
        {

        }
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