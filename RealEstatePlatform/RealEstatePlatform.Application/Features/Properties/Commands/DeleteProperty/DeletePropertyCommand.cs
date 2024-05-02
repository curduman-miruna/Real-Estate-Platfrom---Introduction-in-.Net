using MediatR;

namespace RealEstatePlatform.Application.Features.Properties.Commands.DeleteProperty
{
    public class DeletePropertyCommand : IRequest<DeletePropertyCommandResponse>
    {
        public Guid PropertyId { get; set; }
    }
}
