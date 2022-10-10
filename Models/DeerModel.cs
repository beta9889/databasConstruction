using databasConstruction.HelperClasses;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace databasConstruction.Models
{
    public record DeerModel
    {
        [Required]
        public short DeerNr { get; set; }
        [Required]
        public string DeerName { get; set; }
        [Required]
        public string DeerGroup { get; set; }
        [Required]
        public string Smell { get; set; }
        [Required]
        public bool Retired { get; set; }
        [Required]
        public bool Shown { get; set; } = false;

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
                    result.Add(DataRowToDeerModel(bla));
                }
                return result;
            }
        }

        public static DeerModel GetById(short id)
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

                    deerModels.Add(DataRowToDeerModel(bla));
                }
                if (deerModels.Count != 1) throw new Exception();
                return deerModels[0];
            }
        }

        private static DeerModel DataRowToDeerModel(DataRow data)
        {
            var deer = new DeerModel
            {
                DeerName = (string)data["DeerName"],
                DeerGroup = (string)data["DeerGroup"],
                DeerNr = (short)data["DeerNr"],
                Smell = (string)data["Smell"]
            };
            deer.Retired = ((string)data["retired"] != "Working");
            return deer;
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