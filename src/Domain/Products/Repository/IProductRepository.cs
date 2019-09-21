using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Products.Repository
{
    public interface IProductRepository : IRepository<Product>
    {

        Task<IEnumerable<Product>> GetProductByCategory(int productId);
    }
}
