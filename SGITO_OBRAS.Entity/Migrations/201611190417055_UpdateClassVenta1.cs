namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClassVenta1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Venta", "provincia", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Venta", "provincia");
        }
    }
}
