using Programacion3.Models;

namespace Programacion3.Services
{
    public interface IProductoService
    {
        // Operación 1: Obtener todos los productos
        IEnumerable<Producto> ObtenerTodos();

        // Operación 2: Obtener un producto por su ID
        Producto? ObtenerPorId(int id);
    }
}
