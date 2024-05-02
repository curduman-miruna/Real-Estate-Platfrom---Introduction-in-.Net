using RealEstatePlatform.Domain.Common;

namespace RealEstatePlatform.Domain.Entities
{
    public class PropertyType : AuditableEntity
    {
        public PropertyType()
        {
        }

        public PropertyType(string propertyTypeName)
        {
            PropertyTypeId = Guid.NewGuid();
            PropertyTypeName = propertyTypeName;
        }
        public Guid PropertyTypeId { get; private set; }
        public string PropertyTypeName { get; private set; } = null!;

        public static Result<PropertyType> Create(string propertyTypeName)
        {
            if (string.IsNullOrWhiteSpace(propertyTypeName))
                return Result<PropertyType>.Failure("Property type name cannot be empty");
            return Result<PropertyType>.Success(new PropertyType(propertyTypeName));
        }

        public void Update(string propertyTypeName)
        {
            PropertyTypeName = propertyTypeName;
        }
    }
}
