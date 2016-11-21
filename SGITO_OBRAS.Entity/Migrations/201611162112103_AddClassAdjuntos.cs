namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassAdjuntos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adjuntos",
                c => new
                    {
                        AdjuntosId = c.Int(nullable: false, identity: true),
                        tipo = c.String(),
                        nombreAdjunto = c.String(),
                        rutaAdjunto = c.String(),
                        usuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdjuntosId)
                .ForeignKey("dbo.Usuario", t => t.usuarioId)
                .Index(t => t.usuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Adjuntos", "usuarioId", "dbo.Usuario");
            DropIndex("dbo.Adjuntos", new[] { "usuarioId" });
            DropTable("dbo.Adjuntos");
        }
    }
}
