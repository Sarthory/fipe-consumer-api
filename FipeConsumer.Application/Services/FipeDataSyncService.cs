using FipeConsumer.Infrastructure.ExternalServices;
using FipeConsumer.Domain.Interfaces;
using FipeConsumer.Domain.Entities;

namespace FipeConsumer.Application.Services
{
    public class FipeDataSyncService(
        FipeApiClient apiClient,
        IBrandRepository brandRepository,
        IModelRepository modelRepository,
        IYearRepository yearRepository,
        IPriceRepository priceRepository)
    {
        private readonly FipeApiClient _apiClient = apiClient;
        private readonly IBrandRepository _brandRepository = brandRepository;
        private readonly IModelRepository _modelRepository = modelRepository;
        private readonly IYearRepository _yearRepository = yearRepository;
        private readonly IPriceRepository _priceRepository = priceRepository;

        public async Task AcquireFipeDataAsync()
        {
            List<Brand> remoteBrands = await _apiClient.GetBrandsAsync();
            await _brandRepository.BatchUpsertBrandsAsync(remoteBrands);

            foreach (var brand in remoteBrands)
            {
                List<Model> models = await _apiClient.GetBrandModelsAsync(brand.Code);
                await _modelRepository.BatchUpsertModelsAsync(models, brand.Code);

                foreach (var model in models)
                {
                    List<Year> years = await _apiClient.GetBrandModelYearsAsync(brand.Code, model.Code);
                    await _yearRepository.BatchUpsertYearsAsync(years, model.Code);

                    foreach (var year in years)
                    {
                        Price price = await _apiClient.GetBrandModelYearPricesAsync(brand.Code, model.Code, year.Code);
                        await _priceRepository.UpsertPriceAsync(price, brand.Code, model.Code, year.Code);
                    }
                }
            }
        }
    }
}