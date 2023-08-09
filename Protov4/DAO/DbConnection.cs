
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Protov4.DAO
{
    public class DbConnection
    {
        public SqlConnection conexion;

        public DbConnection(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("conexion");
            if (connectionString != null)
            {
                conexion = new SqlConnection(connectionString);
            }
            else
            {
                // Manejo de error o excepción en caso de que la cadena de conexión sea nula
                // Por ejemplo, puedes lanzar una excepción para indicar que no se pudo obtener la cadena de conexión correctamente.
                throw new ApplicationException("La cadena de conexión es nula.");
            }
        }

        public SqlConnection GetSqlConnection()
        {
            return conexion;
        }
    }

}
