using Datum.Blog.API.Data.Repository.Interfaces;
using Datum.Blog.API.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Datum.Blog.API.Data.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
       where TEntity : class
    {
        // DIP -> Principio de Inversão de Dependência
        protected readonly DataContext context;
        protected readonly DbSet<TEntity> dbSet;

        protected BaseRepository(DataContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            dbSet.Update(entity);
            context.SaveChanges();
        }

        public virtual void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
            context.SaveChanges();
        }        

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
