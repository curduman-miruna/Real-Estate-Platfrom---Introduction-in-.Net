using Infrastructure.Repositories;
using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Entities;

namespace RealEstatePlatform.Infrastructure.Repositories
{
    public class ListingTypeRepository : BaseRepository<ListingType>, IListingTypeRepository
    {
        public ListingTypeRepository(RealEstatePlatformContext dbContext) : base(dbContext)
        {
        }


        public Task<bool> IsListingTypeNameUnique(string listingTypeName)
        {
            var matches = context.ListingTypes.Any(lt => lt.ListingTypeName == listingTypeName);
            return Task.FromResult(matches);
        }
    }
}
