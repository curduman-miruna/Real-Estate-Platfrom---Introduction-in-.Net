using RealEstatePlatform.App.Services.Responses;
using RealEstatePlatform.App.ViewModels;
using RealEstatePlatform.App.ViewModels.Dto;

namespace RealEstatePlatform.App.Contracts
{
    public interface IMessageDataService
    {
        Task<List<MessageViewModel>> GetMessagesAsync();

        Task<Boolean> CreateMessageAsync(MessageViewModel message);
    }
}
