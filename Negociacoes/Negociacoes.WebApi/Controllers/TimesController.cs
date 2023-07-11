using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negociacoes.WebApi.Infra;
using Negociacoes.WebApi.Models.Dtos;
using Negociacoes.WebApi.Models.Entities;

namespace Negociacoes.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimesController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;

        public TimesController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var time = await _applicationContext.Set<Time>().FirstOrDefaultAsync(x => x.Id == id);

            return Ok(time);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var times = await _applicationContext.Set<Time>().ToListAsync();

            return Ok(times);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostTimeDto postTimeDto)
        {
            if(string.IsNullOrWhiteSpace(postTimeDto.Nome))
            {
                return BadRequest("Dados invalidos");
            }

            var time = new Time
            {
                Nome = postTimeDto.Nome
            };

            var timeBase = await _applicationContext.AddAsync(time);
            await _applicationContext.SaveChangesAsync();

            return Ok(timeBase.Entity.Id);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PostTimeDto postTimeDto)
        {
            if(string.IsNullOrWhiteSpace(postTimeDto.Nome))
            {
                return BadRequest("Dados invalidos");
            }

            var time = await _applicationContext.Set<Time>().FirstOrDefaultAsync(x => x.Id == id);

            if(time == null)
            {
                return BadRequest($"Time com o Id: {id} nao encontrado na base");
            }

            time.Nome = postTimeDto.Nome;

            _applicationContext.Update(time);
            await _applicationContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
