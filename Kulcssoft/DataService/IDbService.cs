using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataService
{
    public interface IDbService
    {
        DataTable GetAll();

        bool Delete(int id);

        bool Insert(string name, string email);
    }
}
