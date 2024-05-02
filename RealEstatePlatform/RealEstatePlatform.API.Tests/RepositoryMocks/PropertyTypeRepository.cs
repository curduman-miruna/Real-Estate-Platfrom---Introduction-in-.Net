using NSubstitute;
using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Common;
using RealEstatePlatform.Domain.Entities;

namespace RealEstatePlatform.Application.Tests.RepositoryMocks
{
    public static class PropertyTypeRepository
    {
        internal readonly static List<PropertyType> PropertyTypes =
        [
            PropertyType.Create("Twin House").Value,
            PropertyType.Create("Condo").Value
        ];

        public static IPropertyTypeRepository GetPropertyTypeRepository()
        {
            var mockPropertyTypeRepository = NSubstitute.Substitute.For<IPropertyTypeRepository>();
            mockPropertyTypeRepository.GetAllAsync().Returns(Result<IReadOnlyList<PropertyType>>.Success(PropertyTypes));
            mockPropertyTypeRepository.FindByIdAsync(PropertyTypes[0].PropertyTypeId).Returns(Result<PropertyType>.Success(PropertyTypes[0]));
            mockPropertyTypeRepository.FindByIdAsync(PropertyTypes[1].PropertyTypeId).Returns(Result<PropertyType>.Success(PropertyTypes[1]));
            mockPropertyTypeRepository.FindByIdAsync(Arg.Is<Guid>(id => id != PropertyTypes[0].PropertyTypeId && id != PropertyTypes[1].PropertyTypeId)).Returns(Result<PropertyType>.Failure("PropertyType not found."));
            return mockPropertyTypeRepository;
        }
    }
}
