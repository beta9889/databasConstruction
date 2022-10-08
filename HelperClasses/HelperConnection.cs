using MySql.Data.MySqlClient;
using System.Data;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace databasConstruction.HelperClasses
{
    public static class HelperConnection
    {
        private static IConfiguration configuration { get; set; }
        private static string username { get; set; }
        private static string password { get; set; }
        private static string connecitonString { get; set; }

        public static void setHelper(string username, string password)
        {
            HelperConnection.username = username;
            HelperConnection.password = password;
        }

        public static void SetConfiguration(IConfiguration config)
        {
            configuration = config;
        }

        public static MySqlConnection getConnection()
        {
            if(connecitonString == null)
            {
                connecitonString = configuration.GetValue<string>("ConnectionStrings");
                connecitonString = connecitonString + "User ID = " + username + ";";
                if(password != null)
                {
                    connecitonString = connecitonString + "Password=" + password + ";";
                }
            }
            var connection = new MySqlConnection(connecitonString);
            connection.Open();
            return connection;
        }

        public static List<T> ConvertSingleColumnList<T>(DataSet dataSet, string columnName)
        {
            if (dataSet.Tables != null)
            {
                List<T> result = new();
                foreach (var bla in dataSet.Tables[0].AsEnumerable())
                {
                    result.Add( (T) bla[columnName] );
                }
                return result;
            }
            else
            {
                return new List<T>();
            }
        }

        public static List<List<T>> ConvertDataSetToList<T>(DataSet dataSet, List<string> columnNames)
        {
            if (dataSet.Tables != null)
            {
                List<List<T>> result = new();
                foreach (var bla in dataSet.Tables[0].AsEnumerable())
                {
                    List<T> temp = new();
                    foreach (var c in columnNames)
                    {
                        temp.Add((T)bla[c]);
                    }
                    result.Add(temp);
                }
                return result;
            }
            else
            {
                return new List<List<T>>();
            }
        }

        public static List<T> ConvertRowToList<T>(DataSet dataSet, string columnName, int index)
        {
            if (dataSet.Tables != null)
            {
                List<T> result = new();
                foreach (var bla in dataSet.Tables[0].AsEnumerable())
                {
                    List<T> temp = new();
                    foreach (var c in columnName)
                    {
                        temp.Add((T)bla[c]);
                    }
                    result = temp;
                }
                return result;
            }
            else
            {
                return new List<T>();
            }
        }
    }
}
