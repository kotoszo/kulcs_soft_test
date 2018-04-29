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
