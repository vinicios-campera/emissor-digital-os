using Asp.Versioning;
using Kernel.Net.Api.Controller;
using Kernel.Net.Api.FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Interfaces.Services;
using OrderService.Domain.Dto.Insert;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace OrderService.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    public class HelperController : ControllerBase<HelperController>
    {
        private readonly IHelperService _service;

        public HelperController(ILogger<HelperController> logger, IHelperService service) : base(logger) => _service = service;

        [ProducesResponseType(StatusCodes.Status302Found)]
        [HttpGet("app", Name = nameof(IHelperService.GetDownloadApp))]
        public IActionResult GetDownloadApp() => Redirect(_service.GetDownloadApp());

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
        [HttpPost("message", Name = nameof(IHelperService.AddMessage))]
        public IActionResult AddMessage([Required] MessageInsert payload) => Ok(_service.AddMessage(payload));
    }
}