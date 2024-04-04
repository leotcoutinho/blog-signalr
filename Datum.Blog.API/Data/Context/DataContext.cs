using Datum.Blog.API.Data.Entities;
using Datum.Blog.API.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Datum.Blog.API.Repository.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // mappings ORM
            modelBuilder.ApplyConfiguration(new PostMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());

            // adicionando índices
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(x => x.UsuarioId).IsUnique();
            });
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasIndex(x => x.PostId).IsUnique();
            });
        }

        // configurando o banco com a connection string isolada para o migrations(code-first)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // configurando a string de conexão isolada
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseSqlite(connectionString);
            }
        }

    }
}
