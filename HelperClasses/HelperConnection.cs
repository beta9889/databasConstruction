using MySql.Data.MySqlClient;
using System.Runtime.CompilerServices;

namespace databasConstruction.HelperClasses
{
    public static class HelperConnection
    {
        private static IConfiguration configuration { get; set; }
        private static string username { get; set; }
        private static string password { get; set; }
        private static string connecitonString { get; set; }

        public static void setHelper(string username, string password, IConfiguration config)
        {
            HelperConnection.username = username;
            HelperConnection.password = password;
            HelperConnection.configuration = config;
        }

        public static MySqlConnection getConnection()
        {
            if(connecitonString == null)
            {
                connecitonString = configuration.GetConnectionString("connectionString");
            }
            return new MySqlConnection(connecitonString);
        }

    }
}
