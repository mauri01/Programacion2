namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClassProceso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Proceso", "nombre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Proceso", "nombre");
        }
    }
}
