using Newtonsoft.Json;

namespace Datum.Blog.Client
{
    public class Post
    {
        public string Nome { get; set; }
        public string Comentario { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
