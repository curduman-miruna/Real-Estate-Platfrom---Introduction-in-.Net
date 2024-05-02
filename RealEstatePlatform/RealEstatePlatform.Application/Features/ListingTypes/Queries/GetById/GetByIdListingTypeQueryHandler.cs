using RealEstatePlatform.Application.Persistence;
using MediatR;

namespace RealEstatePlatform.Application.Features.ListingTypes.Queries.GetById
{
    public class GetByIdListingTypeHandler : IRequestHandler<GetByIdListingTypeQuery, ListingTypeDto>
    {
        private readonly IListingTypeRepository repository;
      
        public GetByIdListingTypeHandler(IListingTypeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<ListingTypeDto> Handle(GetByIdListingTypeQuery request, CancellationToken cancellationToken)
        {
            var listingType = await repository.FindByIdAsync(request.Id);
            if (listingType.IsSuccess)
            {
                return new ListingTypeDto
                {
                    ListingTypeId = listingType.Value.ListingTypeId,
                    ListingTypeName = listingType.Value.ListingTypeName
                };
            }
            return new ListingTypeDto();
        }
    }
}