using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using OrderService.Application.Interfaces.Services;
using OrderService.Domain.Dto.Response;
using System.Net.Mime;
using Kernel.Net.Api.Controller;

namespace OrderService.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Authorize]
    public class NotificationController : ControllerBase<NotificationController>
    {
        private readonly INotificationService _service;

        public NotificationController(ILogger<NotificationController> logger, INotificationService service) : base(logger) => _service = service;

        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<NotificationResponse>), StatusCodes.Status200OK)]
        [HttpGet(Name = nameof(INotificationService.GetNotifications))]
        public IActionResult GetNotifications() => Ok(_service.GetNotifications(x => true, true));
    }
}