namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassPerfil : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        perfilId = c.Int(nullable: false, identity: true),
                        descripcion = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.perfilId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Perfil");
        }
    }
}
