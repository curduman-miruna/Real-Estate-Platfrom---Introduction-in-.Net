using RealEstatePlatform.App.Contracts;
using RealEstatePlatform.App.Services.Responses;
using RealEstatePlatform.App.ViewModels;
using RealEstatePlatform.App.ViewModels.Dto;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealEstatePlatform.App.Services
{
    public class MessageDataService : IMessageDataService
    {

        private const string RequestUri = "api/v1/messages";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public MessageDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }
        public async Task<Boolean> CreateMessageAsync(MessageViewModel message)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var response = await httpClient.PostAsJsonAsync(RequestUri, message);
            Console.WriteLine($"Received JSON content for Property Types:{response.Content}");
            return true;
        }

        public async Task<List<MessageViewModel>> GetMessagesAsync()
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
            if (jsonContent.TryGetProperty("messages", out JsonElement propertyTypesElement) && propertyTypesElement.ValueKind == JsonValueKind.Array)
            {
                var messages = JsonSerializer.Deserialize<List<MessageViewModel>>(propertyTypesElement.GetRawText(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return messages!;
            }
            else
            {
                throw new JsonException("Invalid JSON format or missing 'messages' array.");
            }
        }
    }
}
