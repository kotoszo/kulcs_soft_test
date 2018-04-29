using System.Data.SqlClient;
using System.Data;

namespace DataService
{
    public class MsSql : IDbService
    {
        private SqlConnection connection;

        public MsSql()
        {
            connection = new SqlConnection("Server=localhost;Database=master;Trusted_Connection=True;");
            TableInit();
        }

        private void TableInit()
        {
            string select = "SELECT * FROM Users;";
            if (!Execute(GetCommand(select)))
            {
                string query = "CREATE TABLE Users(id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, name varchar(50), email varchar(50));";
                Execute(GetCommand(query));
            }
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM Users WHERE id = @id;";
            SqlCommand command = GetCommand(query);
            command.Parameters.AddWithValue("@id", id);
            return Execute(command);
        }

        public DataTable GetAll()
        {
            string query = "SELECT * FROM Users;";
            SqlCommand command = GetCommand(query);
            return Execute(command, new DataTable());
        }

        public bool Insert(string name, string email)
        {
            string query = "INSERT INTO Users (name, email) VALUES(@name, @email);";
            SqlCommand command = GetCommand(query);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("email", email);
            return Execute(command);
        }
        private SqlCommand GetCommand(string query)
        {
            return new SqlCommand
            {
                CommandText = query,
                Connection = connection
            };
        }
        private bool Execute(SqlCommand command)
        {
            int? howMany;
            try
            {
                command.Connection.Open();
                howMany = command.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                howMany = null;
            }
            finally
            {
                command.Connection.Close();
                command.CommandText = null;
            }
            return howMany != null;
        }
        private DataTable Execute(SqlCommand command, DataTable table)
        {
            SqlDataAdapter adapter;
            try
            {
                command.Connection.Open();
                adapter = new SqlDataAdapter(command);
                table = new DataTable();
                adapter.Fill(table);
            }
            catch (SqlException)
            {
                adapter = null;
            }
            finally
            {
                command.Connection.Close();
                command.CommandText = null;
            }
            return table;
        }
    }
}
