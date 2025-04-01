using FipeConsumer.Domain.Entities;
using FipeConsumer.Domain.Interfaces;

namespace FipeConsumer.Application.Services
{
    public class BrandService(IBrandRepository brandRepository)
    {
        private readonly IBrandRepository _brandRepository = brandRepository;

        public async Task<List<Brand>> GetBrandsAsync()
        {
            return await _brandRepository.GetAllBrandsAsync();
        }

        public async Task UpsertBrandAsync(Brand brand)
        {
            await _brandRepository.UpsertBrandAsync(brand);
        }
    }
}
