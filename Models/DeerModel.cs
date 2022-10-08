using databasConstruction.HelperClasses;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System.Collections.Generic;
using System.Data;

namespace databasConstruction.Models
{
    public record DeerModel
    {
        public short DeerNr { get; set; }
        public string DeerName { get; set; }
        public string DeerGroup { get; set; }
        public string Smell { get; set; }
        public bool Retired { get; set; }

        public static List<DeerModel> GetAllDeers()
        {
            using (var connection = HelperConnection.getConnection())
            {
                var adapter = new MySqlDataAdapter("select DeerName,DeerGroup,retired,DeerNr,Smell from ViewAllDeer", connection);
                DataSet dataSet = new();

                adapter.Fill(dataSet, "names");
                connection.Close();
                List<DeerModel> result = new();

                foreach (var bla in dataSet.Tables[0].AsEnumerable())
                {
                    var deer = new DeerModel
                    {
                        DeerName = (string)bla["DeerName"],
                        DeerGroup = (string)bla["DeerGroup"],
                        DeerNr = (short)bla["DeerNr"],
                        Smell = (string)bla["Smell"]
                    };
                    deer.Retired = ((string)bla["retired"] != "Working");
                    result.Add(deer);
                }

                return result;
            }
        }

        public static DeerModel GetSpecificDeer(short id)
        {
            using (var connection = HelperConnection.getConnection())
            {
                MySqlDataAdapter adapter = new("select DeerName,DeerGroup,retired,DeerNr,Smell from ViewAllDeer Where DeerNr = @id;", connection);
                DataSet dataSet = new();

                adapter.Fill(dataSet, "names");
                connection.Close();
                List<DeerModel> result = new();

                foreach (var bla in dataSet.Tables[0].AsEnumerable())
                {
                    var deer = new DeerModel
                    {
                        DeerName = (string)bla["DeerName"],
                        DeerGroup = (string)bla["DeerGroup"],
                        DeerNr = (short)bla["DeerNr"],
                        Smell = (string)bla["Smell"]
                    };
                    deer.Retired = ((string)bla["retired"] != "Working");
                    result.Add(deer);
                }
                if (result.Count != 1) throw new Exception();
                return result[0];
            }
        }

        public static DeerModel test(short id)
        {
            DataSet table = new();
            using (var conn = HelperConnection.getConnection())
            using (var command = conn.CreateCommand())
            {
                command.CommandText = "select DeerName,DeerGroup,retired,DeerNr,Smell from ViewAllDeer Where DeerNr = @id;";
                command.Parameters.AddWithValue("@id", id);
                command.CommandType = CommandType.Text;

                MySqlDataAdapter adapter = new();
                adapter.SelectCommand = command;
                adapter.Fill(table);
                List<DeerModel> deerModels = new();
                foreach (var bla in table.Tables[0].AsEnumerable())
                {
                    var deer = new DeerModel
                    {
                        DeerName = (string)bla["DeerName"],
                        DeerGroup = (string)bla["DeerGroup"],
                        DeerNr = (short)bla["DeerNr"],
                        Smell = (string)bla["Smell"]
                    };
                    deer.Retired = ((string)bla["retired"] != "Working");
                    deerModels.Add(deer);
                }
                if (deerModels.Count != 1) throw new Exception();
                return deerModels[0];
            }
        }

        public static void RetireDeerCall(int canNr, string factory, string taste, int id)
        {
            using (var conn = HelperConnection.getConnection())
            using (var command = conn.CreateCommand())
            {
                command.CommandText = "call RetireWorkingDeer(@id,@canNr,@factory,@taste);";
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@canNr", canNr);
                command.Parameters.AddWithValue("@factory", factory);
                command.Parameters.AddWithValue("@taste", taste);
                command.CommandType = CommandType.Text;

                command.ExecuteNonQuery();

            }

        }
    }
}