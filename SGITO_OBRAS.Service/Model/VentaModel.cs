using SGITO_OBRAS.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Service.Model
{
    public class VentaModel
    {
        public Venta venta { set; get; }
        public string strAdjId { get; set; }
        public int idVent { set; get; }
        public List<int> adjuntoIdList { get; set; }
        public List<string> desplegableTipo { get; set; }
        public List<string> provincias { get; set; }

    }
}
