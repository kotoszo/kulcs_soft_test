using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public class DbService : IDataService
    {
        IDbService service;

        public DbService(IDbService service)
        {
            this.service = service;
        }

        public bool DeletUser(int id)
        {
            return service.Delete(id);
        }

        public DataTable GetUsers()
        {
            return service.GetAll();    
        }

        public bool InsertUser(string name, string email)
        {
            return service.Insert(name, email);
        }
    }
}
