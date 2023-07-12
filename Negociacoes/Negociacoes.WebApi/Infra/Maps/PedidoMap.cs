using Negociacoes.WebApi.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Negociacoes.WebApi.Infra.Maps
{
    public class PedidoMap : EntityTypeConfiguration<Pedido>
    {
        public PedidoMap()
        {
            ToTable("pedido");

            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.IdComposicaoCarga).IsOptional();
            Property(x => x.Status).IsRequired();
            Property(x => x.Quantidade).IsRequired();
            Property(x => x.Item).IsRequired();
            Property(x => x.DataEntrega).IsRequired();

            HasOptional(x => x.ComposicaoCarga)
                .WithMany()
                .HasForeignKey(x => x.IdComposicaoCarga);
        }
    }
}
