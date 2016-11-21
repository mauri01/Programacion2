namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassVenta : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Venta",
                c => new
                    {
                        ventaId = c.Int(nullable: false, identity: true),
                        nombreVenta = c.String(),
                        descripcion = c.String(),
                        precio = c.Int(nullable: false),
                        tipo = c.String(),
                        cantDormitorios = c.Int(nullable: false),
                        cantBaños = c.Int(nullable: false),
                        metrosCuadrados = c.Int(nullable: false),
                        usuario_usuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.ventaId)
                .ForeignKey("dbo.Usuario", t => t.usuario_usuarioId)
                .Index(t => t.usuario_usuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Venta", "usuario_usuarioId", "dbo.Usuario");
            DropIndex("dbo.Venta", new[] { "usuario_usuarioId" });
            DropTable("dbo.Venta");
        }
    }
}
