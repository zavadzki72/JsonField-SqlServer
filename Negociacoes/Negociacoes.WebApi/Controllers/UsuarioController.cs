using Microsoft.AspNetCore.Mvc;
using Negociacoes.WebApi.Infra;
using Negociacoes.WebApi.Models.Dtos;
using Negociacoes.WebApi.Models.Entities;
using System.Data.Entity;

namespace Negociacoes.WebApi.Controllers
{
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;

        public UsuarioController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _applicationContext.Set<Usuario>().FirstOrDefaultAsync(x => x.Id == id);

            return Ok(data);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _applicationContext.Set<Usuario>().ToListAsync();

            return Ok(data);
        }

        [HttpPost("users")]
        public async Task<IActionResult> Post([FromBody] RegisterUsuarioDto registerUsuarioDto)
        {
            if(string.IsNullOrWhiteSpace(registerUsuarioDto.Nome) || string.IsNullOrWhiteSpace(registerUsuarioDto.Email))
            {
                return BadRequest("Dados invalidos");
            }

            var usuario = new Usuario
            {
                Nome = registerUsuarioDto.Nome,
                Email = registerUsuarioDto.Email,
                TipoUsuario = registerUsuarioDto.TipoUsuario
            };

            var usuarioBase = _applicationContext.Usuario.Add(usuario);
            await _applicationContext.SaveChangesAsync();

            return Ok(usuarioBase.Id);
        }
    }
}
