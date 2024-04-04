using Datum.Blog.API.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Datum.Blog.API.Configurations
{
    public static class EntityFrameworkSetup
    {
        public static void AddEntityFrameworkSetup(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<DataContext>
                    (options => {
                        options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
                        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                    });
        }
    }
}
