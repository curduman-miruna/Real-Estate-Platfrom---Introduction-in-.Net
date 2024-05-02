using MediatR;
using System;

namespace RealEstatePlatform.Application.Features.Images.Commands.UpdateImage
{
    public class UpdateImageCommand : IRequest<UpdateImageCommandResponse>
    {
        public Guid ImageId { get; set; }
        public string ImageData { get; set; } = null!; 
        public Guid PropertyId { get; set; }
    }
}
