namespace Datum.Blog.API.Data.Entities
{
    public class Usuario
    {
        public Usuario(Guid usuarioId, string nome, string email, string senha)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Email = email;
            Senha = senha;
        }

        public Guid UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public List<Post> Posts { get; set; }
    }
}
