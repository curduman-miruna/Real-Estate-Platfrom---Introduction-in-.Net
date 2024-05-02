using NSubstitute;
using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Common;
using RealEstatePlatform.Domain.Entities;

namespace RealEstatePlatform.API.Tests.RepositoryMocks
{
    public static class ListingTypeRepository
    {
        internal readonly static List<ListingType> ListingTypes =
        [
            ListingType.Create("For Sale").Value,
            ListingType.Create("For Rent").Value
        ];

        public static IListingTypeRepository GetListingTypeRepository()
        {
            var mockListingTypeRepository = NSubstitute.Substitute.For<IListingTypeRepository>();
            mockListingTypeRepository.GetAllAsync().Returns(Result<IReadOnlyList<ListingType>>.Success(ListingTypes));
            mockListingTypeRepository.FindByIdAsync(ListingTypes[0].ListingTypeId).Returns(Result<ListingType>.Success(ListingTypes[0]));
            mockListingTypeRepository.FindByIdAsync(ListingTypes[1].ListingTypeId).Returns(Result<ListingType>.Success(ListingTypes[1]));
            mockListingTypeRepository.FindByIdAsync(Arg.Is<Guid>(id => id != ListingTypes[0].ListingTypeId && id != ListingTypes[1].ListingTypeId)).Returns(Result<ListingType>.Failure("ListingType not found."));
            return mockListingTypeRepository;
        }
    }
}
