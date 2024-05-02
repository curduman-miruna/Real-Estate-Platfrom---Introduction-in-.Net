

namespace RealEstatePlatform.Application.Features.Images.Commands.CreateImage
{
    public class CreateImageDto
    {
        public string ImageData { get; set; } = default!;
        public Guid PropertyId { get; set; }
        public Guid ImageId { get; set; }
    }
}
