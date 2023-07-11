using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Negociacoes.WebApi.Models.Entities;

namespace Negociacoes.WebApi.Infra.Maps
{
    public class ComposicaoTimeMap : IEntityTypeConfiguration<ComposicaoTime>
    {
        public void Configure(EntityTypeBuilder<ComposicaoTime> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.SituacaoComposicaoTime)
                .IsRequired();
        }
    }
}
