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
        public int IdComposicaoTime { get; set; }
        public NegociacaoJogadorJson NegociacaoJogadorJson { get; set; }

        public virtual ComposicaoTime ComposicaoTime { get; set; }
    }

    public class NegociacaoJogadorJson
    {
        public List<JogadorDataDto> JogadoresAtuais { get; set; }
        public List<IntData> JogadoresNovos { get; set; }
        public List<IntData> JogadoresRemovidos { get; set; }
    }

    public class JogadorDataDto
    {
        public int IdJogador { get; set; }
        public decimal Salario { get; set; }
    }

    public class IntData
    {
        public int IdJogador { get; set; }
    }
}
