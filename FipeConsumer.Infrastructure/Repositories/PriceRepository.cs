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
            return await _context.Prices.Include(p => p.Brand)
                                                     .Include(p => p.Model)
                                                     .Include(p => p.Year)
                                                     .FirstOrDefaultAsync(p => p.Brand != null && p.Brand.Code == brandCode &&
                                                                               p.Model != null && p.Model.Code == modelCode &&
                                                                               p.Year != null && p.Year.Code == yearCode);
        }

        public async Task UpsertPriceAsync(Price price, string brandCode, int modelCode, string yearCode)
        {
            var existingPrice = await GetSpecificPriceAsync(brandCode, modelCode, yearCode);

            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Code == brandCode) ?? throw new Exception("Brand not found.");
            var model = await _context.Models.FirstOrDefaultAsync(m => m.Code == modelCode) ?? throw new Exception("Model not found.");
            var year = await _context.Years.FirstOrDefaultAsync(y => y.Code == yearCode) ?? throw new Exception("Year not found.");

            price.SetBrandId(brand.BrandId);
            price.SetModelId(model.ModelId);
            price.SetYearId(year.YearId);          

            if (existingPrice == null) _context.Prices.Add(price);
            else
            {
                existingPrice.SetValue(price.Value!);
                existingPrice.SetBrandName(price.BrandName);
                existingPrice.SetModelName(price.ModelName);
                existingPrice.SetModelYear(price.ModelYear);
                existingPrice.SetFuel(price.Fuel);
                existingPrice.SetFipeCode(price.FipeCode);
                existingPrice.SetReferenceMonth(price.ReferenceMonth);
                existingPrice.SetFuelAbbreviation(price.FuelAbbreviation);
                existingPrice.SetBrandId(brand.BrandId);
                existingPrice.SetModelId(model.ModelId);
                existingPrice.SetYearId(year.YearId);
                
                _context.Prices.Update(existingPrice);
            }

            await _context.SaveChangesAsync();
        }
    }
}