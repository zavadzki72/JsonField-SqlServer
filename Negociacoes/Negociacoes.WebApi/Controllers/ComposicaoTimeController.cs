using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negociacoes.WebApi.Enumeradores;
using Negociacoes.WebApi.Infra;
using Negociacoes.WebApi.Models.Entities;

namespace Negociacoes.WebApi.Controllers
{
    [ApiController]
    public class ComposicaoTimeController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;

        public ComposicaoTimeController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpPost("composicoes")]
        public async Task<IActionResult> Post()
        {
            var composicao = new ComposicaoTime
            {
                SituacaoComposicaoTime = SituacaoComposicaoTime.CRIADA
            };

            var composicaoBase = await _applicationContext.AddAsync(composicao);
            await _applicationContext.SaveChangesAsync();

            return Ok(composicaoBase.Entity.Id);
        }

        [HttpPost("composicoes/{idComposicao}/time/{idTime}")]
        public async Task<IActionResult> AddTime(int idComposicao, int idTime)
        {
            var composicao = await _applicationContext.Set<ComposicaoTime>().FirstOrDefaultAsync(x => x.Id == idComposicao);

            if(composicao == null)
            {
                return BadRequest($"Composicao time com o Id: {idComposicao} nao encontrado na base");
            }

            var time = await _applicationContext.Set<Time>().FirstOrDefaultAsync(x => x.Id == idTime);

            if(time == null)
            {
                return BadRequest($"Time com o Id: {idTime} nao encontrado na base");
            }

            time.IdComposicaoTime = composicao.Id;

            _applicationContext.Update(time);
            await _applicationContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
