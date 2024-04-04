using Datum.Blog.API.Data.Entities;
using Datum.Blog.API.Repository.Context;
using Datum.Blog.API.Repository.Interfaces;

namespace Datum.Blog.API.Data.Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(DataContext context):base(context)
        {
                
        }

        public IQueryable<Post> GetAll()
        {
            return dbSet;
        }

        public IQueryable<Post> GetByUsuarioId(string usuarioId)
        {
            return dbSet.Where(x=>x.UsuarioId.Equals(Guid.Parse(usuarioId)));
        }

        public Post GetById(string id)
        {
            return dbSet.Find(Guid.Parse(id));
        }

        public Post GetPostByUsuarioId(string id, string usuarioId)
        {
            return dbSet.Where(x=>x.UsuarioId.Equals(Guid.Parse(usuarioId)) 
                               && x.PostId.Equals(Guid.Parse(id))).FirstOrDefault();
        }

        public double Count()
        {
           return dbSet.Count();
        }
    }
}
