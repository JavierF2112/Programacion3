using Programacion3.Models;

namespace Programacion3.Services
{
    public class ProductoService : IProductoService
        {
            // Simulamos una "base de datos" en memoria para esta etapa
            private readonly List<Producto> _productos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Teclado", Precio = 1500 },
            new Producto { Id = 2, Nombre = "Mouse", Precio = 800 },
            new Producto { Id = 3, Nombre = "Monitor", Precio = 50000 }
        };

            public IEnumerable<Producto> ObtenerTodos()
            {
                return _productos;
            }

            public Producto ObtenerPorId(int id)
            {
                // Buscamos el primero que coincida con el ID, o devolvemos null si no existe
                return _productos.First(p => p.Id == id);
            }
        }
    }