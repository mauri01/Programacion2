using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Service.Model
{
    public class LogMailModel
    {
        public int logMailId { get; set; }
        public string fecha { get; set; }
        public string carpeta { get; set; }
        public string fuente { get; set; }
        public string status_procesamiento { get; set; }
    }
}
