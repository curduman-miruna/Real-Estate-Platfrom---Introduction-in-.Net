using RealEstatePlatform.Application.Contracts;
using RealEstatePlatform.Domain.Entities;

namespace RealEstatePlatform.Application.Persistence
{
    public interface IListingTypeRepository : IAsyncRepository<ListingType>
    {
        Task<bool> IsListingTypeNameUnique(string listingTypeName);
    }
}
