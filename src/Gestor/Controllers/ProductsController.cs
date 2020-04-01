using Gestor.Core.Application;
using Gestor.Core.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gestor.Api.Controllers
{
    [ApiController, Route("v1/[controller]")]
    public class ProductsController : Controller
    {
        private readonly ProductApplication productApplication;

        public ProductsController(ProductApplication productApplication)
            => this.productApplication = productApplication;

        [HttpPost, Route(""), Authorize("Bearer")]
        public async Task<IActionResult> Save(Product product)
        {
            var result = await productApplication.Save(product);

            return Ok(result);
        }

        [HttpGet, Route("{id}"), Authorize("Bearer")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await productApplication.Get(id);

            return Ok(result);
        }

        [HttpDelete, Route("{id}"), Authorize("Bearer")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await productApplication.Delete(id);

            return Ok();
        }
    }
}