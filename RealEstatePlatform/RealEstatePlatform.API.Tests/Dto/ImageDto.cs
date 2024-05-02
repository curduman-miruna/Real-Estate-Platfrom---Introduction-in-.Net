

namespace RealEstatePlatform.API.Tests.Dto
{
    public class ImageDto
    {
        public string ImageData { get; set; } = default!;
        public Guid PropertyId { get; set; }
        public Guid ImageId { get; set; }
    }
}
