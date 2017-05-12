using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TokenINFRA.Entidades;

namespace TokenINFRA.Mapeamento
{
    public class ChaveMap : EntityTypeConfiguration<Chave>
    {
        public ChaveMap()
        {
            // Tabela
            ToTable("Chave");

            // Chave primária
            HasKey(k => k.Id);

            // Coluna: Status - NOT NULL| Order: 1
            Property(x => x.CriadoEm).IsOptional().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed).HasColumnOrder(1);
        }
    }
}
