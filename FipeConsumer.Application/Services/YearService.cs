using FipeConsumer.Domain.Entities;
using FipeConsumer.Domain.Interfaces;

namespace FipeConsumer.Application.Services
{
    public class YearService(IYearRepository yearRepository)
    {
        private readonly IYearRepository _yearRepository = yearRepository;

        public async Task<List<Year>> GetAllYearsAsync()
        {
            return await _yearRepository.GetAllYearsAsync();
        }

        public async Task<List<Year>> GetYearsByModelCodeAsync(int modelCode)
        {
            return await _yearRepository.GetYearsByModelCodeAsync(modelCode);
        }
    }
}
