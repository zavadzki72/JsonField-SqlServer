using Negociacoes.WebApi.Enumeradores;

namespace Negociacoes.WebApi.Models.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
