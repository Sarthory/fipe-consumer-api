using FipeConsumer.Domain.Entities;

namespace FipeConsumer.Domain.Interfaces
{
    public interface IBrandRepository
    {
        Task<List<Brand>> GetAllBrandsAsync();

        Task UpsertBrandAsync(Brand brand);

        Task BatchUpsertBrandsAsync(List<Brand> brands);
    }
}
