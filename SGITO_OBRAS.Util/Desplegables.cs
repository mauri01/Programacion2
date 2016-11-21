using SGITO_OBRAS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGITO_OBRAS.Util
{
    public class Desplegables
    {
        IUnitOfWork unitOfWork;
        public Desplegables() : this(new UnitOfWork()) { }
        public Desplegables(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }
        public IEnumerable<SelectListItem> desplegableDNI(String value = null)
        {
            List<object> list = new List<object>();
            list.Add("DNI");
            list.Add("C.I");
            list.Add("L.C");

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
                Value = t.AdjuntosId.ToString()
            });
        }



    }
}
