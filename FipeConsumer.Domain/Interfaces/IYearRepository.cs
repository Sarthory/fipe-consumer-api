using FipeConsumer.Domain.Entities;

namespace FipeConsumer.Domain.Interfaces
{
    public interface IYearRepository
    {
        Task<List<Year>> GetAllYearsAsync();

        Task<List<Year>> GetYearsByModelCodeAsync(int modelCode);

        Task UpsertYearAsync(Year year, int modelCode);

        Task BatchUpsertYearsAsync(List<Year> years, int modelCode);
    }
}
