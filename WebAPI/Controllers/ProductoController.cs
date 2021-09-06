using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;
        public ProductoController(IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        // http://localhost:5000/api/producto
        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetAllProductos()
        {
            var spec = new ProductoWithCategoriasAndMarcaSpecification();
            var productos = await _productoRepository.GetAllWithSpec(spec);

            return Ok(_mapper.Map<IReadOnlyList<Producto>, IReadOnlyList<ProductoDto>>(productos));
        }

        // http://localhost:5000/api/producto/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProductoById(int id)
        {
            // spec = debe inlcuir la logica de la condicion de la consulta y tambien las relaciones entre entidades
            // la relacion entre marca y categoria
            var spec = new ProductoWithCategoriasAndMarcaSpecification(id);
            var producto = await _productoRepository.GetByIdWithSpec(spec);
            return _mapper.Map<Producto, ProductoDto>(producto);
        }

    }
}
