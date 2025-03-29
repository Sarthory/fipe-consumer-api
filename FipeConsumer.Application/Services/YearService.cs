using FipeConsumer.Domain.Entities;
using FipeConsumer.Domain.Interfaces;

namespace FipeConsumer.Application.Services
{
    public class YearService(IYearRepository yearRepository)
    {
        private readonly IYearRepository _yearRepository = yearRepository;

        public async Task<IEnumerable<Year>> GetYearsAsync()
        {
            return await _yearRepository.GetAllYearsAsync();
        }

        public async Task UpsertYearAsync(Year year, int modelCode)
        {
            await _yearRepository.UpsertYearAsync(year, modelCode);
        }

    }
}
