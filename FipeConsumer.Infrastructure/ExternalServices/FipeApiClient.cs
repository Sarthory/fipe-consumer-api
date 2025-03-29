using System.Net.Http.Headers;
using System.Net.Http.Json;
using FipeConsumer.Domain.Entities;

namespace FipeConsumer.Infrastructure.ExternalServices
{
    public class FipeApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://parallelum.com.br/fipe/api/v1/carros/";
        private readonly string _token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI2NTViYTBhOS03OWQ4LTQzMjgtYjcwMS1hMzg3NjljZWQ4NTgiLCJlbWFpbCI6ImZlbGlwZUBzYXJ0b3JpLmFwcCIsInN0cmlwZVN1YnNjcmlwdGlvbklkIjoic3ViXzFSN2VOV0NTdklzMDh0SUVZT0VWS3JDNiIsImlhdCI6MTc0MzE3Mzk5N30.b5BncmVrvgPSER4rISPZbYGBzpY_boLnAV67PDkNjMA";

        public FipeApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Add("X-Subscription-Token", _token);
        }

        public async Task<List<Brand>> GetBrandsAsync()
        {
            try
            {
                await Task.Delay(100);
                var result = await _httpClient.GetFromJsonAsync<List<Brand>>($"{_baseUrl}marcas");
                return result ?? [];
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
                var res = await _httpClient.GetFromJsonAsync<ModelResponse>($"{_baseUrl}marcas/{brandCode}/modelos");
                var models = res?.Models;
                return models ?? [];
            }
            catch (Exception ex)
            {
                throw new Exception($"Error trying to acquire models. {ex.Message}", ex);
            }
        }

        public async Task<List<Year>> GetBrandModelYearsAsync(string brandCode, int modelCode)
        {
            try
            {
                await Task.Delay(100);
                var result = await _httpClient.GetFromJsonAsync<List<Year>>($"{_baseUrl}marcas/{brandCode}/modelos/{modelCode}/anos");
                return result ?? [];
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
                var result = await _httpClient.GetFromJsonAsync<Price>($"{_baseUrl}marcas/{brandCode}/modelos/{modelCode}/anos/{yearCode}");
                return result!;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error trying to acquire prices. {ex.Message}", ex);
            }
        }
    }
}