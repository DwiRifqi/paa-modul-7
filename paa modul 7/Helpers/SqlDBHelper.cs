using Npgsql;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography.Xml;

namespace paa_modul_7.Helpers
{
    public class SqlDBHelper
    {
        private NpgsqlConnection connection;
        private string __constr;
        public SqlDBHelper(string pConstr)
        {
            __constr = pConstr;
            connection = new NpgsqlConnection();
            connection.ConnectionString = __constr;
        }

        public NpgsqlCommand getNpgsqlCommand(string query)
        {
            connection.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }
        public void closeConnection()
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
        public void Dispose()
        {
            if (connection != null)
            {
                closeConnection();
                connection.Dispose();
            }
        }
    }
}
