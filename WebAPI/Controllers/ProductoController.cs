using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoController(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        // http://localhost:5000/api/producto
        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetAllProductos()
        {
            var productos = await _productoRepository.GetAllProductosAsync();
            return Ok(productos);
        }

        // http://localhost:5000/api/producto/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProductoById(int id)
        {
            var producto = await _productoRepository.GetProductoByIdAsync(id);
            return Ok(producto);
        }

    }
}
