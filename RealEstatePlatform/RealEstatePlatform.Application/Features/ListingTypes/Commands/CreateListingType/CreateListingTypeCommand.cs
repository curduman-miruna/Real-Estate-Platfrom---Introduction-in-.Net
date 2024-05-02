using MediatR;

namespace RealEstatePlatform.Application.Features.ListingTypes.Commands.CreateListingType
{
    public class CreateListingTypeCommand : IRequest<CreateListingTypeCommandResponse>
    {
        public string ListingTypeName { get; set; } = default!;

    }
}
