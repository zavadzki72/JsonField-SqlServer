using Negociacoes.WebApi.Enumeradores;
using Negociacoes.WebApi.Models.Entities;

namespace Negociacoes.WebApi.Models.Dtos
{
    public class NegociacaoComposicaoCargaDto
    {
        public int Id { get; set; }
        public DateTime DataEvento { get; set; }
        public ComposicaoCarga ComposicaoCarga { get; set; }
        public string Observacao { get; set; }
        public TipoNegociacaoComposicaoCarga TipoNegociacao { get; set; }
        public TipoUsuario TipoUsuarioResponsavelProximaEtapa { get; set; }
        public List<NegociacaoPedidoDto> PedidosAtuais { get; set; }
        public List<NegociacaoSugestaoDto> SugestoesGeradasPorNegociacao { get; set; }
        public List<int> PedidosNovos { get; set; }
        public List<int> PedidosRemovidos { get; set; }
        public List<int> SugestoesNovas { get; set; }

        public static NegociacaoComposicaoCargaDto GetFromNegociacaoComposicaoCarga(NegociacaoComposicaoCarga negociacaoComposicaoCarga)
        {
            var metaData = negociacaoComposicaoCarga.MetaData;

            var pedidosAtuais = metaData.PedidosAtuais.Select(x => new NegociacaoPedidoDto { IdPedido = x.IdPedido, Quantidade = x.Quantidade }).ToList();
            var sugestoesGeradasPorNegociacao = metaData.SugestoesGeradasPorNegociacao.Select(x => new NegociacaoSugestaoDto { Item = x.Item, Quantidade = x.Quantidade }).ToList();
            var pedidosNovos = metaData.PedidosNovos.Select(x => x.IdPedido).ToList();
            var pedidosRemovidos = metaData.PedidosRemovidos.Select(x => x.IdPedido).ToList();
            var sugestoesNovas = metaData.SugestoesNovas.Select(x => x.IdSugestao).ToList();

            return new NegociacaoComposicaoCargaDto
            {
                Id = negociacaoComposicaoCarga.Id,
                DataEvento = negociacaoComposicaoCarga.DataEvento,
                ComposicaoCarga = negociacaoComposicaoCarga.ComposicaoCarga,
                Observacao = negociacaoComposicaoCarga.Observacao,
                TipoNegociacao = negociacaoComposicaoCarga.TipoNegociacao,
                TipoUsuarioResponsavelProximaEtapa = negociacaoComposicaoCarga.TipoUsuarioResponsavelProximaEtapa,
                PedidosAtuais = pedidosAtuais,
                SugestoesGeradasPorNegociacao = sugestoesGeradasPorNegociacao,
                PedidosNovos = pedidosNovos,
                PedidosRemovidos = pedidosRemovidos,
                SugestoesNovas = sugestoesNovas
            };
        }
    }

    public class NegociacaoPedidoDto
    {
        public int IdPedido { get; set; }
        public decimal Quantidade { get; set; }
    }

    public class NegociacaoSugestaoDto
    {
        public decimal Quantidade { get; set; }
        public string Item { get; set; }
    }
}
