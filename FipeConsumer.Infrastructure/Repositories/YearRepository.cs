using Microsoft.EntityFrameworkCore;
using FipeConsumer.Domain.Entities;
using FipeConsumer.Domain.Interfaces;
using FipeConsumer.Infrastructure.Data;

namespace FipeConsumer.Infrastructure.Repositories
{
    public class YearRepository(FipeConsumerDbContext context) : IYearRepository
    {
        private readonly FipeConsumerDbContext _context = context;

        public async Task<List<Year>> GetAllYearsAsync()
        {
            return await _context.Years.ToListAsync();
        }

        public Task<List<Year>> GetYearsByModelCodeAsync(int modelCode)
        {
            return _context.Years
                .Include(y => y.Model)
                .Where(y => y.Model != null && y.Model.Code == modelCode)
                .ToListAsync();
        }

        public async Task UpsertYearAsync(Year year, int modelCode)
        {
            var existingYear = await _context.Years.Include(y => y.Model)
                                                   .FirstOrDefaultAsync(y => y.Code == year.Code &&
                                                                        y.Model != null &&
                                                                        y.Model.Code == modelCode);

            var model = await _context.Models.FirstOrDefaultAsync(m => m.Code == modelCode) ?? throw new Exception("Model not found.");

            year.SetModelId(model.ModelId);

            if (existingYear == null) _context.Years.Add(year);
            else
            {
                existingYear.SetModelId(model.ModelId);
                existingYear.SetCode(year.Code);
                existingYear.SetName(year.Name);                

                _context.Years.Update(existingYear);
            }

            await _context.SaveChangesAsync();
        }

        public async Task BatchUpsertYearsAsync(List<Year> years, int modelCode)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingYears = await _context.Years.ToListAsync();

                var model = await _context.Models.FirstOrDefaultAsync(m => m.Code == modelCode) ?? throw new Exception("Model not found.");

                foreach (var year in years)
                {
                    year.SetModelId(model.ModelId);

                    var existingYear = existingYears?.FirstOrDefault(y => y.Code == year.Code &&
                                                                     y.Model != null &&
                                                                     y.Model.Code == modelCode);

                    if (existingYear == null) _context.Years.Add(year);
                    else
                    {
                        existingYear.SetModelId(model.ModelId);
                        existingYear.SetCode(year.Code);
                        
                        _context.Years.Update(existingYear);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error when trying to batch upsert years. {ex.Message}", ex);
            }
        }
    }
}