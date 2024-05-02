using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Common;
using RealEstatePlatform.Domain.Entities;

namespace RealEstatePlatform.Infrastructure.Repositories
{
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        public PropertyRepository(RealEstatePlatformContext context) : base(context)
        {
        }

        public Task<bool> DoesListingTypeExists(Guid listingTypeId)
        {
            var matches = context.ListingTypes.Any(p=>p.ListingTypeId == listingTypeId);
            return Task.FromResult(matches);
        }

        public Task<bool> DoesPropertyTypeExists(Guid propertyTypeId)
        {
            var matches = context.PropertyTypes.Any(p=> p.PropertyTypeId == propertyTypeId);
            return Task.FromResult(matches);
        }

        public Task<bool> IsPropertyNameAndLocationUnique(string propertyName, string location)
        {
            var matches = context.Properties.Any(p => p.PropertyName == propertyName && p.Location == location);
            return Task.FromResult(matches);
        }
    }
}
