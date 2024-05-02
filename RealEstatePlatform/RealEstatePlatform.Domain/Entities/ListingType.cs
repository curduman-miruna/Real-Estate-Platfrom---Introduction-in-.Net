using RealEstatePlatform.Domain.Common;

namespace RealEstatePlatform.Domain.Entities
{
    public class ListingType : AuditableEntity
    {
        public ListingType(string listingTypeName)
        {
            ListingTypeId = Guid.NewGuid();
            ListingTypeName = listingTypeName;
        }
        public Guid ListingTypeId { get; private set; }
        public string ListingTypeName { get; private set; } = null!;

        public static Result<ListingType> Create(string listingTypeName)
        {
            if (string.IsNullOrWhiteSpace(listingTypeName))
                return Result<ListingType>.Failure("Listing type name cannot be empty");
            return Result<ListingType>.Success(new ListingType(listingTypeName));
        }

        public void Update(string listingTypeName)
        {
            ListingTypeName = listingTypeName;
        }
    }
}
