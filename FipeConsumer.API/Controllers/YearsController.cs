using Microsoft.AspNetCore.Mvc;
using FipeConsumer.Application.Services;

namespace FipeConsumer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class YearsController(YearService yearService) : ControllerBase
    {
        private readonly YearService _yearService = yearService;

        [HttpGet]
        public async Task<IActionResult> Get(int modelCode)
        {
            var years = await _yearService.GetYearsByModelCodeAsync(modelCode);
            if (years == null || years.Count == 0)
                return NotFound("No years found for the specified model ID.");

            return Ok(years);
        }
    }
}
