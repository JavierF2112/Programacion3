using Programacion3.Data;
using Programacion3.Models;

namespace Programacion3.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        // Inyectamos el Repository, no la configuración de SQL
        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public IEnumerable<Producto> ObtenerTodos()
        {
            // El Service solo delega al Repository. No sabe cómo se obtienen los datos.
            return _productoRepository.ObtenerTodos();
        }

        public Producto? ObtenerPorId(int id)
        {
            // El Service solo delega. No sabe cómo se busca.
            return _productoRepository.ObtenerPorId(id);
        }
    }
}

