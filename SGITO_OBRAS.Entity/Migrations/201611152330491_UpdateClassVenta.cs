namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClassVenta : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Venta", "usuarioId", "dbo.Usuario");
            DropIndex("dbo.Venta", new[] { "usuarioId" });
            AlterColumn("dbo.Venta", "usuarioId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Venta", "usuarioId", c => c.Int());
            CreateIndex("dbo.Venta", "usuarioId");
            AddForeignKey("dbo.Venta", "usuarioId", "dbo.Usuario", "usuarioId");
        }
    }
}
