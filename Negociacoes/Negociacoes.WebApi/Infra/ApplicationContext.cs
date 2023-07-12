using Negociacoes.WebApi.Infra.Maps;
using Negociacoes.WebApi.Models.Entities;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Negociacoes.WebApi.Infra
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<ApplicationContext>(null);
        }

        protected ApplicationContext(DbConnection dbConnection) : base(dbConnection, true)
        {
            Database.SetInitializer<ApplicationContext>(null);
        }

        public DbSet<ComposicaoCarga> ComposicaoCarga { get; set; }
        public DbSet<NegociacaoComposicaoCarga> NegociacaoComposicaoCarga { get; set; }
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<Sugestao> Sugestao { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new ComposicaoCargaMap());
            modelBuilder.Configurations.Add(new NegociacaoComposicaoCargaMap());
            modelBuilder.Configurations.Add(new PedidoMap());
            modelBuilder.Configurations.Add(new SugestaoMap());
            modelBuilder.Configurations.Add(new UsuarioMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
