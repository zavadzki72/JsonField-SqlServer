using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Negociacoes.WebApi.Models.Entities;

namespace Negociacoes.WebApi.Infra.Maps
{
    public class NegociacaoJogadorMap : IEntityTypeConfiguration<NegociacaoJogador>
    {
        public void Configure(EntityTypeBuilder<NegociacaoJogador> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.IdComposicaoTime)
                .IsRequired();

            builder.HasOne(x => x.ComposicaoTime)
                .WithMany()
                .HasForeignKey(x => x.IdComposicaoTime);

            builder.OwnsOne(
                negociacao => negociacao.NegociacaoJogadorJson, ownedNavigationBuilder => 
                {
                    ownedNavigationBuilder.ToJson("negociacao_jogador_json");
                    ownedNavigationBuilder.OwnsMany(metadata => metadata.JogadoresAtuais).HasJsonPropertyName("jogadores_atuais");
                    ownedNavigationBuilder.OwnsMany(metadata => metadata.JogadoresNovos).HasJsonPropertyName("jogadores_novos");
                    ownedNavigationBuilder.OwnsMany(metadata => metadata.JogadoresRemovidos).HasJsonPropertyName("jogadores_removidos");
                }
            );
        }
    }
}
