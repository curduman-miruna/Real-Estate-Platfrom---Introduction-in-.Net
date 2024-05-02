namespace RealEstatePlatform.Application.Features.PropertyTypes.Commands.CreatePropertyType
{
    public class CreatePropertyTypeDto
    {
        public Guid PropertyTypeId { get; set; }
        public string PropertyTypeName { get; set; } = default!;
    }
}
