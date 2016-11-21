using SGITO_OBRAS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGITO_OBRAS.Util
{
    public class DesplegablesUtil
    {
        IUnitOfWork unitOfWork;
        public DesplegablesUtil() : this(new UnitOfWork()) { }
        public DesplegablesUtil(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }
        public IEnumerable<SelectListItem> desplegableTipo(String value = null)
        {
            List<object> list = new List<object>();
            list.Add("Departamento");
            list.Add("Casa");
            list.Add("Duplex");
            list.Add("Cabaña");

            IEnumerable<object> en = list;

            return
            en
            .Select(t =>
            new SelectListItem
            {
                Selected = (t.ToString() == value),
                Text = t.ToString(),
                Value = t.ToString()
            });
        }

        public IEnumerable<SelectListItem> provincias(String value = null)
        {
            List<object> list = new List<object>();
            list.Add("Buenos Aires");
            list.Add("Catamarca");
            list.Add("Chaco");
            list.Add("Chubut");
            list.Add("Córdoba");
            list.Add("Corrientes");
            list.Add("Entre Ríos");
            list.Add("Formosa");
            list.Add("Jujuy");
            list.Add("La Pampa");
            list.Add("La Rioja");
            list.Add("Mendoza");
            list.Add("Misiones");

            IEnumerable<object> en = list;

            return
            en
            .Select(t =>
            new SelectListItem
            {
                Selected = (t.ToString() == value),
                Text = t.ToString(),
                Value = t.ToString()
            });
        }
        public IEnumerable<SelectListItem> desplegablePerfiles(int? value = null)
        {
            List<Perfil> list = new List<Perfil>();
            list = unitOfWork.PerfilRepository.All().ToList();

            IEnumerable<Perfil> ie = list;

            return
                ie
                .Select(p =>
                new SelectListItem
                {
                    Selected = (p.perfilId == value),
                    Text = p.descripcion,
                    Value = p.perfilId.ToString()
                });
        }
        
        public IEnumerable<SelectListItem> desplegableAdjuntos(string adjuntoIdStr = null)
        {
            List<int> adjuntoId = new List<int>();
            if (!String.IsNullOrEmpty(adjuntoIdStr))
            {
                char[] splitchar = { ';' };
                var idAdj = adjuntoIdStr.Split(splitchar);
                foreach (var id in idAdj)
                {
                    var idInt = Int32.Parse(id);
                    adjuntoId.Add(idInt);
                }
            }
            else
            {
                adjuntoId = null;
            }
            List<Adjuntos> list = new List<Adjuntos>();
            if (adjuntoId != null)
            {
                list = unitOfWork.AdjuntosRepository.Filter(x => adjuntoId.Contains(x.AdjuntosId)).ToList();
            }
            IEnumerable<Adjuntos> enAdj = list;

            return
            enAdj
            .Select(t =>
            new SelectListItem
            {
                Text = t.nombreAdjunto,
                Value = t.nombreAdjunto
            });
        }
        /*
        public IEnumerable<SelectListItem> desplegableProvincias(int? value = null)
        {
            List<Provincia> list = unitOfWork.ProvinciaRepository.All().ToList();

            IEnumerable<Provincia> en = list;

            return
            en
            .Select(t =>
            new SelectListItem
            {
                Selected = (t.provinciaId == value),
                Text = t.descripcion,
                Value = t.provinciaId.ToString()
            });
        }
        
        public IEnumerable<SelectListItem> desplegableEmpresas(int[] sector = null, string value = null)
        {
            List<Contratista> conList = new List<Contratista>();
            List<Subcontratista> subList = new List<Subcontratista>();
            if (sector != null)
            {
                conList = unitOfWork.ContratistaSectorRepository.Filter(x => sector.Contains(x.sectorId)).Select(x => x.contratista).Distinct().ToList();
                subList = unitOfWork.SubcontratistaSectorRepository.Filter(x => sector.Contains(x.sectorId)).Select(x => x.subcontratista).Distinct().ToList();
            }
            IEnumerable<Contratista> enCon = conList;
            IEnumerable<Subcontratista> enSub = subList;

            IEnumerable<SelectListItem> ie = null;

            ie = enCon
                .Select(p =>
                new SelectListItem
                {
                    Selected = ("contratista-" + p.contratistaId.ToString() == value),
                    Text = p.descripcion + " (Contratista)",
                    Value = "contratista-" + p.contratistaId.ToString()
                });

            ie = ie.Union(
                    enSub
                    .Select(p =>
                    new SelectListItem
                    {
                        Selected = ("subcontratista-" + p.contratistaId.ToString() == value),
                        Text = p.descripcion + " (Subcontratista)",
                        Value = "subcontratista-" + p.contratistaId.ToString()
                    })
                );

            return ie;
        }
        public MultiSelectList desplegableSector(int usuarioId, List<int> value = null)
        {
            List<Sector> sectorList = unitOfWork.UsuarioSectorRepository.Filter(x => x.usuarioId == usuarioId).Select(x => x.sector).ToList();
            IEnumerable<Sector> enSector = sectorList;

            return new MultiSelectList(enSector, "sectorId", "descripcion", value);
        }
        public MultiSelectList desplegableContratistasMulti(int sectorId, List<int> value = null)
        {
            List<Contratista> list = unitOfWork.ContratistaSectorRepository.Filter(x => x.sectorId == sectorId).Select(x => x.contratista).ToList();
            IEnumerable<Contratista> enCon = list;

            return new MultiSelectList(enCon, "contratistaId", "descripcion", value);
        }
        public MultiSelectList desplegableSubcontratistasMulti(int sectorId, List<int> contratistaIdList = null, List<int> value = null)
        {
            List<Subcontratista> list = new List<Subcontratista>();
            if (contratistaIdList != null)
            {
                list = unitOfWork.SubcontratistaSectorRepository.Filter(x => x.sectorId == sectorId && contratistaIdList.Contains(x.subcontratista.contratistaId)).Select(x => x.subcontratista).ToList();
            }
            IEnumerable<Subcontratista> enSub = list;

            return new MultiSelectList(enSub, "subcontratistaId", "descripcion", value);
        }
        public IEnumerable<SelectListItem> desplegableAdjuntos(string adjuntoIdStr = null)
        {
            List<int> adjuntoId = new List<int>();
            if (!String.IsNullOrEmpty(adjuntoIdStr))
            {
                char[] splitchar = { ';' };
                var idAdj = adjuntoIdStr.Split(splitchar);
                foreach (var id in idAdj)
                {
                    var idInt = Int32.Parse(id);
                    adjuntoId.Add(idInt);
                }
            }
            else
            {
                adjuntoId = null;
            }
            List<Archivo> list = new List<Archivo>();
            if (adjuntoId != null)
            {
                list = unitOfWork.ArchivoRepository.Filter(x => adjuntoId.Contains(x.archivoId)).ToList();
            }
            IEnumerable<Archivo> enAdj = list;

            return
            enAdj
            .Select(t =>
            new SelectListItem
            {
                Text = t.nombreArchivo,
                Value = t.archivoId.ToString()
            });
        }
        public IEnumerable<SelectListItem> desplegableTipoObra(int sectorId, int? value = null)
        {
            var list = unitOfWork.TipoObraRepository.Filter(x => x.sectorId == sectorId)
                                                    .Select(x => new { tipoObraId = x.tipoObraId, descripcion = x.descripcion, grupo = x.tipoObraGrupo.descripcion == null ? "" : x.tipoObraGrupo.descripcion })
                                                    .OrderBy(x => x.grupo).ThenBy(x => x.descripcion).ToList();
            
            return new SelectList(list, "tipoObraId", "descripcion", "grupo", value);
        }
        public MultiSelectList desplegableUsuarios(int sector, List<int> value = null)
        {
            List<UsuarioSector> UsuariosList = unitOfWork.UsuarioSectorRepository.Filter(x => x.sectorId == sector).ToList();
            IEnumerable<UsuarioSector> enUsuarios = UsuariosList;
            List<Usuario> UsuariosLista = new List<Usuario>();
            foreach (var sp in enUsuarios)
            {
                UsuariosLista.Add(sp.usuario);
            }
            IEnumerable<Usuario> enUsuariosList = UsuariosLista;
            return new MultiSelectList(enUsuariosList, "UsuarioId", "nombreApellido", value);
        }
        public IEnumerable<SelectListItem> desplegableRepresentatesTASA(int? obraId, int? value = null)
        {
            List<Usuario> list = unitOfWork.UsuarioObraRepository.Filter(x => x.obraId == obraId).Select(x => x.usuario).ToList();
            IEnumerable<Usuario> enUsr = list;

            return
                enUsr
                    .Select(t =>
                    new SelectListItem
                    {
                        Selected = (t.usuarioId == value),
                        Text = t.nombreApellido,
                        Value = t.usuarioId.ToString()
                    });
        }
        public IEnumerable<SelectListItem> desplegableEstadosInstalacion(int? value = null)
        {
            List<Estado> list = unitOfWork.EstadoRepository.Filter(x => x.descripcion == "En proceso de instalacion" || x.descripcion == "Finalizado con reparos" || x.descripcion == "Finalizado con pendientes menores" || x.descripcion == "Finalizado sin pendientes").ToList();
            IEnumerable<Estado> enEstado = list;

            return
                enEstado
                    .Select(t =>
                    new SelectListItem
                    {
                        Selected = (t.estadoId == value),
                        Text = t.descripcion,
                        Value = t.estadoId.ToString()
                    });
        } */
    }
}
