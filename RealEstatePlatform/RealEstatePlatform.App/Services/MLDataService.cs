using RealEstatePlatform.App.Contracts;
using RealEstatePlatform.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace RealEstatePlatform.App.Services
{
    public class MLDataService : IMLDataService
    {
        private const string RequestUri = "prediction";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public MLDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;

        }

        public async Task<MLOutputModel> Predict(MLInputModel inputModel)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, inputModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<MLOutputModel>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return response!;
        }
    }
}
