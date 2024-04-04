using Datum.Blog.API.Data.Entities;
using Datum.Blog.API.Data.Repository.Interfaces;
using Datum.Blog.API.Hubs;
using Datum.Blog.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Datum.Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostsController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IHubContext<BlogHub> hubContext;

        public PostsController(IUnitOfWork uow, IHubContext<BlogHub> hubContext)
        {
            this.hubContext = hubContext;
            this.uow = uow;
        }

        [HttpPost("testeHub"), AllowAnonymous]
        public async Task<IActionResult> Post(string user, string message)
        {
            try
            {
                var formattedMessage = $"{user} - {message}";                               

                await hubContext.Clients.All.SendAsync("ReceiveMessage", formattedMessage);

                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet("{usuarioId}")]
        public IActionResult Get(string usuarioId)
        {
            try
            {
                var posts = uow.PostRepository.GetByUsuarioId(usuarioId);

                if (posts == null || posts?.Count() == 0)
                {
                    return NotFound(new { Message = "Nenhum post desse usuário foi encontrado." });
                }

                return Ok(posts);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("getAll"), AllowAnonymous]
        public IActionResult GetAll()
        {
            try
            {
                var post = uow.PostRepository.GetAll();

                if (post == null)
                {
                    return NotFound(new { Message = "Nenhum post doi encontrado." });
                }

                return Ok(post);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostModel model, IHubContext<BlogHub> context)
        {
            try
            {
                if (model == null)
                {
                    return NoContent();
                }

                var post = new Post(Guid.NewGuid(),
                                    Guid.Parse(model.UsuarioId),
                                    model.Comentario,
                                    DateTime.Now);

                uow.PostRepository.Add(post);

                var usuario = uow.UsuarioRepository.GetByUsuarioId(model.UsuarioId);

                await context.Clients.All.SendAsync("", usuario.Nome, model.Comentario);

                return Ok(new { Message = "Cadastrado com sucesso!" });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(PostUpdateModel model)
        {
            try
            {
                var post = uow.PostRepository.GetPostByUsuarioId(model.Id, model.UsuarioId);

                if (post == null)
                {
                    return NotFound(new { Message = $"O post com id = {model.Id} não foi encontrado." });
                }

                post.Comentario = model.Comentario;

                uow.PostRepository.Update(post);

                return Ok(new { Message = "Atualizado com sucesso!" });

            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public IActionResult Delete(PostDeleteModel model)
        {

            try
            {
                var post = uow.PostRepository.GetPostByUsuarioId(model.Id, model.UsuarioId);

                if (post == null)
                {
                    return NotFound(new { Message = $"O post com id = {model.Id} não foi encontrado." });
                }

                uow.PostRepository.Remove(post);

                return Ok(new { Message = "Removido com sucesso!" });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
