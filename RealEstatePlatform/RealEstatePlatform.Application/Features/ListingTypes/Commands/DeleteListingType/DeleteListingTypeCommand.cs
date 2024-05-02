using MediatR;

namespace RealEstatePlatform.Application.Features.ListingTypes.Commands.DeleteListingType
{
    public class DeleteListingTypeCommand : IRequest<DeleteListingTypeCommandResponse>
    {
        public Guid ListingTypeId { get; set; }
    }
}
