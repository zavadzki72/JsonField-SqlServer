using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Negociacoes.WebApi.Enumeradores;
using Negociacoes.WebApi.Infra;
using Negociacoes.WebApi.Models.Dtos;
using Negociacoes.WebApi.Models.Entities;

namespace Negociacoes.WebApi.Controllers
{
    [ApiController]
    public class ComposicaoCargaController : ControllerBase
    {
        private readonly ApplicationContext _applicationContext;

        public ComposicaoCargaController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [HttpPost("load-compositions")]
        public async Task<IActionResult> Post([FromBody] RegisterComposicaoCargaDto registerComposicaoCargaDto)
        {
            var usuario = await _applicationContext.Set<Usuario>().FirstOrDefaultAsync(x => x.Id == registerComposicaoCargaDto.IdUsuario);

            if(usuario == null)
            {
                return BadRequest($"Usuario com o Id: {registerComposicaoCargaDto.IdUsuario} nao encontrado na base");
            }

            var composicao = new ComposicaoCarga
            {
                DataEntrega = registerComposicaoCargaDto.DataEntrega,
                IdUsuario = usuario.Id,
                Observacao = registerComposicaoCargaDto.Observacao,
                Situacao = SituacaoComposicaoCarga.CRIADA
            };

            var composicaoBase = await _applicationContext.AddAsync(composicao);
            await _applicationContext.SaveChangesAsync();

            var idComposicaoCarga = composicaoBase.Entity.Id;

            await AddNegociacaoComposicaoCarga(idComposicaoCarga, TipoNegociacaoComposicaoCarga.COMPOSICAO_CARGA_CRIADA, TipoUsuario.PRODUTO, "Composição carga criada");

            await _applicationContext.SaveChangesAsync();

            return Ok(idComposicaoCarga);
        }

        [HttpPost("load-compositions/{id}/send-supplier")]
        public async Task<IActionResult> EnviarFornecedor(int id)
        {
            var composicao = await _applicationContext.Set<ComposicaoCarga>().FirstOrDefaultAsync(x => x.Id == id);

            if(composicao == null)
            {
                return BadRequest($"Composicao com o Id: {id} nao encontrado na base");
            }

            composicao.Situacao = SituacaoComposicaoCarga.ENVIADO_FORNECEDOR;

            _applicationContext.Update(composicao);

            await AddNegociacaoComposicaoCarga(composicao.Id, TipoNegociacaoComposicaoCarga.COMPOSICAO_CARGA_ENVIADA_FORNECEDOR, TipoUsuario.FORNECEDOR, "Composição carga enviada fornecedor");

            await _applicationContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("load-compositions/{loadCompositionId}/order/{orderId}")]
        public async Task<IActionResult> AddTime(int loadCompositionId, int orderId)
        {
            var composicao = await _applicationContext.Set<ComposicaoCarga>().FirstOrDefaultAsync(x => x.Id == loadCompositionId);

            if(composicao == null)
            {
                return BadRequest($"Composicao time com o Id: {loadCompositionId} nao encontrado na base");
            }

            var pedido = await _applicationContext.Set<Pedido>().FirstOrDefaultAsync(x => x.Id == orderId);

            if(pedido == null)
            {
                return BadRequest($"Pedido com o Id: {orderId} nao encontrado na base");
            }

            pedido.IdComposicaoCarga = composicao.Id;

            _applicationContext.Update(pedido);
            await _applicationContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("load-compositions/{loadCompositionId}/negotiations")]
        public async Task<IActionResult> Get(int loadCompositionId)
        {
            var negociacao = await _applicationContext.Set<NegociacaoComposicaoCarga>()
                .Include(x => x.ComposicaoCarga)
                .FirstOrDefaultAsync(x => x.IdComposicaoCarga == loadCompositionId);

            var negociacaoRetorno = NegociacaoComposicaoCargaDto.GetFromNegociacaoComposicaoCarga(negociacao);

            return Ok(negociacaoRetorno);
        }

