using Negociacoes.WebApi.Models.Entities;
using Newtonsoft.Json;

namespace Negociacoes.WebApi.Models.Dtos
{
    public class NegociacaoJogadorDto
    {
        public int Id { get; set; }
        public DateTime DataContratoProposta { get; set; }
        public string Observacoes { get; set; }
        public Time TimeDestino { get; set; }
        public List<JogadorDto> JogadoresAtuais { get; set; }
        public List<int> JogadoresNovos { get; set; }
        public List<int> JogadoresRemovidos { get; set; }

        public static NegociacaoJogadorDto GetFromNegociacaoJogador(NegociacaoJogador negociacaoJogador)
        {
            var jogadoresAtuais = JsonConvert.DeserializeObject<List<JogadorDto>>(negociacaoJogador.JogadoresAtuais);
            var jogadoresNovos = JsonConvert.DeserializeObject<List<int>>(negociacaoJogador.JogadoresNovos);
            var jogadoresRemovidos = JsonConvert.DeserializeObject<List<int>>(negociacaoJogador.JogadoresRemovidos);

            return new NegociacaoJogadorDto
            {
                Id = negociacaoJogador.Id,
                DataContratoProposta = negociacaoJogador.DataContratoProposta,
                Observacoes = negociacaoJogador.Observacoes,
                TimeDestino = negociacaoJogador.TimeDestino,
                JogadoresAtuais = jogadoresAtuais,
                JogadoresNovos = jogadoresNovos,
                JogadoresRemovidos = jogadoresRemovidos
            };
        }
    }
}
