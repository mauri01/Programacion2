namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassProceso : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Proceso",
                c => new
                    {
                        procesoId = c.Int(nullable: false, identity: true),
                        descripcion = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.procesoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Proceso");
        }
    }
}
