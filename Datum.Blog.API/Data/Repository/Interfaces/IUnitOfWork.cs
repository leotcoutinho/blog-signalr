using Datum.Blog.API.Repository.Interfaces;

namespace Datum.Blog.API.Data.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        #region Transactions

        void BeginTransaction();
        void Commit();
        void RollBack();

        #endregion

        IPostRepository PostRepository { get; }
        IUsuarioRepository UsuarioRepository { get; }
    }
}
