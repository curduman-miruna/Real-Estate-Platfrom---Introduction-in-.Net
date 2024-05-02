using Infrastructure.Repositories;
using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Entities;

namespace RealEstatePlatform.Infrastructure.Repositories
{
    public class PropertyTypeRepository : BaseRepository<PropertyType>, IPropertyTypeRepository
    {
        public PropertyTypeRepository(RealEstatePlatformContext context) : base(context)
        {
        }

        public Task<bool> IsPropertyTypeNameUnique(string propertyTypeName)
        {
            var matches = context.PropertyTypes.Any(pt => pt.PropertyTypeName == propertyTypeName);
            return Task.FromResult(matches);
        }
    }
}
