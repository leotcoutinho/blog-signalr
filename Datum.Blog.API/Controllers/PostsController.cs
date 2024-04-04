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
        private string idUsuarioLogado;

        public PostsController(IUnitOfWork uow, IHubContext<BlogHub> hubContext)
        {
            this.hubContext = hubContext;
            this.uow = uow;
        }

        [HttpGet("getAllByUser")]
        public IActionResult GetByUser()
        {
            try
            {
                idUsuarioLogado = User.FindFirst("id").Value;

                var posts = uow.PostRepository.GetByUsuarioId(idUsuarioLogado);

                if (posts == null)
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
                var posts = uow.PostRepository.GetAll();               

                if (posts == null)
                {
                    return NotFound(new { Message = "Nenhum post doi encontrado." });
                }

                var filteredPosts = from post in posts
                                    select new
                                    {
                                        post.Usuario.Nome,
                                        post.Comentario,
                                        post.DataCadastro
                                    };

                return Ok(filteredPosts);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostModel model)
        {
            try
            {
                idUsuarioLogado = User.FindFirst("id").Value;
                string nome = User.FindFirst("nome").Value;

                if (model == null)
                {
                    return BadRequest(new { Message = "As informações não são válidas." });
                }

                var post = new Post(Guid.NewGuid(),
                                    Guid.Parse(idUsuarioLogado),
                                    model.Comentario,
                                    DateTime.Now);

                uow.PostRepository.Add(post);

                string finalMessage = $"{nome.ToUpper()} : {model.Comentario} \n--------------------------------------------------------";

                // envio pro websocket
                await hubContext.Clients.All.SendAsync("ReceiveMessage", finalMessage);

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
                idUsuarioLogado = User.FindFirst("id").Value;

                if(model == null)
                {
                    return BadRequest(new { Message = "As informações não são válidas." });
                }

                var post = uow.PostRepository.GetPostByUsuarioId(model.Id, idUsuarioLogado);

                if (post == null)
                {
                    return NotFound(new { Message = $"O post com id = {idUsuarioLogado} não foi encontrado." });
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
        public IActionResult Delete(string id)
        {
            try
            {
                idUsuarioLogado = User.FindFirst("id").Value;

                if(id == null)
                {
                    return BadRequest(new { Message = "O id deve ser informado corretamente!" });
                }

                var post = uow.PostRepository.GetPostByUsuarioId(id, idUsuarioLogado);

                if (post == null)
                {
                    return NotFound(new { Message = $"O post com id = {id} não foi encontrado." });
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
