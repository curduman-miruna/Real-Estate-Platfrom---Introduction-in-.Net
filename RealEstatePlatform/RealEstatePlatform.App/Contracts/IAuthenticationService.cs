using Microsoft.JSInterop;
using RealEstatePlatform.App.ViewModels;

namespace RealEstatePlatform.App.Contracts
{
    public interface IAuthenticationService
    {
        Task Login(LoginViewModel loginRequest, IJSRuntime jsRuntime);
        Task Register(RegisterViewModel registerRequest);
        Task RegisterAgent(RegisterViewModel registerRequest);
        Task Logout();

        Task UpdateInformation(UpdateInformationViewModel updateInformationRequest);
    }
}
