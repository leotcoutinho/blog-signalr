using Datum.Blog.API.Configurations;
using Datum.Blog.API.Data.Entities;
using Datum.Blog.API.Data.Repository.Interfaces;
using Datum.Blog.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

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
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    return BadRequest(new { Message = "Favor informar as credenciais corretamente." });
                }

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
                if(model == null)
                {
                    return BadRequest(new { Message = "Informe os dados corretamente!"});
                }

                string regex = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";
                
                bool emailValido = Regex.IsMatch(model.Email, regex, RegexOptions.IgnoreCase);

                if (!emailValido)
                {
                    return BadRequest(new { Message = "E-mail informado incorretamente." });
                }

                var isRegistered = uow.UsuarioRepository.IsUser(model.Email.ToLower());

                if (isRegistered)
                {
                    return BadRequest(new { Message = "Já existe esse email cadastrado." });
                }

                var usuario = new Usuario(Guid.NewGuid(), 
                                          model.Nome, 
                                          model.Email.ToLower(), 
                                          model.Password.Length <= 6 ? model.Password 
                                                                     : throw new Exception("O password deve conter no máximo 6 caracteres") );
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
