using Datum.Blog.API.Data.Entities;
using Datum.Blog.API.Data.Repository.Interfaces;

namespace Datum.Blog.API.Repository.Interfaces
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        IQueryable<Usuario> GetAll();
        Usuario GetByUsuarioId(string id);
        Usuario GetUser(string email, string senha);
    }
}
