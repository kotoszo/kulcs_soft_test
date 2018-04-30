using System.Data;

namespace DataService
{
    public interface IDataService
    {
        bool InsertUser(string name, string email);
        bool DeletUser(int id);
        DataTable GetUsers();
        DataRow GetUser(int id);
        DataRow GetUser(string email);
    }
}
