namespace TokenINFRA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chave",
                c => new
                    {
                        CriadoEm = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Empresa",
                c => new
                    {
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Descricao = c.String(nullable: false, maxLength: 50, unicode: false),
                        Encarregado = c.String(nullable: false, maxLength: 50, unicode: false),
                        CriadoEm = c.DateTime(),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                        Status = c.Boolean(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Musica",
                c => new
                    {
                        Nome = c.String(nullable: false, maxLength: 30, unicode: false),
                        DataLancamento = c.DateTime(nullable: false),
                        Filme = c.String(nullable: false, maxLength: 30, unicode: false),
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Token",
                c => new
                    {
                        TokenStr = c.String(nullable: false, maxLength: 30, unicode: false),
                        Emissao = c.DateTime(nullable: false),
                        Expiracao = c.DateTime(nullable: false),
                        CriadoEm = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Nome = c.String(nullable: false, maxLength: 30, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 30, unicode: false),
                        Email = c.String(nullable: false, maxLength: 30, unicode: false),
                        CriadoEm = c.DateTime(),
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Usuario");
            DropTable("dbo.Token");
            DropTable("dbo.Musica");
            DropTable("dbo.Empresa");
            DropTable("dbo.Chave");
        }
    }
}
