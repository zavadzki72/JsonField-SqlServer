using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negociacoes.WebApi.Enumeradores;
using Negociacoes.WebApi.Infra;
using Negociacoes.WebApi.Models.Dtos;
using Negociacoes.WebApi.Models.Entities;
using Newtonsoft.Json;

namespace Negociacoes.WebApi.Controllers
{
    [ApiController]
    public class NegociacaoJogadorController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;

        public NegociacaoJogadorController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpGet("negotiations/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var negociacao = await _applicationContext.Set<NegociacaoJogador>()
                .Include(x => x.ComposicaoTime)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            var negociacaoRetorno = NegociacaoJogadorDto.GetFromNegociacaoJogador(negociacao);

            return Ok(negociacaoRetorno);
        }

        [HttpPost("negotiations")]
        public async Task<IActionResult> Post(PostNegociacaoJogadorDto postNegociacaoJogadorDto)
        {
            var composicaoTime = await _applicationContext.Set<ComposicaoTime>().FirstOrDefaultAsync(x => x.Id == postNegociacaoJogadorDto.IdComposicaoTime);

            if (composicaoTime == null)
            {
                return BadRequest($"composicao Time ({postNegociacaoJogadorDto.IdComposicaoTime}) nao encontrado na base");
            }

            var jogadoresAtuaisDto = postNegociacaoJogadorDto.JogadoresAtuais.Select(x => new JogadorDataDto { IdJogador = x.Id, Salario = x.Salario }).ToList();
            var jogadoresNovosDto = postNegociacaoJogadorDto.JogadoresNovos.Select(x => new IntData { IdJogador = x }).ToList();
            var jogadoresRemovidosDto = postNegociacaoJogadorDto.JogadoresRemovidos.Select(x => new IntData { IdJogador = x }).ToList();

            var negociacao = new NegociacaoJogador
            {
                DataEvento = DateTime.Now,
                TipoNegociacaoJogador = TipoNegociacaoJogador.EM_NEGOCIACAO,
                DataContratoProposta = postNegociacaoJogadorDto.DataContratoProposta,
                Observacoes = postNegociacaoJogadorDto.Observacoes,
                IdComposicaoTime = composicaoTime.Id,
                NegociacaoJogadorJson = new NegociacaoJogadorJson
                {
                    JogadoresAtuais = jogadoresAtuaisDto,
                    JogadoresNovos = jogadoresNovosDto,
                    JogadoresRemovidos = jogadoresRemovidosDto
                }
            };

            var negociacaoBase = await _applicationContext.AddAsync(negociacao);
            await _applicationContext.SaveChangesAsync();

            return Ok(negociacaoBase.Entity.Id);
        }

        [HttpPost("negotiations/{id}/accept")]
        public async Task<IActionResult> Accept(int id)
        {
            var negociation = await _applicationContext.Set<NegociacaoJogador>().FirstOrDefaultAsync(x => x.Id == id);

            if(negociation == null)
            {
                return BadRequest($"Negociacao ({negociation}) nao encontrada na base");
            }

            throw new NotImplementedException();
        }
    }
}