namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassVenta1 : DbMigration
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
                        usuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ventaId)
                .ForeignKey("dbo.Usuario", t => t.usuarioId)
                .Index(t => t.usuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Venta", "usuarioId", "dbo.Usuario");
            DropIndex("dbo.Venta", new[] { "usuarioId" });
            DropTable("dbo.Venta");
        }
    }
}
