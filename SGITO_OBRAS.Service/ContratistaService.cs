using SGITO_OBRAS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SGITO_OBRAS.Service
{
    public class ContratistaService
    {
        IUnitOfWork unitOfWork;
        public ContratistaService(): this(new UnitOfWork()){ }
        public ContratistaService(IUnitOfWork uow)
        {
            unitOfWork = uow;
        }
        public IEnumerable<SelectListItem> GetContratistasDropDown(int? selectedId = 0)
        {
            return
               unitOfWork.ContratistaRepository.All().ToList().OrderBy(t => t.descripcion)
                      .Select(t =>
                          new SelectListItem
                          {
                              Selected = (t.contratistaId == selectedId),
                              Text = t.descripcion,
                              Value = t.contratistaId.ToString()
                          });
        }
    }
}
