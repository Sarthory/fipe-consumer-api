using FipeConsumer.Domain.Entities;

namespace FipeConsumer.Domain.Interfaces
{
    public interface IPriceRepository
    {
        Task<List<Price>> GetAllPricesAsync();

        Task<Price?> GetSpecificPriceAsync(string brandCode, int modelCode, string yearCode);

        Task UpsertPriceAsync(Price price, string brandCode, int modelCode, string yearCode);
    }
}
