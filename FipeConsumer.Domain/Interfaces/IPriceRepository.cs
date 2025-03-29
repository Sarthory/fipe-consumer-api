using FipeConsumer.Domain.Entities;

namespace FipeConsumer.Domain.Interfaces
{
    public interface IPriceRepository
    {
        Task<IEnumerable<Price>> GetAllPricesAsync();

        Task<Price?> GetSpecificPriceAsync(Brand brand, Model model, Year year);

        Task UpsertPriceAsync(Price price, string brandCode, int modelCode, string yearCode);
    }
}
