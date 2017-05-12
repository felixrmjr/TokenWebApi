using System.Data.Entity.ModelConfiguration;
using TokenINFRA.Entidades;

namespace TokenINFRA.Mapeamento
{
    public class MusicaMap : EntityTypeConfiguration<Musica>
    {
        public MusicaMap()
        {
            // Tabela
            ToTable("Musica");

            // Chave primária
            HasKey(k => k.Id);

            // Coluna: Nome - Máx 30 caracteres | NOT NULL| Order: 1
            Property(x => x.Nome).HasMaxLength(30).IsRequired().HasColumnOrder(1);

            // Coluna: Status - Máx 30 caracteres | NOT NULL| Order: 2
            Property(x => x.DataLancamento).IsRequired().HasColumnOrder(2);

            // Coluna: Nome - Máx 30 caracteres | NOT NULL| Order: 1
            Property(x => x.Filme).HasMaxLength(30).IsRequired().HasColumnOrder(3);
        }
    }
}
