using FipeConsumer.Application.Services;
using FipeConsumer.Domain.Interfaces;
using FipeConsumer.Infrastructure.Data;
using FipeConsumer.Infrastructure.ExternalServices;
using FipeConsumer.Infrastructure.Repositories;
using FipeConsumer.Jobs;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FipeConsumerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IModelRepository, ModelRepository>();
builder.Services.AddScoped<IYearRepository, YearRepository>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<FipeDataSyncService>();

builder.Services.AddHttpClient<FipeApiClient>();
builder.Services.AddTransient<IFipeUpsertJob, FipeUpsertJob>();

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
    {
        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
        QueuePollInterval = TimeSpan.FromSeconds(15),
        UseRecommendedIsolationLevel = true,
        DisableGlobalLocks = true
    }));

builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseHangfireDashboard("/hangfire");

RecurringJob.AddOrUpdate<IFipeUpsertJob>("fipe-upsert-job", job => job.ExecuteAsync(), Cron.Hourly);

app.Run();