        [HttpGet("load-compositions/{loadCompositionId}/negotiations/{negotiationId}")]
        public async Task<IActionResult> Get(int loadCompositionId, int negotiationId)
        {
            var negociacao = await _applicationContext.Set<NegociacaoComposicaoCarga>()
                .Include(x => x.ComposicaoCarga)
                .FirstOrDefaultAsync(x => x.Id == negotiationId && x.IdComposicaoCarga == loadCompositionId);

            var negociacaoRetorno = NegociacaoComposicaoCargaDto.GetFromNegociacaoComposicaoCarga(negociacao);

            return Ok(negociacaoRetorno);
        }

        [HttpPost("load-compositions/{loadCompositionId}/negotiations")]
        public async Task<IActionResult> Post(int loadCompositionId, [FromBody] RegisterNegociacaoComposicaoCargaDto registerNegociacaoComposicaoCargaDto)
        {
            var composicao = await _applicationContext.Set<NegociacaoComposicaoCarga>().FirstOrDefaultAsync(x => x.Id == loadCompositionId);

            if(composicao == null)
            {
                return BadRequest($"composicao ({loadCompositionId}) nao encontrado na base");
            }

            var pedidosAtuaisDto = registerNegociacaoComposicaoCargaDto.PedidosAtuais.Select(x => new NegociacaoPedidoJson { IdPedido = x.IdPedido, Quantidade = x.Quantidade }).ToList();
            var pedidosNovosDto = registerNegociacaoComposicaoCargaDto.PedidosNovos.Select(x => new IntPedido { IdPedido = x }).ToList();
            var pedidosRemovidosDto = registerNegociacaoComposicaoCargaDto.PedidosRemovidos.Select(x => new IntPedido { IdPedido = x }).ToList();
            var sugestoesNovasDto = registerNegociacaoComposicaoCargaDto.SugestoesNovas.Select(x => new IntSugestao { IdSugestao = x }).ToList();

            var negociacao = new NegociacaoComposicaoCarga
            {
                DataEvento = DateTime.Now,
                IdComposicaoCarga = composicao.Id,
                Observacao = "Composicao carga em negociação",
                TipoNegociacao = TipoNegociacaoComposicaoCarga.COMPOSICAO_CARGA_EM_NEGOCIACAO,
                TipoUsuarioResponsavelProximaEtapa = TipoUsuario.FORNECEDOR,
                MetaData = new NegociacaoComposicaoCargaJson
                {
                    PedidosAtuais = pedidosAtuaisDto,
                    PedidosNovos = pedidosNovosDto,
                    PedidosRemovidos = pedidosRemovidosDto,
                    SugestoesNovas = sugestoesNovasDto
                }
            };

            var negociacaoBase = await _applicationContext.AddAsync(negociacao);
            await _applicationContext.SaveChangesAsync();

            return Ok(negociacaoBase.Entity.Id);
        }

        [HttpPost("load-compositions/{loadCompositionId}/negotiations/{negotiationId}/accept")]
        public async Task<IActionResult> Accept(int loadCompositionId, int negotiationId)
        {
            var negociacao = await _applicationContext.Set<NegociacaoComposicaoCarga>()
                            .Include(x => x.ComposicaoCarga)
                            .FirstOrDefaultAsync(x => x.Id == negotiationId && x.IdComposicaoCarga == loadCompositionId);

            if(negociacao == null)
            {
                return BadRequest($"Negociacao com o id: {negotiationId} nao encontrada na base");
            }

            throw new NotImplementedException();
        }

        private async Task AddNegociacaoComposicaoCarga(int idComposicaoCarga, TipoNegociacaoComposicaoCarga tipoNegociacaoComposicaoCarga, TipoUsuario tipoUsuarioResponsavelProximaEtapa, string observacao)
        {
            var negociacaoComposicaoCarga = new NegociacaoComposicaoCarga
            {
                IdComposicaoCarga = idComposicaoCarga,
                DataEvento = DateTime.Now,
                TipoNegociacao = tipoNegociacaoComposicaoCarga,
                TipoUsuarioResponsavelProximaEtapa = tipoUsuarioResponsavelProximaEtapa,
                Observacao = observacao
            };

            await _applicationContext.AddAsync(negociacaoComposicaoCarga);
        }
    }
}
