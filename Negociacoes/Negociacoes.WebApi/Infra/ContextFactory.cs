using System.Data.Entity.Infrastructure;
using System.Data.Entity;

namespace Negociacoes.WebApi.Infra
{
    public class ContextFactory : IDbContextFactory<ApplicationContext>
    {
        public ApplicationContext Create()
        {
            return new ApplicationContext("Data Source=localhost,5434;Initial Catalog=PedidosEFSeis;User ID=sa;Password=Pass@word;Connect Timeout=15;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultipleActiveResultSets=true;");
        }
    }
}
