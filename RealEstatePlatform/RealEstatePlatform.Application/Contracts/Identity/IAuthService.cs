using RealEstatePlatform.Application.Models.Identity;

namespace RealEstatePlatform.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);

        Task<bool> UpdateInformation(UpdateInformationModel model);
        Task<(int, string)> Logout();
    }
}
