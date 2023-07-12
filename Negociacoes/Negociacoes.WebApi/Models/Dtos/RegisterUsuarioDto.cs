using Negociacoes.WebApi.Enumeradores;

namespace Negociacoes.WebApi.Models.Dtos
{
    public class RegisterUsuarioDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
