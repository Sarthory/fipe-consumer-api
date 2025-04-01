using Microsoft.AspNetCore.Mvc;
using FipeConsumer.Application.Services;
using FipeConsumer.Domain.Entities;

namespace FipeConsumer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelsController(ModelService modelService) : ControllerBase
    {
        private readonly ModelService _modelService = modelService;

        [HttpGet]
        public async Task<IActionResult> Get(string brandCode)
        {
            List<Model> models = await _modelService.GetModelsByBrandCodeAsync(brandCode);
            if (models == null || models.Count == 0)
                return NotFound("No models found for the specified brand ID.");

            return Ok(models);
        }
    }
}
