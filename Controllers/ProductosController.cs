using Microsoft.AspNetCore.Mvc;
using Programacion3.Data;
using Programacion3.Models;
using Programacion3.Services;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Programacion3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        // Inyectamos el servicio a través del constructor
        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        // GET: api/<ProductosController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // El controller delega la tarea al Service
                var productos = _productoService.ObtenerTodos();
                return Ok(productos);
            }
            catch
            { 
                // Devolvemos 500 sin detalles de SQL
                return StatusCode(500, "Ocurrió un error interno al procesar la solicitud.");
            }
        }

        // GET api/<ProductosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            { 
                 // El controller le pide al Service que busque el producto
                 var producto = _productoService.ObtenerPorId(id);
                 if (producto == null) return NotFound(); // Si el Service devolvió null, tiramos 404
                 return Ok(producto); // Si lo encontró, tiramos 200 OK
            }
            catch (DataAccessException)
            {
                // Devolvemos 500 sin detalles de SQL
                return StatusCode(500, "Ocurrió un error interno al procesar la solicitud.");
            }
        }

        // POST api/<ProductosController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProductosController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
