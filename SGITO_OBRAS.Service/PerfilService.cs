using SGITO_OBRAS.Entity;
using SGITO_OBRAS.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Service
{
    public class PerfilService
    {
        IUnitOfWork unitOfWork;
        public PerfilService() : this(new UnitOfWork()) { }
        public PerfilService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }

        public string InsertPerfil(Perfil perfil)
        {
            try
            {
                var existe = unitOfWork.PerfilRepository.Find(x => x.descripcion== perfil.descripcion);
                if (existe == null)
                {
                    unitOfWork.PerfilRepository.Create(perfil);
                    unitOfWork.PerfilRepository.Save();
                    return "ok";
                }
                else
                {
                    return "Ya existe un Perfil con ese nombre";
                }
            }
            catch 
            {
                return "Error no Controlado. Comuniquese con el administrador.";
            }
        }

        public string UpdatePerfil(Perfil perfil)
        {
            try
            {
                var existe = unitOfWork.PerfilRepository.Find(x => x.descripcion == perfil.descripcion && x.perfilId != perfil.perfilId);
                if (existe == null)
                {
                    unitOfWork.PerfilRepository.Update(perfil);
                    unitOfWork.PerfilRepository.Save();
                    return "ok";
                }
                else
                {
                    return "Ya existe un Perfil con ese nombre";
                }
            }
            catch 
            {
                return "Error no Controlado. Comuniquese con el administrador.";
            }
        }

        public int Create(PerfilModel modeloPerfil)
        {
                try
                {
                    var existe = unitOfWork.PerfilRepository.Filter(x => x.descripcion == modeloPerfil.perfil.descripcion);
                    if (existe.Count() == 0)
                    {
                        Perfil perfil = new Perfil();
                        perfil.descripcion = modeloPerfil.perfil.descripcion;
                        unitOfWork.PerfilRepository.Create(perfil);
                        unitOfWork.PerfilRepository.Save();
                        foreach (var procesoFuncionalidad in modeloPerfil.procesoFuncionalidadList)
                        {
                            foreach (var permiso in procesoFuncionalidad.funcionalidadPermisoList)
                            {
                                if (permiso.valor == true)
                                {
                                    var permisoId = permiso.permiso.permisoId;
                                    PerfilPermiso perfilPermiso = new PerfilPermiso();
                                    perfilPermiso.perfilId = perfil.perfilId;
                                    perfilPermiso.permisoId = permisoId;
                                    unitOfWork.PerfilPermisoRepository.Create(perfilPermiso);
                                    unitOfWork.PerfilPermisoRepository.Save();
                                }
                            }
                        }
                        return 1;
                    }
                else { 
                        return 2;
                    }
                }
                catch
                {
                    return 0;
                }
        }

        public PerfilModel GetCreate()
        {
            PerfilModel modelo = new PerfilModel();
            var procesos = unitOfWork.ProcesoRepository.All();
            var funcionalidades = unitOfWork.PermisoRepository.Filter(x => procesos.Contains(x.proceso));
            modelo.procesoFuncionalidadList = new List<PerfilModel.ProcesoFuncionalidad>();
            foreach (var proceso in procesos)
            {
                PerfilModel.ProcesoFuncionalidad procesoFuncionalidad = new PerfilModel.ProcesoFuncionalidad();
                procesoFuncionalidad.proceso = proceso;
                procesoFuncionalidad.funcionalidadPermisoList = new List<PerfilModel.FuncionalidadPermiso>();
                var funcionalidadesProceso = funcionalidades.Where(x => x.proceso.procesoId == proceso.procesoId);
                foreach (var funcionalidad in funcionalidadesProceso)
                {
                    PerfilModel.FuncionalidadPermiso permisoFuncionalidad = new PerfilModel.FuncionalidadPermiso();
                    permisoFuncionalidad.permiso = funcionalidad;
                    procesoFuncionalidad.funcionalidadPermisoList.Add(permisoFuncionalidad);
                }
                modelo.procesoFuncionalidadList.Add(procesoFuncionalidad);
            }
            return modelo;
        }

        public PerfilModel GetEdit(int perfilId)
        {
            try
            {
                Perfil perfil = unitOfWork.PerfilRepository.Find(x => x.perfilId == perfilId);
                PerfilModel perfilModelo = new PerfilModel();
                if (perfil != null)
                {
                    perfilModelo.perfil = new Perfil();
                    perfilModelo.perfil.perfilId = perfil.perfilId;
                    perfilModelo.perfil.descripcion = perfil.descripcion;
                    var procesos = unitOfWork.ProcesoRepository.All();
                    var funcionalidades = unitOfWork.PermisoRepository.Filter(x => procesos.Contains(x.proceso));
                    int[] permisoId = unitOfWork.PerfilPermisoRepository.Filter(x => x.perfilId == perfil.perfilId).Select(x => x.permisoId).ToArray();
                    perfilModelo.procesoFuncionalidadList = new List<PerfilModel.ProcesoFuncionalidad>();
                    foreach (var proceso in procesos)
                    {
                        PerfilModel.ProcesoFuncionalidad procesoFuncionalidad = new PerfilModel.ProcesoFuncionalidad();
                        procesoFuncionalidad.proceso = proceso;
                        procesoFuncionalidad.funcionalidadPermisoList = new List<PerfilModel.FuncionalidadPermiso>();
                        var funcionalidadesProceso = funcionalidades.Where(x => x.proceso.procesoId == proceso.procesoId);
                        foreach (var funcionalidad in funcionalidadesProceso)
                        {
                            PerfilModel.FuncionalidadPermiso permisoFuncionalidad = new PerfilModel.FuncionalidadPermiso();
                            permisoFuncionalidad.permiso = funcionalidad;
                            permisoFuncionalidad.valor = permisoId.Contains(funcionalidad.permisoId) ? true : false;
                            procesoFuncionalidad.funcionalidadPermisoList.Add(permisoFuncionalidad);
                        }
                        perfilModelo.procesoFuncionalidadList.Add(procesoFuncionalidad);
                    }
                    return perfilModelo;
                }
                return perfilModelo;
            }
            catch
            {
                PerfilModel perfilModelo = new PerfilModel();
                return perfilModelo;
            }
        }
        public int Edit(PerfilModel modeloPerfil)
        {
             try
            {
                var existe = unitOfWork.PerfilRepository.Filter(x => x.descripcion == modeloPerfil.perfil.descripcion && x.perfilId != modeloPerfil.perfil.perfilId);
                if (existe.Count() == 0)
                {
                    Perfil perfil = unitOfWork.PerfilRepository.Find(x => x.perfilId == modeloPerfil.perfil.perfilId);
                    perfil.perfilId = modeloPerfil.perfil.perfilId;
                    perfil.descripcion = modeloPerfil.perfil.descripcion;
                    unitOfWork.PerfilRepository.Update(perfil);
                    unitOfWork.PerfilRepository.Save();
                    unitOfWork.PerfilPermisoRepository.Delete(x => x.perfilId == modeloPerfil.perfil.perfilId);
                    unitOfWork.PerfilPermisoRepository.Save();
                    foreach (var procesoFuncionalidad in modeloPerfil.procesoFuncionalidadList)
                    {
                        foreach (var permiso in procesoFuncionalidad.funcionalidadPermisoList)
                        {
                            if (permiso.valor == true)
                            {
                                var permisoId = permiso.permiso.permisoId;
                                PerfilPermiso perfilPermiso = new PerfilPermiso();
                                perfilPermiso.perfilId = perfil.perfilId;
                                perfilPermiso.permisoId = permisoId;
                                unitOfWork.PerfilPermisoRepository.Create(perfilPermiso);
                                unitOfWork.PerfilPermisoRepository.Save();
                            }
                        }
                    }
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            catch
            {
                return 0;
            }
        }

        public int Delete(int perfilId, int perfilId_usrLog)
        {
            try
            {
                if (perfilId != perfilId_usrLog)
                {
                    unitOfWork.PerfilPermisoRepository.Delete(x => x.perfilId == perfilId);
                    unitOfWork.PerfilPermisoRepository.Save();
                    unitOfWork.PerfilRepository.Delete(x => x.perfilId == perfilId);
                    unitOfWork.PerfilRepository.Save();
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            catch
            {
                return 0;
            }
        }


    }
}
