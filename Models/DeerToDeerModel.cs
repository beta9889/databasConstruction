using databasConstruction.HelperClasses;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.Sec;
using System.Data;

namespace databasConstruction.Models
{
    public record DeerToDeerModel
    {
        public short DeerNr { get; set; }
        public string DeerName { get; set; }

        public string DeerStatus { get; set; }

        public static List<DeerToDeerModel> GetConnectionById(short id)
        {
            List<DeerToDeerModel> result = new();
            try
            {
                using (var connection = HelperConnection.getConnection())
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM ViewDeerConnection WHERE ViewDeerConnection.DeerNr1= @ssnId;";
                    command.Parameters.AddWithValue("@ssnId", id);
                    command.CommandType = CommandType.Text;

                    MySqlDataAdapter adapter = new();
                    adapter.SelectCommand = command;
                    DataSet table = new();
                    adapter.Fill(table);

                    foreach(var data in table.Tables[0].AsEnumerable())
                    {
                        result.Add(convertTableToModel(data));
                    }
                }
            }
            catch
            {
                Console.WriteLine("Shits fucked yo");
                throw;
            }

            return result;
        }

        public static List<DeerToDeerModel> GetConnectionById2(short id)
        {
            List<DeerToDeerModel> result = new();
            try
            {
                using (var connection = HelperConnection.getConnection())
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM ViewDeerConnection WHERE ViewDeerConnection.DeerNr2= @ssnId;";
                    command.Parameters.AddWithValue("@ssnId", id);
                    command.CommandType = CommandType.Text;

                    MySqlDataAdapter adapter = new();
                    adapter.SelectCommand = command;
                    DataSet table = new();
                    adapter.Fill(table);

                    foreach (var data in table.Tables[0].AsEnumerable())
                    {
                        result.Add(FirstNameConvert(data));
                    }
                }
            }
            catch
            {
                Console.WriteLine("Shits fucked yo");
                throw;
            }

            return result;
        }

        public static void AddConnection(short mainId,short SelectedId)
        {
            using(var connection = HelperConnection.getConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO DeerToDeer(firstDeerNr,secondDeerNr) Values(@mainDeer,@secondDeer)";
                command.Parameters.AddWithValue("@mainDeer", mainId);
                command.Parameters.AddWithValue("@secondDeer", SelectedId);
                command.ExecuteNonQuery();
            }
        }

        private static DeerToDeerModel convertTableToModel(DataRow data)
        {
            return new DeerToDeerModel()
            {
                DeerNr = (short)data["DeerNr2"],
                DeerName = (string)data["name2"],
                DeerStatus = (string)data["Deer2 retired"]
            };
        }

        private static DeerToDeerModel FirstNameConvert(DataRow data)
        {
            return new DeerToDeerModel()
            {
                DeerNr = (short)data["DeerNr1"],
                DeerName = (string)data["name1"],
                DeerStatus = (string)data["Deer1 retired"]
            };
        }
    }
}
