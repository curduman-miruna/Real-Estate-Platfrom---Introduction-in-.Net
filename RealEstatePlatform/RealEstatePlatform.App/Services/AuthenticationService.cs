using Microsoft.JSInterop;
using RealEstatePlatform.App.Contracts;
using RealEstatePlatform.App.ViewModels;
using System.Net.Http.Json;
using System.Text.Json;
using RealEstatePlatform.App.ViewModels;
using RealEstatePlatform.API.Models;
using System.Text;

namespace RealEstatePlatform.TicketManagement.App.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public AuthenticationService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public async Task Login(LoginViewModel loginRequest, IJSRuntime jsRuntime)
        {
            var response = await httpClient.PostAsJsonAsync("api/v1/authentication/login", loginRequest);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
            response.EnsureSuccessStatusCode();
            var token = await response.Content.ReadAsStringAsync();
            string[] tokenParts = token.Split('.');
            string decodedPayload = Encoding.UTF8.GetString(ParseBase64WithoutPadding(tokenParts[1]));
            using JsonDocument document = JsonDocument.Parse(decodedPayload);
            JsonElement root = document.RootElement;
            string email = root.GetProperty("email").GetString();
            string fullName = root.GetProperty("Full Name").GetString();
            string uniqueName = root.GetProperty("unique_name").GetString();
            await tokenService.SetTokenAsync(token);

            if (email != null && fullName != null && uniqueName != null)
            {
                Console.WriteLine($"User {fullName} is authenticated.");

                // Save user details to local storage (simulated)
                SaveCurrentUserInLocalStorage(email,uniqueName,fullName , jsRuntime);
            }
        }
        private async Task SaveCurrentUserInLocalStorage(string email,string unique_name, string fullName, IJSRuntime jsRuntime)
        {
            try
            {
                var user = new
                {
                    Email = email,
                    UniqueName = unique_name,
                    FullName = fullName,
                };
                string userJson = JsonSerializer.Serialize(user);

                // Save serialized user info in local storage
                await jsRuntime.InvokeVoidAsync("localStorage.setItem", "currentUser", userJson);
            }
            catch (Exception ex)
            {
                // Handle serialization or storage error
                Console.WriteLine($"Error saving user to local storage: {ex.Message}");
            }
        }
        public async Task Logout()
        {
            await tokenService.RemoveTokenAsync();
            var result = await httpClient.PostAsync("api/v1/authentication/logout", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Register(RegisterViewModel registerRequest)
        {
            var result = await httpClient.PostAsJsonAsync("api/v1/authentication/register", registerRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
            result.EnsureSuccessStatusCode();
        }

        public async Task RegisterAgent(RegisterViewModel registerRequest)
        {
            var result = await httpClient.PostAsJsonAsync("api/v1/authentication/register-agent", registerRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
            result.EnsureSuccessStatusCode();
        }

        public async Task UpdateInformation(UpdateInformationViewModel updateInformationRequest)
        {
            var result = await httpClient.PutAsJsonAsync("/api/v1/Authentication/update-information", updateInformationRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
            result.EnsureSuccessStatusCode();
        }
    }
}
