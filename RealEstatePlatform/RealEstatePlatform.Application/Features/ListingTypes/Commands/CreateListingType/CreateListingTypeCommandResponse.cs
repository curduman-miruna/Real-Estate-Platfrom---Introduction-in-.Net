using RealEstatePlatform.Application.Responses;

namespace RealEstatePlatform.Application.Features.ListingTypes.Commands.CreateListingType
{
    public class CreateListingTypeCommandResponse : BaseResponse
    {
        public CreateListingTypeCommandResponse() : base()
        {
        }

        public CreateListingTypeDto ListingType { get; set; }
    }
}
