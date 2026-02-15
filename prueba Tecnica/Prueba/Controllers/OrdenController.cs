// Controllers/OrdenController.cs
using Microsoft.AspNetCore.Mvc;
using Prueba.BusinessLogic;
using Prueba.BusinessLogic.Services;
using Prueba.Models;

namespace Prueba.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenController : ControllerBase
    {
        private readonly GeneralServices _generalServices;

        public OrdenController(GeneralServices generalServices)
        {
            _generalServices = generalServices;
        }

        [HttpPost]
        public IActionResult CrearOrden([FromBody] OrdenViewModel ordenViewModel)
        {
            // Validar que OrdenId sea 0
            if (ordenViewModel.OrdenId != 0)
            {
                var errorResult = new ServiceResult();
                errorResult.Success = false;
                errorResult.Message = "El OrdenId debe ser 0 para crear una nueva orden";
                errorResult.Errors.Add("El OrdenId debe ser 0 para crear una nueva orden");
                return BadRequest(errorResult);
            }

            // Validar ModelState
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

            // Validar que haya al menos un detalle
            if (ordenViewModel.Detalle == null || !ordenViewModel.Detalle.Any())
            {
                var errorResult = new ServiceResult();
                errorResult.Success = false;
                errorResult.Message = "La orden debe tener al menos un detalle";
                errorResult.Errors.Add("La orden debe tener al menos un detalle");
                return BadRequest(errorResult);
            }

            var result = _generalServices.CrearOrden(ordenViewModel);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}