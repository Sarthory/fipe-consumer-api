using FipeConsumer.Domain.Entities;
using FipeConsumer.Domain.Interfaces;

namespace FipeConsumer.Application.Services
{
    public class ModelService(IModelRepository modelRepository)
    {
        private readonly IModelRepository _modelRepository = modelRepository;

        public async Task<List<Model>> GetModelsByBrandCodeAsync(string brandCode)
        {
            return await _modelRepository.GetModelsByBrandCodeAsync(brandCode);
        }

        public async Task UpsertModelAsync(Model model, string brandCode)
        {
            await _modelRepository.UpsertModelAsync(model, brandCode);
        }

    }
}
