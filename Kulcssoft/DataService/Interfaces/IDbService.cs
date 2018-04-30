using System.Data;

namespace DataService
{
    public interface IDbService
    {
        DataRow GetUser(int id);

        DataRow GetUser(string email);

        DataTable GetAll();

        bool Delete(int id);

        bool Insert(string name, string email);
        
    }
}
