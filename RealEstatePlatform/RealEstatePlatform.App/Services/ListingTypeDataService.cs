using RealEstatePlatform.App.Contracts;
using RealEstatePlatform.App.Services.Responses;
using RealEstatePlatform.App.ViewModels;
using RealEstatePlatform.App.ViewModels.Dto;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealEstatePlatform.App.Services
{
    public class ListingTypeDataService : IListingTypeDataService
    {
        private const string RequestUri = "api/v1/listingtypes";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ListingTypeDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }
        public async Task<ApiResponse<ListingTypeDto>> CreateListingTypeAsync(ListingTypeViewModel listingType)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, listingType);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ListingTypeDto>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return response!;
        }

        public async Task<ListingTypeViewModel> GetListingTypeByIdAsync(Guid id)
        {
            var result = await httpClient.GetAsync($"{RequestUri}/{id}", HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var listingtype = JsonSerializer.Deserialize<ListingTypeViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return listingtype!;
        }

        public async Task<List<ListingTypeViewModel>> GetListingTypesAsync()
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
            if (jsonContent.TryGetProperty("listingTypes", out JsonElement propertyTypesElement) && propertyTypesElement.ValueKind == JsonValueKind.Array)
            {
                var listingTypes = JsonSerializer.Deserialize<List<ListingTypeViewModel>>(propertyTypesElement.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return listingTypes!;
            }
            else
            {
                throw new JsonException("Invalid JSON format or missing 'listingTypes' array.");
            }
        }

        public async Task<ApiResponse<ListingTypeDto>> UpdateListingTypeAsync(ListingTypeViewModel listingType)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

                var json = JsonSerializer.Serialize(listingType);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var result = await httpClient.PutAsync($"{RequestUri}/{listingType.ListingTypeId}", content);
                result.EnsureSuccessStatusCode();

                var response = await result.Content.ReadFromJsonAsync<ApiResponse<ListingTypeDto>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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
