namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClassVenta3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Venta", "nombreVenta", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Venta", "descripcion", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Venta", "descripcion", c => c.String());
            AlterColumn("dbo.Venta", "nombreVenta", c => c.String());
        }
    }
}
