using MediatR;

namespace RealEstatePlatform.Application.Features.Messages.Queries.GetById
{
    public record GetByIdMessageQuery(Guid Id) : IRequest<MessageDto> 
    {
    }
}
