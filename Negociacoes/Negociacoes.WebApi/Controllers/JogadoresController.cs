using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negociacoes.WebApi.Infra;
using Negociacoes.WebApi.Models.Dtos;
using Negociacoes.WebApi.Models.Entities;

namespace Negociacoes.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JogadoresController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;

        public JogadoresController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var jogador = await _applicationContext.Set<Jogador>().FirstOrDefaultAsync(x => x.Id == id);

            return Ok(jogador);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jogadores = await _applicationContext.Set<Jogador>().ToListAsync();

            return Ok(jogadores);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostJogadorDto postJogadorDto)
        {
            if(string.IsNullOrWhiteSpace(postJogadorDto.Nome) || postJogadorDto.Salario < 1)
            {
                return BadRequest("Dados invalidos");
            }

            var jogador = new Jogador
            {
                Nome = postJogadorDto.Nome,
                Salario = postJogadorDto.Salario
            };

            var jogadorBase = await _applicationContext.AddAsync(jogador);
            await _applicationContext.SaveChangesAsync();

            return Ok(jogadorBase.Entity.Id);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PostJogadorDto postJogadorDto)
        {
            if(string.IsNullOrWhiteSpace(postJogadorDto.Nome) || postJogadorDto.Salario < 1)
            {
                return BadRequest("Dados invalidos");
            }

            var jogador = await _applicationContext.Set<Jogador>().FirstOrDefaultAsync(x => x.Id == id);

            if(jogador == null)
            {
                return BadRequest($"Jogador com o Id: {id} nao encontrado na base");
            }

            jogador.Nome = postJogadorDto.Nome;
            jogador.Salario = postJogadorDto.Salario;

            _applicationContext.Update(jogador);
            await _applicationContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
