using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SGITO_OBRAS.Entity
{
	public class SGITO_OBRASContext : DbContext
	{
		public SGITO_OBRASContext() : base("BigData")
		{
            //Database.SetInitializer<SGITO_OBRASContext>(new SGITO_OBRASInitializer());
		}

		//Tablas
		public DbSet<Perfil> Perfil { set; get; }
		public DbSet<Proceso> Proceso { set; get; }
		public DbSet<Permiso> Permiso { set; get; }
		public DbSet<PerfilPermiso> PerfilPermiso { set; get; }
		public DbSet<Usuario> Usuario { set; get; }
		public DbSet<Adjuntos> Adjuntos { set; get; }
		public DbSet<Venta> Venta { set; get; }
		public DbSet<Mensajes> Mensajes { set; get; }
		
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
			modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
             
		}
	}
}
