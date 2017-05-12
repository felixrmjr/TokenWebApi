using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using TokenINFRA.Entidades;
using TokenINFRA.Mapeamento;

namespace TokenINFRA.Contexto
{
    public class TokenContexto : DbContext
    {
        // COMANDOS DO MIGRATIONS
        // Seta Infra Default
        // Enable-Migrations
        // Add-Migration v1
        // Update-Database

        public TokenContexto() : base("name=DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Chave> Chaves { get; set; }
        public virtual DbSet<Empresa> Empresas { get; set; }
        public virtual DbSet<Musica> Musicas { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Configura todos as colunas do tipo string para serem por padrão "varchar"
            modelBuilder.Properties<string>().Configure(p => p.HasColumnType("varchar"));

            // Adicionando as configurações de mapeamento das tabelas
            modelBuilder.Configurations.Add(new ChaveMap());
            modelBuilder.Configurations.Add(new EmpresaMap());
            modelBuilder.Configurations.Add(new MusicaMap());
            modelBuilder.Configurations.Add(new TokenMap());
            modelBuilder.Configurations.Add(new UsuarioMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}