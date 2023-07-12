using Negociacoes.WebApi.Enumeradores;

namespace Negociacoes.WebApi.Models.Entities
{
    public class ComposicaoCarga
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime DataEntrega { get; set; }
        public SituacaoComposicaoCarga Situacao { get; set; }
        public string Observacao { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
