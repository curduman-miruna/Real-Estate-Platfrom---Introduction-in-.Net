using RealEstatePlatform.App.Services.Responses;
using RealEstatePlatform.App.ViewModels;
using RealEstatePlatform.App.ViewModels.Dto;

namespace RealEstatePlatform.App.Contracts
{
    public interface IPropertyDataService
    {
        Task<List<PropertyViewModel>> GetPropertiesAsync();

        Task<PropertyViewModel> GetPropertyByIdAsync(Guid id);
        Task<ApiResponse<PropertyDto>> CreatePropertyAsync(PropertyViewModel property);
        
        Task<ApiResponse<PropertyDto>> UpdatePropertyAsync(PropertyViewModel property);

        Task<bool> DeletePropertyAsync(Guid id);
    }
}
