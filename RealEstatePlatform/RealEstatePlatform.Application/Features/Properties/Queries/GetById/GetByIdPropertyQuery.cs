using MediatR;

namespace RealEstatePlatform.Application.Features.Properties.Queries.GetById
{
    public record GetByIdPropertyQuery(Guid Id): IRequest<PropertyDto>
    {
    }
}
