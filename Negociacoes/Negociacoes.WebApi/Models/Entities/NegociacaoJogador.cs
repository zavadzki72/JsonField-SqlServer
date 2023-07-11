using Negociacoes.WebApi.Enumeradores;

namespace Negociacoes.WebApi.Models.Entities
{
    public class NegociacaoJogador
    {
        public int Id { get; set; }
        public DateTime DataEvento { get; set; }
        public TipoNegociacaoJogador TipoNegociacaoJogador { get; set; }
        public DateTime DataContratoProposta { get; set; }
        public string Observacoes { get; set; }
        public int IdTimeDestino { get; set; }
        public string JogadoresAtuais { get; set; }
        public string JogadoresNovos { get; set; }
        public string JogadoresRemovidos { get; set; }

        public virtual Time TimeDestino { get; set; }
    }
}
