using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGITO_OBRAS.Entity.Repository;
using SGITO_OBRAS.Entity.Repository.Interface;

namespace SGITO_OBRAS.Entity
{
	public class UnitOfWork: IUnitOfWork, IDisposable
	{
		private SGITO_OBRASContext context = new SGITO_OBRASContext();
						private Repository<Perfil> perfilRepository;
		public IRepository<Perfil> PerfilRepository
		{
			get
			{
				if (this.perfilRepository == null)
				{
					this.perfilRepository = new Repository<Perfil>(context);
				}
				return perfilRepository;
			}
		}
						private Repository<Proceso> procesoRepository;
		public IRepository<Proceso> ProcesoRepository
		{
			get
			{
				if (this.procesoRepository == null)
				{
					this.procesoRepository = new Repository<Proceso>(context);
				}
				return procesoRepository;
			}
		}
						private Repository<Permiso> permisoRepository;
		public IRepository<Permiso> PermisoRepository
		{
			get
			{
				if (this.permisoRepository == null)
				{
					this.permisoRepository = new Repository<Permiso>(context);
				}
				return permisoRepository;
			}
		}
						private Repository<PerfilPermiso> perfilPermisoRepository;
		public IRepository<PerfilPermiso> PerfilPermisoRepository
		{
			get
			{
				if (this.perfilPermisoRepository == null)
				{
					this.perfilPermisoRepository = new Repository<PerfilPermiso>(context);
				}
				return perfilPermisoRepository;
			}
		}
						private Repository<Usuario> usuarioRepository;
		public IRepository<Usuario> UsuarioRepository
		{
			get
			{
				if (this.usuarioRepository == null)
				{
					this.usuarioRepository = new Repository<Usuario>(context);
				}
				return usuarioRepository;
			}
		}
						private Repository<Adjuntos> adjuntosRepository;
		public IRepository<Adjuntos> AdjuntosRepository
		{
			get
			{
				if (this.adjuntosRepository == null)
				{
					this.adjuntosRepository = new Repository<Adjuntos>(context);
				}
				return adjuntosRepository;
			}
		}
						private Repository<Venta> ventaRepository;
		public IRepository<Venta> VentaRepository
		{
			get
			{
				if (this.ventaRepository == null)
				{
					this.ventaRepository = new Repository<Venta>(context);
				}
				return ventaRepository;
			}
		}
						private Repository<Mensajes> mensajesRepository;
		public IRepository<Mensajes> MensajesRepository
		{
			get
			{
				if (this.mensajesRepository == null)
				{
					this.mensajesRepository = new Repository<Mensajes>(context);
				}
				return mensajesRepository;
			}
		}
		
        public int Save()
		{
			return context.SaveChanges();
		}

		private bool disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

	}
}
