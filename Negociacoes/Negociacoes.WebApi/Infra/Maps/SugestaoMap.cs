using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Negociacoes.WebApi.Models.Entities;

namespace Negociacoes.WebApi.Infra.Maps
{
    public class SugestaoMap : IEntityTypeConfiguration<Sugestao>
    {
        public void Configure(EntityTypeBuilder<Sugestao> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Quantidade)
                .IsRequired();

            builder.Property(x => x.Item)
                .IsRequired();

            builder.Property(x => x.DataEntrega)
                .IsRequired();
        }
    }
}
