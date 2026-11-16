using Programacion3.Models;
namespace Programacion3.Data
{
    public interface IProductoRepository
    {
        // Las mismas operaciones que tenía el Service
        IEnumerable<Producto> ObtenerTodos();
        Producto? ObtenerPorId(int id);
    }
}
