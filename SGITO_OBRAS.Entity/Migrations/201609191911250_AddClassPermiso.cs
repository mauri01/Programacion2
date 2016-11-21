namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassPermiso : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permiso",
                c => new
                    {
                        permisoId = c.Int(nullable: false, identity: true),
                        procesoId = c.Int(nullable: false),
                        funcionalidad = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.permisoId)
                .ForeignKey("dbo.Proceso", t => t.procesoId)
                .Index(t => t.procesoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Permiso", "procesoId", "dbo.Proceso");
            DropIndex("dbo.Permiso", new[] { "procesoId" });
            DropTable("dbo.Permiso");
        }
    }
}
