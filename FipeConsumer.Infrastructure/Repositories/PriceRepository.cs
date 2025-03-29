using Microsoft.EntityFrameworkCore;
using FipeConsumer.Domain.Entities;
using FipeConsumer.Domain.Interfaces;
using FipeConsumer.Infrastructure.Data;

namespace FipeConsumer.Infrastructure.Repositories
{
    public class PriceRepository(FipeConsumerDbContext context) : IPriceRepository
    {
        private readonly FipeConsumerDbContext _context = context;

        public async Task<IEnumerable<Price>> GetAllPricesAsync()
        {
            return await _context.Prices.ToListAsync();
        }

        public async Task<Price?> GetSpecificPriceAsync(Brand brand, Model model, Year year)
        {
            return await _context.Prices.FirstOrDefaultAsync(m => m.Brand == brand && m.Model == model && m.Year == year);
        }

        public async Task UpsertPriceAsync(Price price, string brandCode, int modelCode, string yearCode)
        {
            var existingPrice = await _context.Prices.FirstOrDefaultAsync(m =>
                                                                          m.FipeCode == price.FipeCode &&
                                                                          m.Model == price.Model &&
                                                                          m.Brand == price.Brand) ?? null;

            var brand = await _context.Brands.FirstOrDefaultAsync(m => m.Code == brandCode) ?? throw new Exception("Brand not found.");
            var model = await _context.Models.FirstOrDefaultAsync(m => m.Code == modelCode) ?? throw new Exception("Model not found.");
            var year = await _context.Years.FirstOrDefaultAsync(m => m.Code == yearCode) ?? throw new Exception("Year not found.");

            price.Brand = brand;
            price.Model = model;
            price.Year = year;

            if (existingPrice == null)
                _context.Prices.Add(price);
            else
            {
                existingPrice.Value = price.Value;
                existingPrice.FipeCode = price.FipeCode;
                existingPrice.Brand = price.Brand;
                existingPrice.Model = price.Model;
                existingPrice.Year = price.Year;
            }

            await _context.SaveChangesAsync();
        }
    }
}