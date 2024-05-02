using RealEstatePlatform.App.Services.Responses;
using RealEstatePlatform.App.ViewModels;
using RealEstatePlatform.App.ViewModels.Dto;

namespace RealEstatePlatform.App.Contracts
{
    public interface IListingTypeDataService
    {
        Task<List<ListingTypeViewModel>> GetListingTypesAsync();

        Task<ListingTypeViewModel> GetListingTypeByIdAsync(Guid id);

        Task<ApiResponse<ListingTypeDto>> CreateListingTypeAsync(ListingTypeViewModel listingType);

        Task<ApiResponse<ListingTypeDto>> UpdateListingTypeAsync(ListingTypeViewModel listingType);
    }
}
