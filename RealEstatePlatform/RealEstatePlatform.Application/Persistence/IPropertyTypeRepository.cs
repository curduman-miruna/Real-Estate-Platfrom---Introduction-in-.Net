using RealEstatePlatform.Application.Contracts;
using RealEstatePlatform.Domain.Entities;

namespace RealEstatePlatform.Application.Persistence
{
    public interface IPropertyTypeRepository : IAsyncRepository<PropertyType>
    {
        Task<bool> IsPropertyTypeNameUnique(string propertyTypeName);
    }
}
