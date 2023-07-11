namespace Negociacoes.WebApi.Models.Dtos
{
    public class PostNegociacaoJogadorDto
    {
        public DateTime DataContratoProposta { get; set; }
        public string Observacoes { get; set; }
        public int IdTimeDestino { get; set; }
        public List<JogadorDto> JogadoresAtuais { get; set; }
        public List<int> JogadoresNovos { get; set; }
        public List<int> JogadoresRemovidos { get; set; }
    }

    public class JogadorDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Salario { get; set; }
    }
}
