using RealEstatePlatform.Domain.Common;

namespace RealEstatePlatform.Domain.Entities
{
    public class Property : AuditableEntity
    {
        public Property(string propertyName, string description, string location, int price, decimal area, int bedrooms, int bathrooms, string username)
        {
            PropertyId = Guid.NewGuid();
            PropertyName = propertyName;
            Description = description;
            Location = location;
            Price = price;
            Area = area;
            Bedrooms = bedrooms;
            Bathrooms = bathrooms;
            Username = username;
        }

        public Guid PropertyId { get; private set; }
        public string PropertyName { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public string Location { get; private set; } = null!;
        public int Price { get; private set; }
        public Guid PropertyTypeId { get; private set; }
        public Guid ListingTypeId { get; private set; }
        public decimal Area { get; private set; }
        public int Bedrooms { get; private set; }
        public int Bathrooms { get; private set; }
        public string Username { get; private set; }

        public void AttachPropertryType(Guid propertyTypeId)
        {
            if (propertyTypeId != Guid.Empty)
            {
                PropertyTypeId = propertyTypeId;
            }
        }

        public void AttachListingType(Guid listingTypeId)
        {
            if (listingTypeId != Guid.Empty)
            {
                ListingTypeId = listingTypeId;
            }
        }

        public static Result<Property> Create(string propertyName, string description, string location, int price, decimal area, int bedrooms, int bathrooms, string username)
        {
            if(string.IsNullOrWhiteSpace(propertyName))
                return Result<Property>.Failure("Property name cannot be empty");
            if(string.IsNullOrWhiteSpace(description))
                return Result<Property>.Failure("Description cannot be empty");
            if(string.IsNullOrWhiteSpace(location))
                return Result<Property>.Failure("Location cannot be empty");
            if(price <= 0)
                return Result<Property>.Failure("Price cannot be less than or equal to zero");
            if(area <= 0)
                return Result<Property>.Failure("Area cannot be less than or equal to zero");
            if(bedrooms <= 0)
                return Result<Property>.Failure("Bedrooms cannot be less than or equal to zero");
            if(bathrooms <= 0)
                return Result<Property>.Failure("Bathrooms cannot be less than or equal to zero");
            if(string.IsNullOrWhiteSpace(username))
                return Result<Property>.Failure("User id cannot be empty");
            return Result<Property>.Success(new Property(propertyName, description, location, price, area, bedrooms, bathrooms, username));
        }

        public void Update(string propertyName, string description, Guid listingTypeId, Guid propertyTypeId, string location, int price, decimal area, int bedrooms, int bathrooms)
        {
            PropertyName = propertyName;
            Description = description;
            Location = location;
            Price = price;
            Area = area;
            Bedrooms = bedrooms;
            Bathrooms = bathrooms;
            ListingTypeId = listingTypeId;
            PropertyTypeId = propertyTypeId;
        }
    }
}
