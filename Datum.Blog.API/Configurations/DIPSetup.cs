using Datum.Blog.API.Data.Repository;
using Datum.Blog.API.Data.Repository.Interfaces;
using Datum.Blog.API.Repository.Interfaces;

namespace Datum.Blog.API.Configurations
{
    public class DIPSetup
    {
        public static void Register(IServiceCollection service)
        {           
            service.AddTransient<IPostRepository, PostRepository>();
            service.AddTransient<IUsuarioRepository, UsuarioRepository>();
            service.AddTransient<IUnitOfWork,UnitOfWork>();
        }
    }
}
