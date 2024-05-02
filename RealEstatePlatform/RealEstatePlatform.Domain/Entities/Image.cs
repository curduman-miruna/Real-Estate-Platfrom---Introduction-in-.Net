using RealEstatePlatform.Domain.Common;

namespace RealEstatePlatform.Domain.Entities
{
    public class Image : AuditableEntity
    {
        public Image(string imageData, Guid propertyId)
        {
            ImageId = Guid.NewGuid();
            ImageData = imageData;
            PropertyId = propertyId;
        }
        public Guid ImageId { get; private set; }
        public string ImageData { get; private set; } = null!;
        public Guid PropertyId { get; private set; }

        public static Result<Image> Create(string imageData, Guid propertyId)
        {
            if (string.IsNullOrWhiteSpace(imageData))
                return Result<Image>.Failure("Image data cannot be empty");
            if (propertyId == default)
                return Result<Image>.Failure("Property id cannot be empty");
            return Result<Image>.Success(new Image(imageData, propertyId));
        }

        public void Update(string imageData, Guid propertyId)
        {
            ImageData = imageData;
            PropertyId = propertyId;
        }
    }
}
