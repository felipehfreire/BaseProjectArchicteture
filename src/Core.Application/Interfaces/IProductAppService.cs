using Core.Application.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Interfaces
{
    public interface IProductAppService : IDisposable
    {
        Task<ProductViewModel> Add(ProductViewModel entity);
        Task<ProductViewModel> GetById(int id);
        Task<List<ProductViewModel>> GetAll();
        Task<ProductViewModel> Update(ProductViewModel entity);
        Task Remove(int id);
    }
}
