using Datum.Blog.API.Data.Entities;
using Datum.Blog.API.Data.Repository.Interfaces;

namespace Datum.Blog.API.Repository.Interfaces
{
    public interface IPostRepository: IBaseRepository<Post>
    {
        IQueryable<Post> GetAll();
        IQueryable<Post> GetByUsuarioId(string usuarioId);
        Post GetById(string id);
        Post GetPostByUsuarioId(string id, string usuarioId);
        double Count();
    }
}
