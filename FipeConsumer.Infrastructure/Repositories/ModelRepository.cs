using Microsoft.EntityFrameworkCore;
using FipeConsumer.Domain.Entities;
using FipeConsumer.Domain.Interfaces;
using FipeConsumer.Infrastructure.Data;

namespace FipeConsumer.Infrastructure.Repositories
{
    public class ModelRepository(FipeConsumerDbContext context) : IModelRepository
    {
        private readonly FipeConsumerDbContext _context = context;

        public async Task<IEnumerable<Model>> GetAllModelsAsync()
        {
            return await _context.Models.ToListAsync();
        }

        public async Task<Model?> GetModelByCodeAsync(int modelCode)
        {
            return await _context.Models.FirstOrDefaultAsync(m => m.Code == modelCode);
        }

        public async Task UpsertModelAsync(Model model, string brandCode)
        {
            var existingModel = await _context.Models.FirstOrDefaultAsync(m => m.Code == model.Code);
            var brand = await _context.Brands.FirstOrDefaultAsync(m => m.Code == brandCode) ?? throw new Exception("Brand not found");
            model.Brand = brand;

            if (existingModel == null) _context.Models.Add(model);
            else
            {
                existingModel.Name = model.Name;
                existingModel.Code = model.Code;
                existingModel.Brand = model.Brand;
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
                    model.Brand = brand;
                    var existingModel = existingModels?.FirstOrDefault(m => m.Code == model.Code);

                    if (existingModel != null)
                    {
                        existingModel.Name = model.Name;
                        existingModel.Code = model.Code;
                        existingModel.Brand = model.Brand;
                    }
                    else
                    {
                        _context.Models.Add(model);
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