namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassNuevosRequire2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Venta", "provincia", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Venta", "provincia", c => c.String(nullable: false));
        }
    }
}
