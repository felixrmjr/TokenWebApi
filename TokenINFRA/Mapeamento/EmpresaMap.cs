using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using TokenINFRA.Entidades;

namespace TokenINFRA.Mapeamento
{
    public class EmpresaMap : EntityTypeConfiguration<Empresa>
    {
        public EmpresaMap()
        {
            // Tabela
            ToTable("Empresa");

            // Chave primária
            HasKey(k => k.Id);

            // Coluna: Nome - Máx 30 caracteres | NOT NULL| Order: 1
            Property(x => x.Nome).HasMaxLength(50).IsRequired().HasColumnOrder(1);

            // Coluna: Status - Máx 30 caracteres | NOT NULL| Order: 2
            Property(x => x.Descricao).HasMaxLength(50).IsRequired().HasColumnOrder(2);

            // Coluna: Email - Máx 50 caracteres | NOT NULL| Order: 3
            Property(x => x.Encarregado).HasMaxLength(50).IsRequired().HasColumnOrder(3);

            // Coluna: Status - NOT NULL| Order: 4
            Property(x => x.CriadoEm).IsOptional().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed).HasColumnOrder(4);

            // Coluna: Email - Máx 50 caracteres | NOT NULL| Order: 5
            Property(x => x.Email).HasMaxLength(50).IsRequired().HasColumnOrder(5);

            // Coluna: Status - NOT NULL| Order: 6
            Property(x => x.Status).IsRequired().HasColumnOrder(6);

            // // (FK:TarefaId 1:N) Uma empresa deve ter um usuário e um usuário pode ter várias empresas
            // HasRequired(x => x.Usuario).WithMany(x => x.Empresas).Map(m => m.MapKey("UsuarioId"));
        }
    }
}
