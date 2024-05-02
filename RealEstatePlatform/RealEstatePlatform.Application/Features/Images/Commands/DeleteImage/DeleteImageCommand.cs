using MediatR;

namespace RealEstatePlatform.Application.Features.Images.Commands.DeleteImage
{
    public class DeleteImageCommand : IRequest<DeleteImageCommandResponse>
    {
        public Guid PropertyId { get; set; }
    }
}
