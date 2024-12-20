using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Club_App
{
    public class DataBase
    {
        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-04Q21HO6;Initial Catalog=Орлов_Д_419/4_УП;Integrated Security=True");
        public void openConnection() { if (connect.State == System.Data.ConnectionState.Closed) { connect.Open(); } }
        public void closeConnection() { if (connect.State == System.Data.ConnectionState.Open) { connect.Close(); } }
        public SqlConnection getConnection() { return connect; }

        public SqlCommand createCommand(string sql, SqlConnection connection)
        {
            // Создает команду для выполнения SQL-запроса
            return new SqlCommand(sql, connection);
        }
    }
}
