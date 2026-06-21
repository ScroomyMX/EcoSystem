using EcoSystem.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcoSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private static readonly List<Producto> Productos = new();

        [HttpGet]
        public ActionResult<IEnumerable<Producto>> Get()
        {
            return Ok(Productos);
        }

        [HttpPost]
        public ActionResult<Producto> Post(Producto producto)
        {
            producto.Id = Productos.Count + 1;
            Productos.Add(producto);
            return CreatedAtAction(nameof(Get), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Producto producto)
        {
            var existente = Productos.FirstOrDefault(p => p.Id == id);

            if (existente == null)
                return NotFound();

            existente.Nombre = producto.Nombre;
            existente.Descripcion = producto.Descripcion;
            existente.Precio = producto.Precio;
            existente.Stock = producto.Stock;
            existente.SKU = producto.SKU;
            existente.CategoriaId = producto.CategoriaId;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var producto = Productos.FirstOrDefault(p => p.Id == id);

            if (producto == null)
                return NotFound();

            Productos.Remove(producto);
            return NoContent();
        }
    }
}