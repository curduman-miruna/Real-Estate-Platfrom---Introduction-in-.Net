using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Entities;
using MediatR;

namespace RealEstatePlatform.Application.Features.ListingTypes.Commands.UpdateListingType
{
    public class UpdateListingTypeCommandHandler : IRequestHandler<UpdateListingTypeCommand, UpdateListingTypeCommandResponse>
    {
        private readonly IListingTypeRepository repository;

        public UpdateListingTypeCommandHandler(IListingTypeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdateListingTypeCommandResponse> Handle(UpdateListingTypeCommand request, CancellationToken cancellationToken)
        {
            var role = await repository.FindByIdAsync(request.ListingTypeId);
            if(!role.IsSuccess)
            {
               return new UpdateListingTypeCommandResponse
               {
                   Success = false,
                   ValidationsErrors = new List<string> { role.Error }
               };
            }
            request.ListingTypeName ??= role.Value.ListingTypeName;
            var validator = new UpdateListingTypeCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
            {
                return new UpdateListingTypeCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            role.Value.Update(request.ListingTypeName);

            var result = await repository.UpdateAsync(role.Value);

            return new UpdateListingTypeCommandResponse
            {
                Success = result.IsSuccess,
                ListingType = new UpdateListingTypeDto
                {
                    ListingTypeId = role.Value.ListingTypeId,
                    ListingTypeName = role.Value.ListingTypeName
                }
            };

        }
    }
}
