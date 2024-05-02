namespace RealEstatePlatform.Application.Features.ListingTypes.Commands.CreateListingType
{
    public class CreateListingTypeDto
    {
        public string ListingTypeName { get; set; } = default!;
        public Guid ListingTypeId { get; set; }
    }
}