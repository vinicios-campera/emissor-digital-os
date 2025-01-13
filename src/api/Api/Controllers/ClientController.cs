using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OrderService.Application.Interfaces.Services;
using OrderService.Domain.Dto.Insert;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Dto.Update;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Kernel.Net.Api.Controller;
using Kernel.Net.Api.FluentValidation;

namespace OrderService.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Authorize]
    public class ClientController : ControllerBase<ClientController>
    {
        private readonly IClientService _service;

        public ClientController(ILogger<ClientController> logger, IClientService service) : base(logger) => _service = service;

        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<ClientResponse>), StatusCodes.Status200OK)]
        [HttpGet(Name = nameof(IClientService.GetClients))]
        public IActionResult GetClients() => Ok(_service.GetClients(x => true));

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
        [HttpPost(Name = nameof(IClientService.AddClient))]
        public IActionResult AddClient([Required] ClientInsert payload) => Ok(_service.AddClient(payload));

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
        [HttpPut(Name = nameof(IClientService.UpdateClient))]
        public IActionResult UpdateClient([Required] ClientUpdate payload) => Ok(_service.UpdateClient(payload));

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpDelete("{id}", Name = nameof(IClientService.DeleteClient))]
        public IActionResult DeleteClient([Required][FromRoute] Guid id) => Ok(_service.DeleteClient(id));
    }
}