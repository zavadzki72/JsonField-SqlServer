using Negociacoes.WebApi.Enumeradores;

namespace Negociacoes.WebApi.Models.Dtos
{
    public class RegisterNegociacaoComposicaoCargaDto
    {
        public List<NegociacaoPedidoDto> PedidosAtuais { get; set; }
        public List<int> PedidosNovos { get; set; }
        public List<int> PedidosRemovidos { get; set; }
        public List<int> SugestoesNovas { get; set; }
    }
}
