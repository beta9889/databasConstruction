using databasConstruction.HelperClasses;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace databasConstruction.Models
{
    public class DeerModel
    {
        
        public DataTable getAllDeer()
        {   
            var connection = HelperConnection.getConnection();
            connection.Open();
            var adapter = new MySqlDataAdapter("select deerName from ViewAllDeer",connection);
            
            return ;
        }
    }
}
