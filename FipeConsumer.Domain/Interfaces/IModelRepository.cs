using FipeConsumer.Domain.Entities;

namespace FipeConsumer.Domain.Interfaces
{
    public interface IModelRepository
    {
        Task<List<Model>> GetAllModelsAsync();

        Task<List<Model>> GetModelsByBrandCodeAsync(string brandCode);

        Task UpsertModelAsync(Model model, string brandCode);

        Task BatchUpsertModelsAsync(List<Model> models, string brandCode);
    }
}
