using RealEstatePlatform.Application.Responses;


namespace RealEstatePlatform.Application.Features.ListingTypes.Commands.UpdateListingType
{
    public class UpdateListingTypeCommandResponse : BaseResponse
    {
        public UpdateListingTypeCommandResponse() : base()
        {
        }

        public UpdateListingTypeDto ListingType { get; set; }
    }
}