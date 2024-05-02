using RealEstatePlatform.App.ViewModels;

namespace RealEstatePlatform.App.Contracts
{
    public interface IMLDataService
    {
        
        Task<MLOutputModel> Predict(MLInputModel inputModel);
    }
}
