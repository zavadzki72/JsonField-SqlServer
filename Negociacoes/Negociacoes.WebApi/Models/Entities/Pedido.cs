using Negociacoes.WebApi.Enumeradores;

namespace Negociacoes.WebApi.Models.Entities
{
    public class Pedido
    {
        public int Id { get; set; }
        public int? IdComposicaoCarga { get; set; }
        public DateTime DataEntrega { get; set; }
        public decimal Quantidade { get; set; }
        public string Item { get; set; }
        public StatusPedido Status { get; set; }

        public virtual ComposicaoCarga ComposicaoCarga { get; set; }
    }
}
