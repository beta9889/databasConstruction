using databasConstruction.HelperClasses;
using MySql.Data.MySqlClient;
using System.Data;

namespace databasConstruction.Models
{
    public record DeerToDeerModel
    {
        public short DeerNr { get; set; }
        public string DeerName { get; set; }
        public string DeerStatus { get; set; }

        public static List<DeerToDeerModel> GetAll(short id)
        {
            List<DeerToDeerModel> result = new();
            try
            {
                using (var connection = HelperConnection.getConnection())
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT DeerNr1 as Deer, name1 as name, 'Deer1 retired' as status FROM ViewDeerConnection " +
                        $"WHERE ViewDeerConnection.DeerNr2= @ssnId" +
                        $" union SELECT DeerNr2 as Deer, name2 as name, 'Deer2 retired' as status " +
                        $"FROM ViewDeerConnection WHERE ViewDeerConnection.DeerNr1= @ssnId;";
                    command.Parameters.AddWithValue("@ssnId", id);
                    command.CommandType = CommandType.Text;

                    MySqlDataAdapter adapter = new();
                    adapter.SelectCommand = command;
                    DataSet table = new();
                    adapter.Fill(table);

                    foreach (var data in table.Tables[0].AsEnumerable())
                    {
                        result.Add(convert(data));
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

        public static void AddConnection(short mainId, short SelectedId)
        {
            using (var connection = HelperConnection.getConnection())
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "INSERT INTO DeerToDeer(firstDeerNr,secondDeerNr) Values(@mainDeer,@secondDeer)";
                command.Parameters.AddWithValue("@mainDeer", mainId);
                command.Parameters.AddWithValue("@secondDeer", SelectedId);
                command.ExecuteNonQuery();
            }
        }

        private static DeerToDeerModel convert(DataRow data)
        {
            return new DeerToDeerModel()
            {
                DeerNr = (short)data["Deer"],
                DeerName = (string)data["name"],
                DeerStatus = (string)data["status"]
            };
        }
    }
}