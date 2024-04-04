namespace Datum.Blog.API.Data.Repository.Interfaces
{
    public interface IBaseRepository<TEntity> : IDisposable
        where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
