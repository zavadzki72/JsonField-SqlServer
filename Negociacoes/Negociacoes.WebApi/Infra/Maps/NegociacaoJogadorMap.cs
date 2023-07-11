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

            builder.Property(x => x.IdTimeDestino)
                .IsRequired();

            builder.Property(x => x.JogadoresAtuais)
                .HasJsonPropertyName("jogadores_atuais");

            builder.Property(x => x.JogadoresNovos)
                .HasJsonPropertyName("jogadores_novos");

            builder.Property(x => x.JogadoresRemovidos)
                .HasJsonPropertyName("jogadores_removidos");

            builder.HasOne(x => x.TimeDestino)
                .WithMany()
                .HasForeignKey(x => x.IdTimeDestino);
        }
    }
}
