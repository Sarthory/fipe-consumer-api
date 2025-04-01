using Microsoft.AspNetCore.Mvc;
using FipeConsumer.Application.Services;

namespace FipeConsumer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PricesController(PriceService priceService) : ControllerBase
    {
        private readonly PriceService _priceService = priceService;

        [HttpGet]
        public async Task<IActionResult> Get(string brandCode, int modelCode, string yearCode)
        {
            var price = await _priceService.GetSpecificPriceAsync(brandCode, modelCode, yearCode);
            if (price == null)
                return NotFound("No prices for this vehicle were found.");

            return Ok(price);
        }

    }
}
