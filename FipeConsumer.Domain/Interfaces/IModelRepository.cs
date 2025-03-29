using FipeConsumer.Domain.Entities;

namespace FipeConsumer.Domain.Interfaces
{
    public interface IModelRepository
    {
        Task<IEnumerable<Model>> GetAllModelsAsync();

        Task<Model?> GetModelByCodeAsync(int modelCode);

        Task UpsertModelAsync(Model model, string brandCode);

        Task BatchUpsertModelsAsync(List<Model> models, string brandCode);
    }
}
