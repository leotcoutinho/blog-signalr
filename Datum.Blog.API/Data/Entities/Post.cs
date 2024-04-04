namespace Datum.Blog.API.Data.Entities
{
    public class Post
    {
        public Post(Guid postId, Guid usuarioId, string comentario, DateTime dataCadastro)
        {
            PostId = postId;
            UsuarioId = usuarioId;
            Comentario = comentario;
            DataCadastro = dataCadastro;
        }

        public Guid PostId { get; set; }
        public Guid UsuarioId { get; set; }
        public string Comentario { get; set; }
        public DateTime DataCadastro { get; set; }

        public Usuario Usuario { get; set; }
    }
}
