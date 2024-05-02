using RealEstatePlatform.App.Services.Responses;
using RealEstatePlatform.App.ViewModels;
using RealEstatePlatform.App.ViewModels.Dto;

namespace RealEstatePlatform.App.Contracts
{
    public interface IPropertyTypeDataService
    {
        Task<List<PropertyTypeViewModel>> GetPropertyTypesAsync();

        Task<PropertyTypeViewModel> GetPropertyTypeByIdAsync(Guid id);

        Task<ApiResponse<PropertyTypeDto>> CreatePropertyTypeAsync(PropertyTypeViewModel propertyType);

        Task<ApiResponse<PropertyTypeDto>> UpdatePropertyTypeAsync(PropertyTypeViewModel propertyType);
    }
}
