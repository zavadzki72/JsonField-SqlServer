namespace Negociacoes.WebApi.Models.Dtos
{
    public class RegisterPedidoDto
    {
        public DateTime DataEntrega { get; set; }
        public decimal Quantidade { get; set; }
        public string Item { get; set; }
    }
}
