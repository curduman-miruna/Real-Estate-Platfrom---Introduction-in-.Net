using RealEstatePlatform.Application.Persistence;
using MediatR;
using RealEstatePlatform.Domain.Entities;

namespace RealEstatePlatform.Application.Features.ListingTypes.Queries.GetAll
{
    public class GetAllListingTypesQueryHandler : IRequestHandler<GetAllListingTypesQuery, GetAllListingTypesResponse>
    {
        private readonly IListingTypeRepository repository;

        public GetAllListingTypesQueryHandler(IListingTypeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllListingTypesResponse> Handle(GetAllListingTypesQuery request, CancellationToken cancellationToken)
        {
            GetAllListingTypesResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.ListingTypes = result.Value.Select(c => new ListingTypeDto
                {
                    ListingTypeId = c.ListingTypeId,
                    ListingTypeName = c.ListingTypeName
                }).ToList();
            }
            return response;
        }
    }
}
