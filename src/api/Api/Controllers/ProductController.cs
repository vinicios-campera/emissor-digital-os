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
    public class ProductController : ControllerBase<ProductController>
    {
        private readonly IProductService _service;

        public ProductController(ILogger<ProductController> logger, IProductService service) : base(logger) => _service = service;

        [EnableQuery]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        [HttpGet(Name = nameof(IProductService.GetProducts))]
        public IActionResult GetProducts() => Ok(_service.GetProducts(x => true));

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
        [HttpPost(Name = nameof(IProductService.AddProduct))]
        public IActionResult AddProduct([Required] ProductInsert payload) => Ok(_service.AddProduct(payload));

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Error>), StatusCodes.Status400BadRequest)]
        [HttpPut(Name = nameof(IProductService.UpdateProduct))]
        public IActionResult UpdateProduct([Required] ProductUpdate payload) => Ok(_service.UpdateProduct(payload));

        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [HttpDelete("{id}", Name = nameof(IProductService.DeleteProduct))]
        public IActionResult DeleteProduct([Required][FromRoute] Guid id) => Ok(_service.DeleteProduct(id));
    }
}