using Negociacoes.WebApi.Enumeradores;

namespace Negociacoes.WebApi.Models.Entities
{
    public class Sugestao
    {
        public int Id { get; set; }
        public DateTime DataEntrega { get; set; }
        public decimal Quantidade { get; set; }
        public string Item { get; set; }
        public StatusSugestao Status { get; set; }
    }
}
