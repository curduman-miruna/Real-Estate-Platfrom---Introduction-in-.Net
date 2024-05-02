using RealEstatePlatform.App.Contracts;
using RealEstatePlatform.App.Services.Responses;
using RealEstatePlatform.App.ViewModels;
using RealEstatePlatform.App.ViewModels.Dto;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace RealEstatePlatform.App.Services
{
    public class PropertyDataService : IPropertyDataService
    {
        private const string RequestUri = "api/v1/properties";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public PropertyDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }
        public async Task<ApiResponse<PropertyDto>> CreatePropertyAsync(PropertyViewModel propertyViewModel)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

                // Serialize PropertyViewModel to JSON for inspection
                var serializedPayload = JsonSerializer.Serialize(propertyViewModel);
                Console.WriteLine($"JSON Payload: {serializedPayload}");

                var response = await httpClient.PostAsJsonAsync(RequestUri, propertyViewModel);

                // Check if the response indicates an error
                if (!response.IsSuccessStatusCode)
                {
                    // Capture error details if the response is not successful
                    var errorContent = await response.Content.ReadAsStringAsync();
                    // You can log or handle the error content here
                    Console.WriteLine($"Error response content: {errorContent}");

                    // Throw an exception or handle the error based on the error content received
                    throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
                }
                // Read the JSON response as a string
                var jsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Received JSON string: {jsonString}");
                var apiResponse = new ApiResponse<PropertyDto>();
                var jsonContent = JsonSerializer.Deserialize<JsonElement>(jsonString);

                if (jsonContent.TryGetProperty("property", out JsonElement propertyElement))
                {
                    apiResponse.Data = JsonSerializer.Deserialize<PropertyDto>(propertyElement.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                }
                if(jsonContent.TryGetProperty("message", out JsonElement messageElement))
                {
                    apiResponse.Message = messageElement.GetString();
                }
                if(jsonContent.TryGetProperty("validationsErrors", out JsonElement validationsErrorsElement))
                {
                    apiResponse.ValidationErrors = validationsErrorsElement.GetString();
                }
                if(jsonContent.TryGetProperty("success", out JsonElement successElement))
                {
                    apiResponse.IsSuccess = successElement.GetBoolean();
                }
                Console.WriteLine($"Deserialized property edit: {apiResponse.Data.PropertyId}");
                Console.WriteLine($"Deserialized property edit: {apiResponse.IsSuccess}");
                return apiResponse;
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
            return new ApiResponse<PropertyDto>();
        }



        public async Task<bool> DeletePropertyAsync(Guid id)
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



        public async Task<List<PropertyViewModel>> GetPropertiesAsync()
        {
            var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            Console.WriteLine($"Received JSON content: {content}");

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


        public async Task<PropertyViewModel> GetPropertyByIdAsync(Guid id)
        {
            try
            {
                var result = await httpClient.GetAsync($"{RequestUri}/{id}", HttpCompletionOption.ResponseHeadersRead);

                // Check if the HTTP response is successful (status code 200-299)
                result.EnsureSuccessStatusCode();

                var content = await result.Content.ReadAsStringAsync();
                Console.WriteLine($"Received JSON content: {content}");

                // Deserialize JSON into PropertyViewModel
                var property = JsonSerializer.Deserialize<PropertyViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (property == null)
                {
                    // Handle if deserialization fails
                    throw new ApplicationException("Failed to deserialize JSON into PropertyViewModel");
                }

                Console.WriteLine($"Deserialized property: {property}");

                return property;
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


        public async Task<ApiResponse<PropertyDto>> UpdatePropertyAsync(PropertyViewModel property)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());

                var json = JsonSerializer.Serialize(property);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await httpClient.PutAsync($"{RequestUri}/{property.PropertyId}", content);

                result.EnsureSuccessStatusCode();

                var response = await result.Content.ReadFromJsonAsync<ApiResponse<PropertyDto>>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

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
