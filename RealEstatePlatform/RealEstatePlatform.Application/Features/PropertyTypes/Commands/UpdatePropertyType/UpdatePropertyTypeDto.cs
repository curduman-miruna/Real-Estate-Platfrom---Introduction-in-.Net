namespace RealEstatePlatform.Application.Features.PropertyTypes.Commands.UpdatePropertyType
{
    public class UpdatePropertyTypeDto
    {
        public Guid PropertyTypeId { get; set; }
        public string PropertyTypeName { get; set; } = default!;
    }
}
