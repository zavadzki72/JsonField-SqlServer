using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Negociacoes.WebApi.Models.Entities;

namespace Negociacoes.WebApi.Infra.Maps
{
    public class ComposicaoCargaMap : IEntityTypeConfiguration<ComposicaoCarga>
    {
        public void Configure(EntityTypeBuilder<ComposicaoCarga> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.DataEntrega)
                .IsRequired();

            builder.Property(x => x.Situacao)
                .IsRequired();

            builder.Property(x => x.Observacao)
                .HasMaxLength(500);

            builder.Property(x => x.IdUsuario)
                .IsRequired();

            builder.HasOne(x => x.Usuario)
                .WithMany()
                .HasForeignKey(x => x.IdUsuario);
        }
    }
}
