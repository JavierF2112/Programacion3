using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Programacion3.Models;


namespace Programacion3.Data
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly string _connectionString;

        // Inyectamos la configuración para leer la Connection String
        public ProductoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public IEnumerable<Producto> ObtenerTodos() //Obtener todos los productos
        {
            var productos = new List<Producto>();
            try // Envolvemos todo en un try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    // Consulta a la tabla
                    string query = "SELECT Id, Nombre, Precio FROM Productos";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Transformamos el registro de Sql en un Objeto Producto
                                var producto = new Producto
                                {
                                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                    Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                    Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                    // Stock = reader.GetInt32(reader.GetOrdinal("Stock")) // Aparentemente sin Stock en la tabla
                                };
                                productos.Add(producto);
                            }
                        }
                    }
                }
                return productos;
            }
            catch(SqlException ex) // <--- CAPTURAMOS SOLO ERRORES DE SQL
            {
                // Logueamos el error real internamente (en un futuro acá va un ILogger)
                Console.WriteLine($"Error de SQL: {ex.Message}");

                // Lanzamos nuestra excepción genérica SIN datos sensibles
                throw new DataAccessException("Ocurrió un error al intentar obtener los productos de la base de datos.");
            }
        }
        public Producto? ObtenerPorId(int id) //Obetener un producto por ID
        {
            Producto? producto = null;

            try //  try acá ambién.
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Nombre, Precio, Stock FROM Productos WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //Usamos parámetros para evitar Inyección Sql
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) //Si encontró la fila
                        {
                            producto = new Producto
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                Precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                               // Stock = reader.GetInt32(reader.GetOrdinal("Stock"))
                            };
                        }
                    }
                }
            }
            return producto;
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Error de SQL: {ex.Message}");
            throw new DataAccessException("Ocurrió un error al intentar obtener el producto de la base de datos.");
            }
        }
    }
}