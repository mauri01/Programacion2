namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClassAdjunto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adjuntos", "ventaId", c => c.Int());
            CreateIndex("dbo.Adjuntos", "ventaId");
            AddForeignKey("dbo.Adjuntos", "ventaId", "dbo.Venta", "ventaId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Adjuntos", "ventaId", "dbo.Venta");
            DropIndex("dbo.Adjuntos", new[] { "ventaId" });
            DropColumn("dbo.Adjuntos", "ventaId");
        }
    }
}
