using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Datum.Blog.API.Data.Entities;

namespace Datum.Blog.API.Data.Mappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => new { x.PostId, x.UsuarioId });
            builder.Property(x => x.Comentario).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.DataCadastro).IsRequired();
            builder.HasOne(x => x.Usuario)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.UsuarioId);
        }
    }

}
