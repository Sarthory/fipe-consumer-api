using Microsoft.AspNetCore.Mvc;
using FipeConsumer.Application.Services;

namespace FipeConsumer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandsController(BrandService service) : ControllerBase
    {

        private readonly BrandService _service = service;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var brands = await _service.GetBrandsAsync();
            if (brands == null || brands.Count == 0)
                return NotFound("No brands found.");

            return Ok(brands);
        }
    }
}
