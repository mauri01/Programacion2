using SGITO_OBRAS.Entity;
using SGITO_OBRAS.Service.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Service
{
    public class ProcesamientoService
    {
        IUnitOfWork unitOfWork;
        private IUnitOfWork uow;


        public ProcesamientoService() : this(new UnitOfWork()) { }

        public ProcesamientoService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public ProcesamientoService(UnitOfWork uow)
        {
            this.unitOfWork = uow;
        }

        public List<LogMailModel> GetProcesamientoGrid()
        {
            List<LogMailModel> filtereMail = new List<LogMailModel>();
            DataTable dt = new DataTable();
            
             dt = SPProcesamientosGrid();
            
            foreach (DataRow item in dt.Rows)
            {
                LogMailModel o = new LogMailModel();
                o.logMailId = Int32.Parse(item["logMailId"].ToString());
                o.fuente = item["fuente"].ToString();
                o.carpeta = item["carpeta"].ToString();
                o.fecha = item["fecha"].ToString();
                o.status_procesamiento = item["status_procesamiento"].ToString();

                filtereMail.Add(o);
            }
            return filtereMail;
        }

        public DataTable SPProcesamientosGrid()
        {
            var conect = GetConnectionString("BigData");
            using (SqlConnection connection = new SqlConnection(conect))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("BUSQUEDA_VW_LOGMAIL", connection))
                {
                    //Agregar cualquier parámetro requerido, si es el caso:
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        DataTable result = new DataTable();
                        result.Load(r);
                        return result;
                    }
                }
            }
        }


        public string VW_Mail()
        {
            var conect = GetConnectionString("BigData");
            using (SqlConnection connection = new SqlConnection(conect))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("COUNTALL_VW_LOGMAIL", connection))
                {
                    //Agregar cualquier parámetro requerido, si es el caso:
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        DataTable result = new DataTable();
                        result.Load(r);
                        
                        var cant = result.Rows[0]["cantidad"].ToString();
                        return cant;
                    }
                }
            }
            
            
        }




        public string GetConnectionString(string dbName)
        {
            var conect = ConfigurationManager.ConnectionStrings[dbName].ConnectionString;
            return conect;
        }
    }
}
