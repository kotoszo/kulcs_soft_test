using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    interface IDataService
    {
        bool InsertUser(string name, string email);
        bool DeletUser(int id);
        DataTable GetUsers();
    }
}
