using Datum.Blog.API.Data.Repository.Interfaces;
using Datum.Blog.API.Repository.Context;
using Datum.Blog.API.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Datum.Blog.API.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction transaction;
        private readonly DataContext context;

        public UnitOfWork(DataContext context)
        {
            this.context = context;            
        }

        #region Transactions

        public void Dispose()
        {
            context.Dispose();
        }

        public void BeginTransaction()
        {
            transaction = context.Database.BeginTransaction();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void RollBack()
        {
            transaction.Rollback();
        }

        #endregion

        public IPostRepository PostRepository => new PostRepository(context);
        public IUsuarioRepository UsuarioRepository => new UsuarioRepository(context);
    }
}
