using Asp.Versioning;
using Kernel.Net.Api.Controller;
using Kernel.Net.Api.FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeDetective;
using OrderService.Application.Interfaces.Services;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Dto.Update;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

namespace OrderService.Api.Controllers
{
    [ApiVersion("1.0")]
    [Produces(MediaTypeNames.Application.Json)]
    [Authorize]
    public class UserController : ControllerBase<UserController>
    {
        private readonly IUserService _service;

        public UserController(ILogger<UserController> logger, IUserService service) : base(logger) => _service = service;

        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [HttpGet(Name = nameof(IUserService.GetUser))]
        public IActionResult GetUser() => Ok(_service.GetUser());

        [AllowAnonymous]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [HttpGet("picture/{id}", Name = nameof(IUserService.GetPictureByUserId))]
        public IActionResult GetPictureByUserId([Required][FromRoute] Guid id)
        {
            var picture = _service.GetPictureByUserId(id);
            if (picture == null || picture.Length == 0)
                return NotFound();

            var Inspector = new ContentInspectorBuilder()
            {
                Definitions = MimeDetective.Definitions.Default.All()
            }.Build();
            var result = Inspector.Inspect(picture);
            return File(picture, result[0].Definition.File.MimeType);
        }

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
        [HttpPut(Name = nameof(IUserService.UpdateUser))]
        public IActionResult UpdateUser([Required] UserUpdate payload) => Ok(_service.UpdateUser(payload));

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpPatch("privacypolicy", Name = nameof(IUserService.AcceptPrivacyPolicy))]
        public IActionResult AcceptPrivacyPolicy() => Ok(_service.AcceptPrivacyPolicy());

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpPatch("push/token/{token}", Name = nameof(IUserService.UpdatePushNotificationToken))]
        public IActionResult UpdatePushNotificationToken([Required][FromRoute] string token) => Ok(_service.UpdatePushNotificationToken(token));

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpPatch("pictureInOrder/{insert}", Name = nameof(IUserService.UpdateAddPictureInOrder))]
        public IActionResult UpdateAddPictureInOrder([Required][FromRoute] bool insert)
            => Ok(_service.UpdateAddPictureInOrder(insert));

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpDelete(Name = nameof(IUserService.DeleteAccount))]
        public IActionResult DeleteAccount() => Ok(_service.DeleteAccount());
    }
}