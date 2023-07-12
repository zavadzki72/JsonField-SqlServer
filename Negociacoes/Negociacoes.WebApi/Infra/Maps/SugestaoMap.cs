using Negociacoes.WebApi.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Negociacoes.WebApi.Infra.Maps
{
    public class SugestaoMap : EntityTypeConfiguration<Sugestao>
    {
        public SugestaoMap()
        {
            ToTable("sugestao");

            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Status).IsRequired();
            Property(x => x.Quantidade).IsRequired();
            Property(x => x.Item).IsRequired();
            Property(x => x.DataEntrega).IsRequired();
        }
    }
}
