using Microsoft.AspNetCore.Mvc;
using Negociacoes.WebApi.Enumeradores;
using Negociacoes.WebApi.Infra;
using Negociacoes.WebApi.Models.Dtos;
using Negociacoes.WebApi.Models.Entities;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Data.Entity.Migrations;

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

            var composicaoBase = _applicationContext.ComposicaoCarga.Add(composicao);
            await _applicationContext.SaveChangesAsync();

            var idComposicaoCarga = composicaoBase.Id;

            AddNegociacaoComposicaoCarga(idComposicaoCarga, TipoNegociacaoComposicaoCarga.COMPOSICAO_CARGA_CRIADA, TipoUsuario.PRODUTO, "Composição carga criada");

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

            _applicationContext.ComposicaoCarga.AddOrUpdate(composicao);

            AddNegociacaoComposicaoCarga(composicao.Id, TipoNegociacaoComposicaoCarga.COMPOSICAO_CARGA_ENVIADA_FORNECEDOR, TipoUsuario.FORNECEDOR, "Composição carga enviada fornecedor");

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

            _applicationContext.Pedido.AddOrUpdate(pedido);
            await _applicationContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("load-compositions/{id}/cancel")]
        public async Task<IActionResult> Cancelar(int id)
        {
            var composicao = await _applicationContext.Set<ComposicaoCarga>().FirstOrDefaultAsync(x => x.Id == id);

            if(composicao == null)
            {
                return BadRequest($"Composicao com o Id: {id} nao encontrado na base");
            }

            var pedidos = await _applicationContext.Set<Pedido>().Where(x => x.IdComposicaoCarga == composicao.Id).ToListAsync();

            composicao.Situacao = SituacaoComposicaoCarga.CANCELADA;
            foreach(var pedido in pedidos)
            {
                pedido.IdComposicaoCarga = null;
            }

            AddNegociacaoComposicaoCarga(composicao.Id, TipoNegociacaoComposicaoCarga.COMPOSICAO_CARGA_CANCELADA, TipoUsuario.PRODUTO, "Composição carga cancelada");

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
            var composicao = await _applicationContext.Set<ComposicaoCarga>().FirstOrDefaultAsync(x => x.Id == loadCompositionId);

            if(composicao == null)
            {
                return BadRequest($"composicao ({loadCompositionId}) nao encontrado na base");
            }

            var pedidosAtuaisDto = registerNegociacaoComposicaoCargaDto.PedidosAtuais.Select(x => new NegociacaoPedidoJson { IdPedido = x.IdPedido, Quantidade = x.Quantidade }).ToList();
            var sugestoesGeradasPorNegociacaoDto = registerNegociacaoComposicaoCargaDto.SugestoesGeradasPorNegociacao.Select(x => new NegociacaoSugestaoJson { Item = x.Item, Quantidade = x.Quantidade }).ToList();
            var pedidosNovosDto = registerNegociacaoComposicaoCargaDto.PedidosNovos.Select(x => new IntPedido { IdPedido = x }).ToList();
            var pedidosRemovidosDto = registerNegociacaoComposicaoCargaDto.PedidosRemovidos.Select(x => new IntPedido { IdPedido = x }).ToList();
            var sugestoesNovasDto = registerNegociacaoComposicaoCargaDto.SugestoesNovas.Select(x => new IntSugestao { IdSugestao = x }).ToList();

            var metaData = new NegociacaoComposicaoCargaJson
            {
                PedidosAtuais = pedidosAtuaisDto,
                SugestoesGeradasPorNegociacao = sugestoesGeradasPorNegociacaoDto,
                PedidosNovos = pedidosNovosDto,
                PedidosRemovidos = pedidosRemovidosDto,
                SugestoesNovas = sugestoesNovasDto
            };

            var jsonMetaData = JsonConvert.SerializeObject(metaData);

            var negociacao = new NegociacaoComposicaoCarga
            {
                DataEvento = DateTime.Now,
                IdComposicaoCarga = composicao.Id,
                Observacao = "Composicao carga em negociação",
                TipoNegociacao = TipoNegociacaoComposicaoCarga.COMPOSICAO_CARGA_EM_NEGOCIACAO,
                TipoUsuarioResponsavelProximaEtapa = TipoUsuario.FORNECEDOR,
                MetaData = jsonMetaData
            };

            composicao.Situacao = SituacaoComposicaoCarga.EM_NEGOCIACAO;

            var negociacaoBase = _applicationContext.NegociacaoComposicaoCarga.Add(negociacao);
            await _applicationContext.SaveChangesAsync();

            return Ok(negociacaoBase.Id);
        }

        [HttpPost("load-compositions/{loadCompositionId}/negotiations/emergency")]
        public async Task<IActionResult> PostEmergency(int loadCompositionId, [FromBody] RegisterNegociacaoComposicaoCargaDto registerNegociacaoComposicaoCargaDto)
        {
            var composicao = await _applicationContext.Set<ComposicaoCarga>().FirstOrDefaultAsync(x => x.Id == loadCompositionId);

            if(composicao == null)
            {
                return BadRequest($"composicao ({loadCompositionId}) nao encontrado na base");
            }

            var pedidosAtuaisDto = registerNegociacaoComposicaoCargaDto.PedidosAtuais.Select(x => new NegociacaoPedidoJson { IdPedido = x.IdPedido, Quantidade = x.Quantidade }).ToList();
            var sugestoesGeradasPorNegociacaoDto = registerNegociacaoComposicaoCargaDto.SugestoesGeradasPorNegociacao.Select(x => new NegociacaoSugestaoJson { Item = x.Item, Quantidade = x.Quantidade }).ToList();
            var pedidosNovosDto = registerNegociacaoComposicaoCargaDto.PedidosNovos.Select(x => new IntPedido { IdPedido = x }).ToList();
            var pedidosRemovidosDto = registerNegociacaoComposicaoCargaDto.PedidosRemovidos.Select(x => new IntPedido { IdPedido = x }).ToList();
            var sugestoesNovasDto = registerNegociacaoComposicaoCargaDto.SugestoesNovas.Select(x => new IntSugestao { IdSugestao = x }).ToList();

            var metaData = new NegociacaoComposicaoCargaJson
            {
                PedidosAtuais = pedidosAtuaisDto,
                SugestoesGeradasPorNegociacao = sugestoesGeradasPorNegociacaoDto,
                PedidosNovos = pedidosNovosDto,
                PedidosRemovidos = pedidosRemovidosDto,
                SugestoesNovas = sugestoesNovasDto
            };

            var jsonMetaData = JsonConvert.SerializeObject(metaData);

            var negociacao = new NegociacaoComposicaoCarga
            {
                DataEvento = DateTime.Now,
                IdComposicaoCarga = composicao.Id,
                Observacao = "Composicao carga em negociação emergencial",
                TipoNegociacao = TipoNegociacaoComposicaoCarga.COMPOSICAO_CARGA_EM_NEGOCIACAO_EMERGENCIAL,
                TipoUsuarioResponsavelProximaEtapa = TipoUsuario.FORNECEDOR,
                MetaData = jsonMetaData
            };

            composicao.Situacao = SituacaoComposicaoCarga.EM_NEGOCIACAO_EMERGENCIAL;

            var negociacaoBase = _applicationContext.NegociacaoComposicaoCarga.Add(negociacao);
            await _applicationContext.SaveChangesAsync();

            return Ok(negociacaoBase.Id);
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

            await ProcessaPedidoAceiteNegociacao(negociacao);
            await ProcessaSugestoesAceiteNegociacao(negociacao);

            var tipoNegociacao = negociacao.ComposicaoCarga.Situacao == SituacaoComposicaoCarga.EM_NEGOCIACAO ? TipoNegociacaoComposicaoCarga.COMPOSICAO_CARGA_EM_NEGOCIACAO_ACEITA : TipoNegociacaoComposicaoCarga.COMPOSICAO_CARGA_EM_NEGOCIACAO_EMERGENCIAL_ACEITA;
            var obsNegociacao = negociacao.ComposicaoCarga.Situacao == SituacaoComposicaoCarga.EM_NEGOCIACAO ? "Negociacao de composicao de carga aceita" : "Negociacao emergencial de composicao de carga aceita";

            negociacao.ComposicaoCarga.Situacao = SituacaoComposicaoCarga.ACEITA;

            AddNegociacaoComposicaoCarga(negociacao.IdComposicaoCarga, tipoNegociacao, TipoUsuario.PRODUTO, obsNegociacao);

            await _applicationContext.SaveChangesAsync();

            return NoContent();
        }

        private async Task ProcessaPedidoAceiteNegociacao(NegociacaoComposicaoCarga negociacao)
        {
            var metaData = JsonConvert.DeserializeObject<NegociacaoComposicaoCargaJson>(negociacao.MetaData);

            var pedidosParaAtualizar = new List<Pedido>();
            var pedidosParaAdicionar = new List<Pedido>();
            var pedidosParaRemover = new List<Pedido>();

            if(metaData.PedidosAtuais.Any())
            {
                var ids = metaData.PedidosAtuais.Select(x => x.IdPedido).ToList();
                pedidosParaAtualizar = await (from p in _applicationContext.Set<Pedido>() where ids.Contains(p.Id) select p).ToListAsync();
            }

            if(metaData.PedidosNovos.Any())
            {
                var ids = metaData.PedidosNovos.Select(x => x.IdPedido).ToList();
                pedidosParaAdicionar = await (from p in _applicationContext.Set<Pedido>() where ids.Contains(p.Id) select p).ToListAsync();
            }

            if(metaData.PedidosRemovidos.Any())
            {
                var ids = metaData.PedidosRemovidos.Select(x => x.IdPedido).ToList();
                pedidosParaRemover = await (from p in _applicationContext.Set<Pedido>() where ids.Contains(p.Id) select p).ToListAsync();
            }

            foreach(var pedido in pedidosParaAtualizar)
            {
                var pedidoReferencia = metaData.PedidosAtuais.First(x => x.IdPedido == pedido.Id);

                pedido.Quantidade = pedidoReferencia.Quantidade;
                pedido.DataEntrega = negociacao.ComposicaoCarga.DataEntrega;
            }

            foreach(var pedido in pedidosParaAdicionar)
            {
                var pedidoReferencia = metaData.PedidosNovos.First(x => x.IdPedido == pedido.Id);

                pedido.DataEntrega = negociacao.ComposicaoCarga.DataEntrega;
                pedido.IdComposicaoCarga = negociacao.IdComposicaoCarga;
            }

            foreach(var pedido in pedidosParaRemover)
            {
                var pedidoReferencia = metaData.PedidosRemovidos.First(x => x.IdPedido == pedido.Id);

                pedido.IdComposicaoCarga = null;
            }
        }

        private async Task ProcessaSugestoesAceiteNegociacao(NegociacaoComposicaoCarga negociacao)
        {
            var metaData = JsonConvert.DeserializeObject<NegociacaoComposicaoCargaJson>(negociacao.MetaData);

            var sugestoesParaAdicionar = new List<Sugestao>();

            if(metaData.SugestoesNovas.Any())
            {
                var ids = metaData.SugestoesNovas.Select(x => x.IdSugestao).ToList();
                sugestoesParaAdicionar = await (from s in _applicationContext.Set<Sugestao>() where ids.Contains(s.Id) select s).ToListAsync();
            }

            var sugestoesParaCriar = metaData.SugestoesGeradasPorNegociacao.ToList();

            foreach(var sugestao in sugestoesParaAdicionar)
            {
                var sugestaoReferencia = metaData.SugestoesNovas.First(x => x.IdSugestao == sugestao.Id);

                var pedido = Pedido.GetFromSugestao(sugestao);
                pedido.DataEntrega = negociacao.ComposicaoCarga.DataEntrega;
                pedido.IdComposicaoCarga = negociacao.IdComposicaoCarga;

                sugestao.Status = StatusSugestao.ACEITA;

                _applicationContext.Pedido.Add(pedido);
            }

            foreach(var sugestao in sugestoesParaCriar)
            {
                var pedido = new Pedido
                {
                    DataEntrega = negociacao.ComposicaoCarga.DataEntrega,
                    IdComposicaoCarga = negociacao.IdComposicaoCarga,
                    Item = sugestao.Item,
                    Quantidade = sugestao.Quantidade,
                    Status = StatusPedido.CRIADO
                };

                _applicationContext.Pedido.Add(pedido);
            }
        }

        private void AddNegociacaoComposicaoCarga(int idComposicaoCarga, TipoNegociacaoComposicaoCarga tipoNegociacaoComposicaoCarga, TipoUsuario tipoUsuarioResponsavelProximaEtapa, string observacao)
        {
            var negociacaoComposicaoCarga = new NegociacaoComposicaoCarga
            {
                IdComposicaoCarga = idComposicaoCarga,
                DataEvento = DateTime.Now,
                TipoNegociacao = tipoNegociacaoComposicaoCarga,
                TipoUsuarioResponsavelProximaEtapa = tipoUsuarioResponsavelProximaEtapa,
                Observacao = observacao
            };

            _applicationContext.NegociacaoComposicaoCarga.Add(negociacaoComposicaoCarga);
        }
    }
}
