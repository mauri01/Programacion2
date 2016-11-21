namespace SGITO_OBRAS.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClassPerfilPermiso : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PerfilPermiso",
                c => new
                    {
                        perfilPermisoId = c.Int(nullable: false, identity: true),
                        perfilId = c.Int(nullable: false),
                        permisoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.perfilPermisoId)
                .ForeignKey("dbo.Perfil", t => t.perfilId)
                .ForeignKey("dbo.Permiso", t => t.permisoId)
                .Index(t => t.perfilId)
                .Index(t => t.permisoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PerfilPermiso", "permisoId", "dbo.Permiso");
            DropForeignKey("dbo.PerfilPermiso", "perfilId", "dbo.Perfil");
            DropIndex("dbo.PerfilPermiso", new[] { "permisoId" });
            DropIndex("dbo.PerfilPermiso", new[] { "perfilId" });
            DropTable("dbo.PerfilPermiso");
        }
    }
}
