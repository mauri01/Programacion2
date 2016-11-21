namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClassUsuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "pass", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "pass");
        }
    }
}
