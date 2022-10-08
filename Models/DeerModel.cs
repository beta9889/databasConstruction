using databasConstruction.HelperClasses;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace databasConstruction.Models
{
    public class DeerModel
    {
        
        public static List<string>? getAllDeer()
        {   
            var connection = HelperConnection.getConnection();
            connection.Open();
            var adapter = new MySqlDataAdapter("select deerName from ViewAllDeer",connection);
            DataSet dataSet = new();

            adapter.Fill(dataSet,"names");
            connection.Close();
            List<string> list = new();

            foreach(var bla in dataSet.Tables[0].AsEnumerable())
            {
                Console.WriteLine(bla["DeerName"].ToString());
                list.Add((string)bla["DeerName"]);
            }

            return list;
            throw new Exception("waaaahhh");
        }
    }
}
