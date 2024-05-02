using MediatR;
namespace RealEstatePlatform.Application.Features.Images.Queries
{
    public record GetByIdImageQuery(Guid Id) : IRequest<ImageDto>;
}
