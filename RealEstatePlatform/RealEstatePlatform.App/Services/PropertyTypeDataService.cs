using RealEstatePlatform.App.Contracts;
using RealEstatePlatform.App.Services.Responses;
using RealEstatePlatform.App.ViewModels;
using RealEstatePlatform.App.ViewModels.Dto;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealEstatePlatform.App.Services
{
    public class PropertyTypeDataService : IPropertyTypeDataService
    {
        private const string RequestUri = "api/v1/propertytypes";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public PropertyTypeDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }
        public async Task<ApiResponse<PropertyTypeDto>> CreatePropertyTypeAsync(PropertyTypeViewModel propertyType)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, propertyType);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<PropertyTypeDto>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return response!;
        }

        public async Task<PropertyTypeViewModel> GetPropertyTypeByIdAsync(Guid id)
        {
            var result = await httpClient.GetAsync($"{RequestUri}/{id}", HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var propertyType = JsonSerializer.Deserialize<PropertyTypeViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return propertyType!;
        }

        public async Task<List<PropertyTypeViewModel>> GetPropertyTypesAsync()
        {
            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();

            // Log or print the received JSON content
            Console.WriteLine("Received JSON content for Property Types:");
            Console.WriteLine(content);

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var jsonContent = JsonSerializer.Deserialize<JsonElement>(content);
            if (jsonContent.TryGetProperty("propertyTypes", out JsonElement propertyTypesElement) && propertyTypesElement.ValueKind == JsonValueKind.Array)
            {
                var propertyTypes = JsonSerializer.Deserialize<List<PropertyTypeViewModel>>(propertyTypesElement.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return propertyTypes!;
            }
            else
            {
                throw new JsonException("Invalid JSON format or missing 'propertyTypes' array.");
            }
        }

        public async Task<List<PropertyViewModel>> GetPropertiesAsync()
        {
            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();

            // Log or print the received JSON content
            Console.WriteLine("Received JSON content for Properties:");
            Console.WriteLine(content);

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var jsonContent = JsonSerializer.Deserialize<JsonElement>(content);
            if (jsonContent.TryGetProperty("properties", out JsonElement propertiesElement) && propertiesElement.ValueKind == JsonValueKind.Array)
            {
                var properties = JsonSerializer.Deserialize<List<PropertyViewModel>>(propertiesElement.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return properties!;
            }
            else
            {
                throw new JsonException("Invalid JSON format or missing 'properties' array.");
            }
        }

        public async Task<ApiResponse<PropertyTypeDto>> UpdatePropertyTypeAsync(PropertyTypeViewModel propertyType)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

                var json = JsonSerializer.Serialize(propertyType);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var result = await httpClient.PutAsync($"{RequestUri}/{propertyType.PropertyTypeId}", content);
                result.EnsureSuccessStatusCode();

                var response = await result.Content.ReadFromJsonAsync<ApiResponse<PropertyTypeDto>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return response!;
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request-related exceptions
                Console.WriteLine($"HTTP Request Exception: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
