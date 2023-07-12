using Negociacoes.WebApi.Enumeradores;

namespace Negociacoes.WebApi.Models.Entities
{
    public class NegociacaoComposicaoCarga
    {
        public int Id { get; set; }
        public DateTime DataEvento { get; set; }
        public int IdComposicaoCarga { get; set; }
        public string Observacao { get; set; }
        public TipoNegociacaoComposicaoCarga TipoNegociacao { get; set; }
        public TipoUsuario TipoUsuarioResponsavelProximaEtapa { get; set; }
        public string MetaData { get; set; }

        public virtual ComposicaoCarga ComposicaoCarga { get; set; }
    }

    public class NegociacaoComposicaoCargaJson
    {
        public List<NegociacaoPedidoJson> PedidosAtuais { get; set; } = new();
        public List<IntPedido> PedidosNovos { get; set; } = new();
        public List<IntPedido> PedidosRemovidos { get; set; } = new();
        public List<IntSugestao> SugestoesNovas { get; set; } = new();
        public List<NegociacaoSugestaoJson> SugestoesGeradasPorNegociacao { get; set; } = new();
    }

    public class NegociacaoPedidoJson
    {
        public int IdPedido { get; set; }
        public decimal Quantidade { get; set; }
    }

    public class NegociacaoSugestaoJson
    {
        public decimal Quantidade { get; set; }
        public string Item { get; set; }
    }

    public class IntPedido
    {
        public int IdPedido { get; set; }
    }

    public class IntSugestao
    {
        public int IdSugestao { get; set; }
    }
}
