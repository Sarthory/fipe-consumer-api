using Microsoft.EntityFrameworkCore;
using FipeConsumer.Domain.Entities;
using FipeConsumer.Domain.Interfaces;
using FipeConsumer.Infrastructure.Data;

namespace FipeConsumer.Infrastructure.Repositories
{
    public class YearRepository(FipeConsumerDbContext context) : IYearRepository
    {
        private readonly FipeConsumerDbContext _context = context;

        public async Task<IEnumerable<Year>> GetAllYearsAsync()
        {
            return await _context.Years.ToListAsync();
        }

        public async Task<Year?> GetYearByCodeAsync(string code)
        {
            return await _context.Years.FirstOrDefaultAsync(m => m.Code == code);
        }

        public async Task UpsertYearAsync(Year year, int modelCode)
        {
            var existingYear = await _context.Years.FirstOrDefaultAsync(m => m.Code == year.Code);
            var model = await _context.Models.FirstOrDefaultAsync(m => m.Code == modelCode) ?? throw new Exception("Model not found");

            year.Model = model;

            if (existingYear == null) _context.Years.Add(year);
            else
            {
                existingYear.Name = year.Name;
                existingYear.Code = year.Code;
                existingYear.Model = year.Model;
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
                    year.Model = model;
                    var existingYear = existingYears?.FirstOrDefault(y => y.Code == year.Code);

                    if (existingYear != null)
                    {
                        existingYear.Name = year.Name;
                        existingYear.Code = year.Code;
                        existingYear.Model = year.Model;
                    }
                    else
                    {
                        _context.Years.Add(year);
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