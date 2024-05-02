namespace RealEstatePlatform.Application.Features.ListingTypes.Commands.UpdateListingType
{
    public class UpdateListingTypeDto
    {
        public Guid ListingTypeId { get; set; }
        public string ListingTypeName { get; set; } = default!;
    }
}
