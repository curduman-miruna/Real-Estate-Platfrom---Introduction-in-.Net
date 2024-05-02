using FluentValidation;

namespace RealEstatePlatform.Application.Features.ListingTypes.Commands.UpdateListingType
{
    public class UpdateListingTypeCommandValidator : AbstractValidator<UpdateListingTypeCommand>
    {
        public UpdateListingTypeCommandValidator()
        {
            RuleFor(expression: x => x.ListingTypeName)
                .NotEmpty().WithMessage("Listing type name is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(expression: x => x.ListingTypeId)
                .NotEmpty().WithMessage("Listing type id is required.")
                .NotNull();
        }
    }
}
