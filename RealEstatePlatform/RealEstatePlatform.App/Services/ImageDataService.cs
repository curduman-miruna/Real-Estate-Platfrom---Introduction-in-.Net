using RealEstatePlatform.App.Contracts;
using RealEstatePlatform.App.Services.Responses;
using RealEstatePlatform.App.ViewModels;
using RealEstatePlatform.App.ViewModels.Dto;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealEstatePlatform.App.Services
{
    public class ImageDataService : IImageDataService
    {
        private const string RequestUri = "api/v1/images";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ImageDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }
        public async Task<ApiResponse<ImageDto>> CreateImageAsync(ImageViewModel image)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, image);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ImageDto>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return response!;
        }

        public async Task<bool> DeleteImageAsync(Guid id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

                var result = await httpClient.DeleteAsync($"{RequestUri}/{id}");

                result.EnsureSuccessStatusCode();

                var responseContent = await result.Content.ReadAsStringAsync();
                Console.WriteLine($"Received JSON content: {responseContent}");
                return true;

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

        public async Task<List<ImageViewModel>> GetImagesAsync()
        {
            var RequestUri = "api/v1/images";
            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine($"Received JSON content: {content}");

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var jsonContent = JsonSerializer.Deserialize<JsonElement>(content);
            if (jsonContent.TryGetProperty("images", out JsonElement propertiesElement) && propertiesElement.ValueKind == JsonValueKind.Array)
            {
                var images = JsonSerializer.Deserialize<List<ImageViewModel>>(propertiesElement.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return images!;
            }
            else
            {
                throw new JsonException("Invalid JSON format or missing 'properties' array.");
            }
        }

        public async Task<List<ImageViewModel>> GetImagesByPropertyIdAsync(Guid id)
        {

            var RequestUri = $"propertyId/{id}";
            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine($"Received JSON content: {content}");

            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var jsonContent = JsonSerializer.Deserialize<JsonElement>(content);
            if (jsonContent.TryGetProperty("images", out JsonElement propertiesElement) && propertiesElement.ValueKind == JsonValueKind.Array)
            {
                var images = JsonSerializer.Deserialize<List<ImageViewModel>>(propertiesElement.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return images!;
            }
            else
            {
                throw new JsonException("Invalid JSON format or missing 'properties' array.");
            }
        }
    }
}
