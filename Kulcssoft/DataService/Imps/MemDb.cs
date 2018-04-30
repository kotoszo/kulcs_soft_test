using System;
using System.Data;

namespace DataService
{
    public class MemDb : IDbService
    {
        private DataTable table;

        public MemDb()
        {
            TableInit();
        }

        private void TableInit()
        {
            string[] columns = new string[] { "Id", "Name", "Email" };
            table = new DataTable("Users");
            foreach (var column in columns)
            {
                table.Columns.Add(column);
            }
            table.PrimaryKey = new DataColumn[] { table.Columns[0] };
            table.Columns[2].Unique = true;
            AddUsers();
        }

        private void AddUsers()
        {
            string[] names = new string[] { "Erno Kiss", "Lajos Kovats" };
            string[] emails = new string[] { "email@gmail.com", "notascam@gmail.com" };
            for (int i = 0; i < names.Length; i++)
            {
                DataRow row = table.NewRow();
                row["Id"] = i;
                row["Name"] = names[i];
                row["Email"] = emails[i];
                table.Rows.Add(row);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                string query = "Id = " + id;
                table.Rows.Remove(table.Select(query)[0]);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong with the remove");
                Console.WriteLine(e);
            }
            return false;
        }

        public DataTable GetAll()
        {
            return table;
        }

        public bool Insert(string name, string email)
        {
            try
            {
                DataRow row = table.NewRow();
                DataRow lastRow = table.Rows[table.Rows.Count - 1];
                int lastId = int.Parse(lastRow["Id"].ToString());
                row["Id"] = lastId+1;
                row["Name"] = name;
                row["Email"] = email;
                table.Rows.Add(row);
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong with the insert");
                Console.WriteLine(e);
            }
            return false;
        }

        public DataRow GetUser(int id)
        {
            string query = "Id = " + id;
            DataRow[] row = table.Select(query);
            if (row.Length > 0)
            {
                return row[0];
            }
            return null;
        }

        public DataRow GetUser(string email)
        {
            string query = "Email = '" + email+"'";
            DataRow[] row = table.Select(query);
            if(row.Length > 0)
            {
                return row[0];
            }
            return null;
        }
    }
}