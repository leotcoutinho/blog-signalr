using Datum.Blog.API.Data.Entities;
using Datum.Blog.API.Repository.Context;
using Datum.Blog.API.Repository.Interfaces;

namespace Datum.Blog.API.Data.Repository
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DataContext context) : base(context)
        {

        }

        public IQueryable<Usuario> GetAll()
        {
            return dbSet;
        }

        public Usuario GetByUsuarioId(string id)
        {
            return dbSet.Find(Guid.Parse(id));            
        }

        public Usuario GetUser(string email, string senha)
        {
            return dbSet.FirstOrDefault(x => x.Email.Equals(email) && x.Senha.Equals(senha));
        }
    }
}
