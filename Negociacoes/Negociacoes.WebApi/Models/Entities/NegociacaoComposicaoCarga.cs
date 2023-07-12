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
        public NegociacaoComposicaoCargaJson? MetaData { get; set; }

        public virtual ComposicaoCarga ComposicaoCarga { get; set; }
    }

    public class NegociacaoComposicaoCargaJson
    {
        public List<NegociacaoPedidoJson> PedidosAtuais { get; set; }
        public List<IntPedido> PedidosNovos { get; set; }
        public List<IntPedido> PedidosRemovidos { get; set; }
        public List<IntSugestao> SugestoesNovas { get; set; }
    }

    public class NegociacaoPedidoJson
    {
        public int IdPedido { get; set; }
        public decimal Quantidade { get; set; }
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
