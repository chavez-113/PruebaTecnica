using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prueba.BusinessLogic;
using Prueba.BusinessLogic.Services;
using Prueba.Entities.Entities;
using Prueba.Models;

namespace Prueba.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly GeneralServices _generalServices;
        private readonly IMapper _mapper;

        public ProductoController(GeneralServices generalServices, IMapper mapper)
        {
            _generalServices = generalServices;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            var result = _generalServices.ListarProductos();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(long id)
        {
            if (id <= 0)
            {
                var errorResult = new ServiceResult();
                return BadRequest(errorResult.BadRequest("ID inválido"));
            }

            var result = _generalServices.BuscarProducto(id);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        public IActionResult Insert([FromBody] ProductoViewModel item)
        {
            if (item.ProductoId != 0)
            {
                var errorResult = new ServiceResult();
                errorResult.Success = false;
                errorResult.Message = "El ProductoId debe ser 0 para crear un nuevo producto";
                errorResult.Errors.Add("El ProductoId debe ser 0 para crear un nuevo producto");
                return BadRequest(errorResult);
            }

            if (!ModelState.IsValid)
            {
                var errorResult = new ServiceResult();
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                errorResult.Success = false;
                errorResult.Message = "Datos inválidos";
                errorResult.Errors = errors;
                return BadRequest(errorResult);
            }

            var mapped = _mapper.Map<Producto>(item);
            var result = _generalServices.InsertarProducto(mapped);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Actualizar(long id, [FromBody] ProductoViewModel item)
        {
            if (id <= 0)
            {
                var errorResult = new ServiceResult();
                return BadRequest(errorResult.BadRequest("ID inválido"));
            }

            if (!ModelState.IsValid)
            {
                var errorResult = new ServiceResult();
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                errorResult.Success = false;
                errorResult.Message = "Datos inválidos";
                errorResult.Errors = errors;
                return BadRequest(errorResult);
            }

            var mapped = _mapper.Map<Producto>(item);
            var result = _generalServices.ActualizarProducto(id, mapped);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                if (result.Code == 404)
                {
                    return NotFound(result);
                }
                return BadRequest(result);
            }
        }
    }
}