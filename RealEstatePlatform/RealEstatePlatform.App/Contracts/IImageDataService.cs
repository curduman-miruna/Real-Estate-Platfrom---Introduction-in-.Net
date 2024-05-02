using RealEstatePlatform.App.Services.Responses;
using RealEstatePlatform.App.ViewModels;
using RealEstatePlatform.App.ViewModels.Dto;

namespace RealEstatePlatform.App.Contracts
{
    public interface IImageDataService
    {
        Task<List<ImageViewModel>> GetImagesAsync();

        Task<List<ImageViewModel>> GetImagesByPropertyIdAsync(Guid id);

        Task<ApiResponse<ImageDto>> CreateImageAsync(ImageViewModel image);

        Task <bool> DeleteImageAsync(Guid id);

    }
}
