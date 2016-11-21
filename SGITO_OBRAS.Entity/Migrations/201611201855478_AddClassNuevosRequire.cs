namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassNuevosRequire : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Venta", "tipo", c => c.String(nullable: false));
            AlterColumn("dbo.Venta", "provincia", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Venta", "provincia", c => c.String());
            AlterColumn("dbo.Venta", "tipo", c => c.String());
        }
    }
}
