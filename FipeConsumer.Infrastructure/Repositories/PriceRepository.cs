using Microsoft.EntityFrameworkCore;
using FipeConsumer.Domain.Entities;
using FipeConsumer.Domain.Interfaces;
using FipeConsumer.Infrastructure.Data;

namespace FipeConsumer.Infrastructure.Repositories
{
    public class PriceRepository(FipeConsumerDbContext context) : IPriceRepository
    {
        private readonly FipeConsumerDbContext _context = context;

        public async Task<List<Price>> GetAllPricesAsync()
        {
            return await _context.Prices.ToListAsync();
        }

        public async Task<Price?> GetSpecificPriceAsync(string brandCode, int modelCode, string yearCode)
        {
            return await _context.Prices.Include(m => m.Brand)
                                        .Include(m => m.Model)
                                        .Include(m => m.Year)
                                        .FirstOrDefaultAsync(m => m.Brand != null && m.Brand.Code == brandCode &&
                                                                  m.Model != null && m.Model.Code == modelCode &&
                                                                  m.Year != null && m.Year.Code == yearCode);
        }

        public async Task UpsertPriceAsync(Price price, string brandCode, int modelCode, string yearCode)
        {
            var existingPrice = await _context.Prices.Include(p => p.Brand)
                                                     .Include(p => p.Model)
                                                     .Include(p => p.Year)
                                                     .FirstOrDefaultAsync(p => p.Brand != null && p.Brand.Code == brandCode &&
                                                                               p.Model != null && p.Model.Code == modelCode &&
                                                                               p.Year != null && p.Year.Code == yearCode);

            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Code == brandCode) ?? throw new Exception("Brand not found.");
            var model = await _context.Models.FirstOrDefaultAsync(m => m.Code == modelCode) ?? throw new Exception("Model not found.");
            var year = await _context.Years.FirstOrDefaultAsync(y => y.Code == yearCode) ?? throw new Exception("Year not found.");

            price.Brand = brand;
            price.Model = model;
            price.Year = year;

            if (existingPrice == null) _context.Prices.Add(price);
            else
            {
                Price.CopyProperties(price, existingPrice);
                _context.Prices.Update(existingPrice);
            }

            await _context.SaveChangesAsync();
        }
    }
}