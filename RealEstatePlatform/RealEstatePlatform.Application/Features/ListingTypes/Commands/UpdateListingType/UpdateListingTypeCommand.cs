using MediatR;

namespace RealEstatePlatform.Application.Features.ListingTypes.Commands.UpdateListingType
{
    public class UpdateListingTypeCommand : IRequest<UpdateListingTypeCommandResponse>
    {
        public Guid ListingTypeId { get; set; }
        public string ListingTypeName { get; set; } = default!;
    }
}
