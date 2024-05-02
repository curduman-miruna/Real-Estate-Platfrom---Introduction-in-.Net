using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RealEstatePlatform.App;
using RealEstatePlatform.App.Auth;
using RealEstatePlatform.App.Contracts;
using RealEstatePlatform.App.Services;
using RealEstatePlatform.TicketManagement.App.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
    config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
    config.JsonSerializerOptions.WriteIndented = false;
});
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<CustomStateProvider>();
builder.Services.AddBlazorise()
        .AddBootstrapProviders()
        .AddFontAwesomeIcons();
builder.Services.AddHttpClient<IPropertyDataService, PropertyDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7052/");
});
builder.Services.AddHttpClient<IPropertyTypeDataService, PropertyTypeDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7052/");
});
builder.Services.AddHttpClient<IListingTypeDataService, ListingTypeDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7052/");
});
builder.Services.AddHttpClient<IMessageDataService, MessageDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7052/");
});
builder.Services.AddHttpClient<IImageDataService, ImageDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7052/");
});
builder.Services.AddHttpClient<IMLDataService, MLDataService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7052/");
});
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7052");
});

await builder.Build().RunAsync();
