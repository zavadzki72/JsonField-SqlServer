using Negociacoes.WebApi.Models.Entities;

namespace Negociacoes.WebApi.Models.Dtos
{
    public class NegociacaoJogadorDto
    {
        public int Id { get; set; }
        public DateTime DataContratoProposta { get; set; }
        public string Observacoes { get; set; }
        public ComposicaoTime ComposicaoTime { get; set; }
        public List<JogadorDataDto> JogadoresAtuais { get; set; }
        public List<int> JogadoresNovos { get; set; }
        public List<int> JogadoresRemovidos { get; set; }

        public static NegociacaoJogadorDto GetFromNegociacaoJogador(NegociacaoJogador negociacaoJogador)
        {
            var jogadoresAtuais = negociacaoJogador.NegociacaoJogadorJson.JogadoresAtuais.Select(x => new JogadorDataDto { IdJogador = x.IdJogador, Salario = x.Salario }).ToList();
            var jogadoresNovos = negociacaoJogador.NegociacaoJogadorJson.JogadoresNovos.Select(x => x.IdJogador).ToList();
            var jogadoresRemovidos = negociacaoJogador.NegociacaoJogadorJson.JogadoresRemovidos.Select(x => x.IdJogador).ToList();

            return new NegociacaoJogadorDto
            {
                Id = negociacaoJogador.Id,
                DataContratoProposta = negociacaoJogador.DataContratoProposta,
                Observacoes = negociacaoJogador.Observacoes,
                ComposicaoTime = negociacaoJogador.ComposicaoTime,
                JogadoresAtuais = jogadoresAtuais,
                JogadoresNovos = jogadoresNovos,
                JogadoresRemovidos = jogadoresRemovidos
            };
        }
    }
}
