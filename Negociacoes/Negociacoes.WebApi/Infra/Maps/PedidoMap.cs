using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Negociacoes.WebApi.Models.Entities;

namespace Negociacoes.WebApi.Infra.Maps
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.IdComposicaoCarga);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Quantidade)
                .IsRequired();

            builder.Property(x => x.Item)
                .IsRequired();

            builder.Property(x => x.DataEntrega)
                .IsRequired();

            builder.HasOne(x => x.ComposicaoCarga)
                .WithMany()
                .HasForeignKey(x => x.IdComposicaoCarga);
        }
    }
}
