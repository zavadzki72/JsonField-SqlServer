using Negociacoes.WebApi.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Negociacoes.WebApi.Infra.Maps
{
    public class ComposicaoCargaMap : EntityTypeConfiguration<ComposicaoCarga>
    {
        public ComposicaoCargaMap()
        {
            ToTable("composicao_carga");

            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            Property(x => x.DataEntrega).IsRequired();
            Property(x => x.Situacao).IsRequired();
            Property(x => x.Observacao).HasMaxLength(500);
            Property(x => x.IdUsuario).IsRequired();

            HasRequired(x => x.Usuario)
                .WithMany()
                .HasForeignKey(x => x.IdUsuario);
        }
    }
}
