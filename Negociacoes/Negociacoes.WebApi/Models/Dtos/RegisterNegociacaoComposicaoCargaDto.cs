using Negociacoes.WebApi.Enumeradores;

namespace Negociacoes.WebApi.Models.Dtos
{
    public class RegisterNegociacaoComposicaoCargaDto
    {
        public DateTime DataEvento { get; set; }
        public int IdComposicaoCarga { get; set; }
        public string Observacao { get; set; }
        public TipoNegociacaoComposicaoCarga TipoNegociacao { get; set; }
        public TipoUsuario TipoUsuarioResponsavelProximaEtapa { get; set; }
        public List<NegociacaoPedidoDto> PedidosAtuais { get; set; }
        public List<int> PedidosNovos { get; set; }
        public List<int> PedidosRemovidos { get; set; }
        public List<int> SugestoesNovas { get; set; }
    }
}
