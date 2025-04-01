using Microsoft.EntityFrameworkCore;
using FipeConsumer.Infrastructure.Data;
using FipeConsumer.Infrastructure.Repositories;
using FipeConsumer.Domain.Interfaces;
using FipeConsumer.Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FipeConsumerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IModelRepository, ModelRepository>();
builder.Services.AddScoped<IYearRepository, YearRepository>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();

builder.Services.AddScoped<BrandService>();
builder.Services.AddScoped<ModelService>();
builder.Services.AddScoped<YearService>();
builder.Services.AddScoped<PriceService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
