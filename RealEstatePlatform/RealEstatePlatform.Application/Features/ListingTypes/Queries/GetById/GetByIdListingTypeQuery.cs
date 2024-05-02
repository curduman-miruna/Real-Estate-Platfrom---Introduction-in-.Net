using MediatR;

namespace RealEstatePlatform.Application.Features.ListingTypes.Queries.GetById
{
    public record GetByIdListingTypeQuery(Guid Id) : IRequest<ListingTypeDto>;
}
