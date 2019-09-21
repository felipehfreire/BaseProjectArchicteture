using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity< TEntity> 
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> GetById(int id);
        Task<List<TEntity>> GetAll();
        Task<TEntity> Update(TEntity entity);
        Task Remove(int id);
        Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();

    }
}
