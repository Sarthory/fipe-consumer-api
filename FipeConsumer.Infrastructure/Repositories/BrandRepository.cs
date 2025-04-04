using Microsoft.EntityFrameworkCore;
using FipeConsumer.Domain.Entities;
using FipeConsumer.Domain.Interfaces;
using FipeConsumer.Infrastructure.Data;

namespace FipeConsumer.Infrastructure.Repositories
{
    public class BrandRepository(FipeConsumerDbContext context) : IBrandRepository
    {
        private readonly FipeConsumerDbContext _context = context;

        public async Task<List<Brand>> GetAllBrandsAsync()
        {
            return await _context.Brands.ToListAsync();
        }

        public async Task UpsertBrandAsync(Brand brand)
        {
            var existingBrand = await _context.Brands.FirstOrDefaultAsync(b => b.Code == brand.Code);

            if (existingBrand == null) _context.Brands.Add(brand);
            else
            {
                existingBrand.SetCode(brand.Code);
                existingBrand.SetName(brand.Name);
                
                _context.Brands.Update(existingBrand);
            }

            await _context.SaveChangesAsync();
        }

        public async Task BatchUpsertBrandsAsync(List<Brand> brands)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingBrands = await _context.Brands.ToListAsync();

                foreach (var brand in brands)
                {
                    var existingBrand = existingBrands?.FirstOrDefault(b => b.Code == brand.Code);

                    if (existingBrand == null) _context.Brands.Add(brand);
                    else
                    {
                        existingBrand.SetCode(brand.Code);
                        existingBrand.SetName(brand.Name);                       

                        _context.Brands.Update(existingBrand);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error trying to batch upsert brands. {ex.Message}", ex);
            }
        }
    }
}
