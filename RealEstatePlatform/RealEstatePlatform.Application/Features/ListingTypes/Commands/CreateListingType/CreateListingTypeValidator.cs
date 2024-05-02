using FluentValidation;
using RealEstatePlatform.Application.Persistence;

namespace RealEstatePlatform.Application.Features.ListingTypes.Commands.CreateListingType
{
    public class CreateListingTypeCommandValidator : AbstractValidator<CreateListingTypeCommand>
    {
        private IListingTypeRepository repository;

        public CreateListingTypeCommandValidator(IListingTypeRepository repository)
        {
            RuleFor(expression: x => x.ListingTypeName)
                .NotEmpty().WithMessage("Listing type name is required.")
                .NotNull()
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
           
            RuleFor (expression: x => x).Must(ListingTypeNameUnique).WithMessage("Listing type name must be unique.");
            this.repository = repository;
        }

        private bool ListingTypeNameUnique(CreateListingTypeCommand command)
        {
            return !repository.IsListingTypeNameUnique(command.ListingTypeName).Result;
        }
    }
}
