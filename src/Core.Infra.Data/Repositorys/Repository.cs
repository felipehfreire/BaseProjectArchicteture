using Core.Domain.Models;
using Core.Infra.Data.Context;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infra.Data.Repositorys
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
    {
        protected CoreContext Db;
        protected DbSet<TEntity> DbSet;

        protected Repository(CoreContext context)
        {
            Db = context;
            DbSet = Db.Set<TEntity>();
        }

        public virtual async Task<TEntity> Add(TEntity obj)
        {
            EntityEntry<TEntity> entry =await DbSet.AddAsync(obj);
            return entry.Entity;
        }

        public virtual async Task<TEntity> Update(TEntity obj)
        {
            EntityEntry<TEntity> entry =DbSet.Update(obj);
            return await Task<TEntity>.Factory.StartNew(() =>
                      entry.Entity
                );
        }

        public virtual async Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetById(int id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public virtual async  Task<List<TEntity>> GetAll()
        {
            var x = await DbSet.ToListAsync();
            return x;
        }

        public virtual async Task Remove(int id)
        {
            await Update(await GetById(id));
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db.Dispose();
        }



       
    }
}
