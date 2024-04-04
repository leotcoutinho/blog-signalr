using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Datum.Blog.API.Data.Entities;

namespace Datum.Blog.API.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.UsuarioId);
            builder.Property(x => x.Email).HasMaxLength(64).IsRequired();
            builder.Property(x => x.Nome).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Senha).HasMaxLength(6).IsRequired();   
        }
    }
}
