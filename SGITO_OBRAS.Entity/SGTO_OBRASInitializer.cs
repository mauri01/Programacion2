using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGITO_OBRAS.Entity
{
    public class SGITO_OBRASInitializer: IDatabaseInitializer<SGITO_OBRASContext>
    {
        public bool nueva=false;
        public void InitializeDatabase(SGITO_OBRASContext context)
        {
            bool dbExists;
            dbExists = context.Database.Exists();
            if (dbExists)
            {
                try
                {
                    if (!context.Database.CompatibleWithModel(true))
                    {
                        throw new Exception("La base de datos existe y no es compatible...");
                    }
                }
                catch
                {
                    return;
                }
            }
            else
            {
                context.Database.Create();
                context.SaveChanges();
                nueva = true;
                return;
            }
            return;
        }

       
        public void CreateUser(SGITO_OBRASContext context)
        {
            
        }

        protected void Seed(SGITO_OBRASContext context)
        
        {

        }
    }
}