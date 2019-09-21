using Core.Infra.Data.Context;
using Domain.Products;
using Domain.Products.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infra.Data.Repositorys
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(CoreContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Product>> GetProductByCategory(int CategoryId)
        {
            return await DbSet.AsNoTracking().Where(p => p.CategoryId == CategoryId).ToListAsync();
        }

    }
}
