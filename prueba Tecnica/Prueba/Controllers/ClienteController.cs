using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Prueba.BusinessLogic;
using Prueba.BusinessLogic.Services;
using Prueba.Entities.Entities;
using Prueba.Models;

namespace Prueba.Controllers
{
    public class ClienteController : Controller
    {
        private readonly GeneralServices _generalServices;
        private readonly IMapper _mapper;
        public ClienteController(GeneralServices generalServices, IMapper mapper)
        {
            _generalServices = generalServices;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("InsertCliente")]
        public IActionResult Insert([FromBody] ClienteViewModel item)
        {
            var mapped = _mapper.Map<Cliente>(item);
            var result = _generalServices.InsertarCliente(mapped);
            return Ok(result);
        }
        [HttpGet("Listar")]
        public IActionResult ListarCargos()
        {
            var list = _generalServices.ListarClientes();
            return Ok(list);
        }
        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            if (id <= 0)
            {
                var errorResult = new ServiceResult();
                return BadRequest(errorResult.BadRequest("ID inválido"));
            }

            var result = _generalServices.BuscarCliente(id);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Actualizar(int id, [FromBody] ClienteViewModel item)
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
                return BadRequest(errorResult.BadRequest("Datos inválidos"));
            }

            var mapped = _mapper.Map<Cliente>(item);
            var result = _generalServices.ActualizarCliente(id, mapped);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                // Si es un error 404 (no encontrado)
                if (result.Code == 404)
                {
                    return NotFound(result);
                }
                // Otros errores (400)
                return BadRequest(result);
            }
        }


    }
}
