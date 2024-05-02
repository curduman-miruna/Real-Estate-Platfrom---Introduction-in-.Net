using RealEstatePlatform.Application.Models;

namespace RealEstatePlatform.Application.Contracts
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(Mail email);
    }
}
