namespace Negociacoes.WebApi.Models.Entities
{
    public class Time
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int? IdComposicaoTime { get; set; }

        public virtual ComposicaoTime? ComposicaoTime { get; set; }
    }
}
