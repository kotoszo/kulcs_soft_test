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
        private void DropTable()
        {
            string query = "DROP TABLE Users";
            Execute(GetCommand(query));
        }
        private void TableInit()
        {
            string select = "SELECT * FROM Users;";
            if (!Execute(GetCommand(select)))
            {
                string query = "CREATE TABLE Users(id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, name varchar(50) NOT NULL, email varchar(50) NOT NULL UNIQUE);";
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
            catch (SqlException e)
            {
                System.Console.WriteLine(e);
                howMany = null;
            }
            finally
            {
                command.Connection.Close();
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
                adapter.Fill(table);
            }
            catch (SqlException e)
            {
                System.Console.WriteLine(e);
                adapter = null;
            }
            finally
            {
                command.Connection.Close();
            }
            return table;
        }

        public DataRow GetUser(int id)
        {
            string query = "SELECT * FROM Users WHERE id = @id";
            SqlCommand command = GetCommand(query);
            command.Parameters.AddWithValue("@id", id);
            var rows = Execute(command, new DataTable()).Rows;
            if(rows.Count > 0)
            {
                return rows[0];
            }
            return null;
        }

        public DataRow GetUser(string email)
        {
            string query = "SELECT * FROM Users WHERE email = @email";
            SqlCommand command = GetCommand(query);
            command.Parameters.AddWithValue("@email", email);
            var rows = Execute(command, new DataTable()).Rows;
            if (rows.Count > 0)
            {
                return rows[0];
            }
            return null;
        }
    }
}
