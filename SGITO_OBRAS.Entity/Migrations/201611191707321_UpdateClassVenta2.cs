namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClassVenta2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Venta", "mensajes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Venta", "mensajes");
        }
    }
}
