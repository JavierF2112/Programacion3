using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Programacion3.Models;
namespace Programacion3.Services
{
    public class ProductoService : IProductoService
    {    
        private readonly string _connectionString;
 
        // Inyectamos la configuración por el constructor (Prueba de conexion)
        public ProductoService(IConfiguration configuration)
        {
            // El signo ! evita los warnings de nulos
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public IEnumerable<Producto> ObtenerTodos()
        {
            var productos = new List<Producto>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nombre, Precio FROM Productos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // SqlDataReader lee los datos fila por fila
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Mientras haya filas para leer...
                        while (reader.Read())
                        {
                            // Creamos un objeto Producto y lo llenamos con los datos de la BD
                            var producto = new Producto
                            {
                                // Importante: leer por el nombre de la columna de SQL
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                // Si el Precio en SQL es decimal, usamos GetDecimal
                                Precio = reader.GetDecimal(reader.GetOrdinal("Precio"))
                            };

                            productos.Add(producto);
                        }
                    }
                }
            }

            return productos;
        }

        public Producto ObtenerPorId(int id)
        {
            Producto producto = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nombre, Precio FROM Productos WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Usamos parámetros para evitar Inyección SQL (muy importante)
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            producto = new Producto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                Precio = reader.GetDecimal(reader.GetOrdinal("Precio"))
                            };
                        }
                    }
                }
            }

            return producto;
        }
    }
}


