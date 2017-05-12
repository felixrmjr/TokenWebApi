using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TokenINFRA.Entidades;

namespace TokenINFRA.Mapeamento
{
    public class TokenMap : EntityTypeConfiguration<Token>
    {
        public TokenMap()
        {
            // Tabela
            ToTable("Token");

            // Chave primária
            HasKey(k => k.Id);

            // Coluna: Nome - Máx 100 caracteres | NOT NULL| Order: 1
            Property(x => x.TokenStr).HasMaxLength(30).IsRequired().HasColumnOrder(1);

            // Coluna: Status - NOT NULL| Order: 2
            Property(x => x.Emissao).IsRequired().HasColumnOrder(2);

            // Coluna: Email -  NOT NULL| Order: 3
            Property(x => x.Expiracao).IsRequired().HasColumnOrder(3);

            // Coluna: Status - NOT NULL| Order: 4
            Property(x => x.CriadoEm).IsOptional().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed).HasColumnOrder(4);
        }
    }
}
