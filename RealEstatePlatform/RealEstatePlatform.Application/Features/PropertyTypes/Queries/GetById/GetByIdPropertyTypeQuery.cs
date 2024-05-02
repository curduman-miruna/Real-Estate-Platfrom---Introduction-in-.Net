using MediatR;

namespace RealEstatePlatform.Application.Features.PropertyTypes.Queries.GetById
{
    public record GetByIdPropertyTypeQuery(Guid Id): IRequest<PropertyTypeDto>
    {
    }
}
