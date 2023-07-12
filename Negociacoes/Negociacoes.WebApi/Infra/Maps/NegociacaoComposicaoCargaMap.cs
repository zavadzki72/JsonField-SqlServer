using Negociacoes.WebApi.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Negociacoes.WebApi.Infra.Maps
{
    public class NegociacaoComposicaoCargaMap : EntityTypeConfiguration<NegociacaoComposicaoCarga>
    {
        public NegociacaoComposicaoCargaMap()
        {
            ToTable("negociacao_composicao_carga");

            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.IdComposicaoCarga).IsRequired();
            Property(x => x.Observacao).IsOptional();
            Property(x => x.DataEvento).IsRequired();
            Property(x => x.TipoNegociacao).IsRequired();
            Property(x => x.MetaData).IsOptional();

            HasRequired(x => x.ComposicaoCarga)
                .WithMany()
                .HasForeignKey(x => x.IdComposicaoCarga);
        }
    }
}
