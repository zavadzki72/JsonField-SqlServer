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
            var negociacao = await _applicationContext.Set<NegociacaoJogador>().FirstOrDefaultAsync(x => x.Id == id);
            var negociacaoRetorno = NegociacaoJogadorDto.GetFromNegociacaoJogador(negociacao);

            return Ok(negociacaoRetorno);
        }

        [HttpPost("negotiations")]
        public async Task<IActionResult> Post(PostNegociacaoJogadorDto postNegociacaoJogadorDto)
        {
            var timeDestino = await _applicationContext.Set<Time>().FirstOrDefaultAsync(x => x.Id == postNegociacaoJogadorDto.IdTimeDestino);

            if (timeDestino == null)
            {
                return BadRequest($"Time destino ({postNegociacaoJogadorDto.IdTimeDestino}) nao encontrado na base");
            }

            var jogadoresAtuaisJson = JsonConvert.SerializeObject(postNegociacaoJogadorDto.JogadoresAtuais);
            var jogadoresNovosJson = JsonConvert.SerializeObject(postNegociacaoJogadorDto.JogadoresNovos);
            var jogadoresRemovidosJson = JsonConvert.SerializeObject(postNegociacaoJogadorDto.JogadoresRemovidos);

            var negociacao = new NegociacaoJogador
            {
                DataEvento = DateTime.Now,
                TipoNegociacaoJogador = TipoNegociacaoJogador.EM_NEGOCIACAO,
                DataContratoProposta = postNegociacaoJogadorDto.DataContratoProposta,
                Observacoes = postNegociacaoJogadorDto.Observacoes,
                IdTimeDestino = timeDestino.Id,
                JogadoresAtuais = jogadoresAtuaisJson,
                JogadoresNovos = jogadoresNovosJson,
                JogadoresRemovidos = jogadoresRemovidosJson
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