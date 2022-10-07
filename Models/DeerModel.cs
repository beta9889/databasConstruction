using databasConstruction.HelperClasses;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace databasConstruction.Models
{
    public class DeerModel
    {
        
        public static DataTable? getAllDeer()
        {   
            var connection = HelperConnection.getConnection();
            connection.Open();
            var adapter = new MySqlDataAdapter("select deerName from ViewAllDeer",connection);
            DataSet dataSet = new();

            adapter.Fill(dataSet,"names");
            connection.Close();
            if(dataSet.Tables != null)
            {
                return (DataTable) dataSet.Tables["names"];
            }
            throw new Exception("waaaahhh");
        }
    }
}
