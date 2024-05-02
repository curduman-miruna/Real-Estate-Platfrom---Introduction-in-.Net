using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Entities;
using MediatR;

namespace RealEstatePlatform.Application.Features.ListingTypes.Commands.CreateListingType
{
    public class CreateListingTypeCommandHandler : IRequestHandler<CreateListingTypeCommand, CreateListingTypeCommandResponse>
    {
        private readonly IListingTypeRepository repository;

        public CreateListingTypeCommandHandler(IListingTypeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateListingTypeCommandResponse> Handle(CreateListingTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateListingTypeCommandValidator(repository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateListingTypeCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var listingType = ListingType.Create(request.ListingTypeName);
            if (!listingType.IsSuccess)
            {
                return new CreateListingTypeCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { listingType.Error }
                };
            }

            await repository.AddAsync(listingType.Value);

            return new CreateListingTypeCommandResponse
            {
                Success = true,
                ListingType = new CreateListingTypeDto
                {
                    ListingTypeId = listingType.Value.ListingTypeId,
                    ListingTypeName = listingType.Value.ListingTypeName
               
                }
            };
        }
    }
}
