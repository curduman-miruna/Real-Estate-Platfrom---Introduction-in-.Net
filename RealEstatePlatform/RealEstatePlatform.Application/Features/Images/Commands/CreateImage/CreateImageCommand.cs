using MediatR;

namespace RealEstatePlatform.Application.Features.Images.Commands.CreateImage
{
    public class CreateImageCommand : IRequest<CreateImageCommandResponse>
    {
        public string ImageData { get; set; } = default!;
        public Guid PropertyId { get; set; }
    }
}
