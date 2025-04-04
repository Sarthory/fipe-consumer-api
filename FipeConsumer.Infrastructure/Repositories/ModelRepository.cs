using Microsoft.EntityFrameworkCore;
using FipeConsumer.Domain.Entities;
using FipeConsumer.Domain.Interfaces;
using FipeConsumer.Infrastructure.Data;

namespace FipeConsumer.Infrastructure.Repositories
{
    public class ModelRepository(FipeConsumerDbContext context) : IModelRepository
    {
        private readonly FipeConsumerDbContext _context = context;

        public async Task<List<Model>> GetAllModelsAsync()
        {
            return await _context.Models.ToListAsync();
        }

        public async Task<List<Model>> GetModelsByBrandCodeAsync(string brandCode)
        {
            return await _context.Models
                .Include(m => m.Brand)
                .Where(m => m.Brand != null && m.Brand.Code == brandCode)
                .ToListAsync();
        }

        public async Task UpsertModelAsync(Model model, string brandCode)
        {
            var existingModel = await _context.Models.FirstOrDefaultAsync(m => m.Code == model.Code);
            var brand = await _context.Brands.FirstOrDefaultAsync(m => m.Code == brandCode) ?? throw new Exception("Brand not found");
            model.SetBrandId(brand.BrandId);

            if (existingModel == null) _context.Models.Add(model);
            else
            {
                existingModel.SetCode(model.Code);
                existingModel.SetName(model.Name);
                existingModel.SetBrandId(brand.BrandId);

                _context.Models.Update(existingModel);
            }

            await _context.SaveChangesAsync();
        }

        public async Task BatchUpsertModelsAsync(List<Model> models, string brandCode)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var existingModels = await _context.Models.ToListAsync();

                var brand = await _context.Brands.FirstOrDefaultAsync(m => m.Code == brandCode) ?? throw new Exception("Brand not found.");

                foreach (var model in models)
                {
                    model.SetBrandId(brand.BrandId);
                    var existingModel = existingModels?.FirstOrDefault(m => m.Code == model.Code);

                    if (existingModel == null) _context.Models.Add(model);
                    else
                    {
                        existingModel.SetCode(model.Code);
                        existingModel.SetName(model.Name);
                        existingModel.SetBrandId(brand.BrandId);                       

                        _context.Models.Update(existingModel);
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Error trying to batch upsert models. {ex.Message}", ex);
            }
        }
    }
}