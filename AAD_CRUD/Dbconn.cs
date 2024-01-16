using System.Xml.Linq;
using Microsoft.Data.SqlClient;

namespace AAD_CRUD
{
    public class Dbconn
    {
        public static SqlConnection DBConnection()
        {
            string connString = String.Format(
                "Server = localhost; Database = AAD; Trusted_Connection=true; TrustServerCertificate=true;");
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}
