namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassUsuario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        usuarioId = c.Int(nullable: false, identity: true),
                        usuarioRed = c.String(nullable: false, maxLength: 50),
                        dominio = c.String(nullable: false, maxLength: 50),
                        nombreApellido = c.String(nullable: false, maxLength: 100),
                        perfilId = c.Int(nullable: false),
                        email = c.String(),
                        tipoDocumento = c.String(maxLength: 10),
                        nroDocumento = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.usuarioId)
                .ForeignKey("dbo.Perfil", t => t.perfilId)
                .Index(t => t.perfilId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario", "perfilId", "dbo.Perfil");
            DropIndex("dbo.Usuario", new[] { "perfilId" });
            DropTable("dbo.Usuario");
        }
    }
}
