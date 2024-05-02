namespace RealEstatePlatform.Application.Features.Images.Commands.UpdateImage
{
    public class UpdateImageDto
    {
        public Guid ImageId { get;  set; }
        public string ImageData { get; set; } = null!;
        public Guid PropertyId { get; set; }
    }
}
