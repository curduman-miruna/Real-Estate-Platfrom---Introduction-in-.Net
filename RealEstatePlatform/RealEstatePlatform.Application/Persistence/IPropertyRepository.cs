using RealEstatePlatform.Application.Contracts;
using RealEstatePlatform.Domain.Entities;

namespace RealEstatePlatform.Application.Persistence
{
    public interface IPropertyRepository : IAsyncRepository<Property>
    {
        Task<bool> IsPropertyNameAndLocationUnique(string propertyName, string location);

        Task<bool> DoesListingTypeExists(Guid listingTypeId);

        Task<bool> DoesPropertyTypeExists(Guid propertyTypeId);

    }
}
