using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MTest.Providers
{
    public class ConnectionProvider
    {
        public static IDbConnection GetConnection(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MTestDB");
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

    }
}
