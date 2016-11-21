using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SGITO_OBRAS.Entity.Repository.Interface;

namespace SGITO_OBRAS.Entity
{
	public interface IUnitOfWork
	{
		IRepository<Perfil> PerfilRepository {get;}
		IRepository<Proceso> ProcesoRepository {get;}
		IRepository<Permiso> PermisoRepository {get;}
		IRepository<PerfilPermiso> PerfilPermisoRepository {get;}
		IRepository<Usuario> UsuarioRepository {get;}
		IRepository<Adjuntos> AdjuntosRepository {get;}
		IRepository<Venta> VentaRepository {get;}
		IRepository<Mensajes> MensajesRepository {get;}
				int Save();
	}
}