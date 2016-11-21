namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassMensajes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mensajes",
                c => new
                    {
                        MensajesId = c.Int(nullable: false, identity: true),
                        mensaje = c.String(),
                        Usuario = c.Int(nullable: false),
                        ventaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MensajesId)
                .ForeignKey("dbo.Venta", t => t.ventaId)
                .Index(t => t.ventaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Mensajes", "ventaId", "dbo.Venta");
            DropIndex("dbo.Mensajes", new[] { "ventaId" });
            DropTable("dbo.Mensajes");
        }
    }
}
