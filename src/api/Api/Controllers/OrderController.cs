using Asp.Versioning;
using Kernel.Net.Api.Controller;
using Kernel.Net.Api.FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using MimeDetective;
using OrderService.Application.Interfaces.Services;
using OrderService.Domain.Dto.Insert;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace OrderService.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Authorize]
    public class OrderController : ControllerBase<OrderController>
    {
        private readonly IOrderService _service;

        public OrderController(ILogger<OrderController> logger, IOrderService service) : base(logger) => _service = service;

        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
        [HttpGet(Name = nameof(IOrderService.GetOrders))]
        public IActionResult GetOrders() => Ok(_service.GetOrders(x => true));

        [ProducesResponseType(typeof(OrderPdfResponse), StatusCodes.Status200OK)]
        [HttpGet("pdf", Name = nameof(IOrderService.GetOrderAsPdf))]
        public IActionResult GetOrderAsPdf([Required][FromQuery] string[] ids) => Ok(_service.GetOrderAsPdf(ids));

        [ProducesResponseType(typeof(OrderDetailResponse), StatusCodes.Status200OK)]
        [HttpGet("pdf/detail/{id}", Name = nameof(IOrderService.GetOrderDetailById))]
        public IActionResult GetOrderDetailById([Required][FromRoute] Guid id) => Ok(_service.GetOrderDetailById(id));

        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpGet("pdf/document/{id}", Name = nameof(IOrderService.GetOrderPdfById))]
        public IActionResult GetOrderById([Required][FromRoute] Guid id)
        {
            var order = _service.GetOrderPdfById(id);
            if (order == null)
                return NotFound();

            var Inspector = new ContentInspectorBuilder()
            {
                Definitions = MimeDetective.Definitions.Default.All()
            }.Build();
            var result = Inspector.Inspect(order);
            return File(order, result[0].Definition.File.MimeType);
        }

        [ProducesResponseType(typeof(OrderPdfResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
        [HttpPost(Name = nameof(IOrderService.AddOrder))]
        public IActionResult AddOrder([Required] OrderInsert payload) => Ok(_service.AddOrder(payload));

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpPatch("state", Name = nameof(IOrderService.UpdateOrderState))]
        public IActionResult UpdateOrderState([Required][FromQuery] string[] ids, [Required][FromQuery] OrderState state) => Ok(_service.UpdateOrderState(ids, state));

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpDelete("{id}", Name = nameof(IOrderService.DeleteOrder))]
        public IActionResult DeleteOrder([Required][FromRoute] Guid id) => Ok(_service.DeleteOrder(id));
    }
}