using System.Net.Http.Json;
 using FipeConsumer.Domain.Dtos;
 using FipeConsumer.Domain.Entities;
 using FipeConsumer.Infrastructure.Configuration;

 namespace FipeConsumer.Infrastructure.ExternalServices
 {
     public class FipeApiClient
     {
         private readonly HttpClient _httpClient;
         private readonly FipeApiConfig _config;

         public FipeApiClient(HttpClient httpClient, FipeApiConfig config)
         {
             _httpClient = httpClient;
             _config = config;
         }

         public async Task<List<Brand>> GetBrandsAsync()
         {
             try
             {
                 await Task.Delay(100);
                 var result = await _httpClient.GetFromJsonAsync<List<BrandDto>>($"marcas");

                 var brands = result?.Select(b => new Brand(b.Code, b.Name)).ToList();

                 return brands ?? [];
             }
             catch (Exception ex)
             {
                 throw new Exception($"Error trying to acquire brands. {ex.Message}", ex);
             }
         }

         public async Task<List<Model>> GetBrandModelsAsync(string brandCode)
         {
             try
             {
                 await Task.Delay(100);
                 var res = await _httpClient.GetFromJsonAsync<ModelResponse>($"marcas/{brandCode}/modelos");

                 var models = res.Models.Select(m => new Model(m.Code, m.Name)).ToList();

                 return models ?? [];
             }
             catch (Exception ex)
             {
                 throw new Exception($"Error trying to acquire models. {ex.Message}", ex);
             }
         }

         public async Task<List<Year>> GetBrandModelYearsAsync(string brandCode, int? modelCode)
         {
             try
             {
                 await Task.Delay(100);
                 var result = await _httpClient.GetFromJsonAsync<List<YearDto>>($"marcas/{brandCode}/modelos/{modelCode}/anos");

                 var years = result?.Select(y => new Year(y.Code, y.Name)).ToList();

                 return years ?? [];
             }
             catch (Exception ex)
             {
                 throw new Exception($"Error trying ot acquire years. {ex.Message}", ex);
             }
         }

         public async Task<Price> GetBrandModelYearPricesAsync(string brandCode, int modelCode, string yearCode)
         {
             try
             {
                 await Task.Delay(100);
                 var result = await _httpClient.GetFromJsonAsync<PriceDto>($"marcas/{brandCode}/modelos/{modelCode}/anos/{yearCode}");

                 var price = new Price(
                     value: result!.Value,
                     brandName: result!.BrandName,
                     modelName: result!.ModelName,
                     modelYear: result!.ModelYear,
                     fuel: result!.Fuel,
                     fipeCode: result!.FipeCode,
                     referenceMonth: result!.ReferenceMonth,
                     fuelAbbreviation: result!.FuelAbbreviation
                 );

                 return price;
             }
             catch (Exception ex)
             {
                 throw new Exception($"Error trying to acquire prices. {ex.Message}", ex);
             }
         }
     }
 }