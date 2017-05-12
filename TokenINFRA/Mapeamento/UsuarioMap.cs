using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TokenINFRA.Entidades;

namespace TokenINFRA.Mapeamento
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            // Tabela
            ToTable("Usuario");

            // Chave primária
            HasKey(k => k.Id);

            // Coluna: Nome - Máx 30 caracteres | NOT NULL| Order: 1
            Property(x => x.Nome).HasMaxLength(30).IsRequired().HasColumnOrder(1);

            // Coluna: Status - Máx 30 caracteres | NOT NULL| Order: 2
            Property(x => x.Senha).HasMaxLength(30).IsRequired().HasColumnOrder(2);

            // Coluna: Email - Máx 50 caracteres | NOT NULL| Order: 3
            Property(x => x.Email).HasMaxLength(30).IsRequired().HasColumnOrder(3);

            // Coluna: Status - NOT NULL| Order: 4
            Property(x => x.CriadoEm).IsOptional().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed).HasColumnOrder(4);
        }
    }
}