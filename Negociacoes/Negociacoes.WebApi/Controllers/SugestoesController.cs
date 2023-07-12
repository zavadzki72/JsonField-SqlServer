using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negociacoes.WebApi.Enumeradores;
using Negociacoes.WebApi.Infra;
using Negociacoes.WebApi.Models.Dtos;
using Negociacoes.WebApi.Models.Entities;

namespace Negociacoes.WebApi.Controllers
{
    [ApiController]
    public class SugestoesController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;

        public SugestoesController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpGet("suggestions/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _applicationContext.Set<Sugestao>().FirstOrDefaultAsync(x => x.Id == id);

            return Ok(data);
        }

        [HttpGet("suggestions")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _applicationContext.Set<Sugestao>().ToListAsync();

            return Ok(data);
        }

        [HttpPost("suggestions")]
        public async Task<IActionResult> Post([FromBody] RegisterSugestaoDto registerSugestaoDto)
        {
            if(string.IsNullOrWhiteSpace(registerSugestaoDto.Item) || registerSugestaoDto.Quantidade < 1)
            {
                return BadRequest("Dados invalidos");
            }

            var sugestao = new Sugestao
            {
                DataEntrega = registerSugestaoDto.DataEntrega,
                Item = registerSugestaoDto.Item,
                Quantidade = registerSugestaoDto.Quantidade,
                Status = StatusSugestao.CRIADA
            };

            var sugestaoBase = await _applicationContext.AddAsync(sugestao);
            await _applicationContext.SaveChangesAsync();

            return Ok(sugestaoBase.Entity.Id);
        }

        [HttpPatch("suggestions/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RegisterSugestaoDto registerSugestaoDto)
        {
            if(string.IsNullOrWhiteSpace(registerSugestaoDto.Item) || registerSugestaoDto.Quantidade < 1)
            {
                return BadRequest("Dados invalidos");
            }

            var data = await _applicationContext.Set<Sugestao>().FirstOrDefaultAsync(x => x.Id == id);

            if(data == null)
            {
                return BadRequest($"Sugestao com o Id: {id} nao encontrado na base");
            }

            data.DataEntrega = registerSugestaoDto.DataEntrega;
            data.Item = registerSugestaoDto.Item;
            data.Quantidade = registerSugestaoDto.Quantidade;

            _applicationContext.Update(data);
            await _applicationContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
