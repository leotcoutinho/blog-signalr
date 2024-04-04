using Datum.Blog.API.Configurations;
using Datum.Blog.API.Data.Entities;
using Datum.Blog.API.Data.Repository.Interfaces;
using Datum.Blog.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Datum.Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IConfiguration config;

        public UsuariosController(IUnitOfWork uow, IConfiguration config)
        {
            this.uow = uow;
            this.config = config;
        }      
               
        [HttpPost("login")]
        public IActionResult Post(string email, string password)
        {
            try
            {
                var usuario = uow.UsuarioRepository.GetUser(email, password);

                if (usuario == null)
                {
                    return NotFound(new { Message = "Credenciais inválidas!" });
                }

                var token = JwtTokenSetup.GenerateToken(usuario, config);

                usuario.Senha = string.Empty;

                return Ok(new { Token = token });
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("register")]
        public IActionResult Post(UsuarioModel model)
        {
            try
            {
                var usuario = new Usuario(Guid.NewGuid(), model.Nome, model.Email, model.Password);
                uow.UsuarioRepository.Add(usuario);

                return Ok(new { Message = "Cadastrado com sucesso!" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }        
    }
}
