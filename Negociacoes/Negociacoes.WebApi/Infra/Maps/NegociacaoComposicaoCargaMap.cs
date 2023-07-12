using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Negociacoes.WebApi.Models.Entities;

namespace Negociacoes.WebApi.Infra.Maps
{
    public class NegociacaoComposicaoCargaMap : IEntityTypeConfiguration<NegociacaoComposicaoCarga>
    {
        public void Configure(EntityTypeBuilder<NegociacaoComposicaoCarga> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.IdComposicaoCarga)
                .IsRequired();

            builder.Property(x => x.Observacao);

            builder.Property(x => x.DataEvento)
                .IsRequired();

            builder.Property(x => x.TipoNegociacao)
                .IsRequired();

            builder.HasOne(x => x.ComposicaoCarga)
                .WithMany()
                .HasForeignKey(x => x.IdComposicaoCarga);

            builder.OwnsOne(
                negociacao => negociacao.MetaData, ownedNavigationBuilder => {
                    ownedNavigationBuilder.ToJson("meta_data");
                    ownedNavigationBuilder.OwnsMany(metadata => metadata.PedidosAtuais).HasJsonPropertyName("pedidos_atuais");
                    ownedNavigationBuilder.OwnsMany(metadata => metadata.PedidosNovos).HasJsonPropertyName("pedidos_novos");
                    ownedNavigationBuilder.OwnsMany(metadata => metadata.PedidosRemovidos).HasJsonPropertyName("pedidos_removidos");
                    ownedNavigationBuilder.OwnsMany(metadata => metadata.SugestoesNovas).HasJsonPropertyName("sugestoes_novas");
                    ownedNavigationBuilder.OwnsMany(metadata => metadata.SugestoesGeradasPorNegociacao).HasJsonPropertyName("sugestoes_geradas_por_negociacao");
                }
            );
        }
    }
}
