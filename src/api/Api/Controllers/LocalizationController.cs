using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Interfaces.Services;
using OrderService.Domain.Dto.Response;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Kernel.Net.Api.Controller;

namespace OrderService.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Authorize]
    public class LocalizationController : ControllerBase<LocalizationController>
    {
        private readonly ILocalizationService _service;

        public LocalizationController(ILogger<LocalizationController> logger, ILocalizationService service) : base(logger) => _service = service;

        [ProducesResponseType(typeof(CityResponse), StatusCodes.Status200OK)]
        [HttpGet("city/{cep}", Name = nameof(ILocalizationService.GetCityByCep))]
        public IActionResult GetCityByCep([Required][FromRoute] string cep) => Ok(_service.GetCityByCep(cep));
    }
}