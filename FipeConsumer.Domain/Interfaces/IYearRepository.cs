using FipeConsumer.Domain.Entities;

namespace FipeConsumer.Domain.Interfaces
{
    public interface IYearRepository
    {
        Task<IEnumerable<Year>> GetAllYearsAsync();

        Task<Year?> GetYearByCodeAsync(string codigo);

        Task UpsertYearAsync(Year year, int modelCode);

        Task BatchUpsertYearsAsync(List<Year> years, int modelCode);
    }
}
