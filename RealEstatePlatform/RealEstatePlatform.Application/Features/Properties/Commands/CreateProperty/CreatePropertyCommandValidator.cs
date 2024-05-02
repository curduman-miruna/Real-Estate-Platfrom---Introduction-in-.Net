using FluentValidation;
using RealEstatePlatform.Application.Persistence;

namespace RealEstatePlatform.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        private IPropertyRepository repository;

        public CreatePropertyCommandValidator(IPropertyRepository repository)
        {
            RuleFor(p => p.PropertyName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.PropertyTypeId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ListingTypeId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p=>p.Price)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than 0.")
                .NotNull();

            RuleFor(p => p.Area)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
                .NotNull();

            RuleFor(p => p.Bathrooms)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than 0.")
                .NotNull();

            RuleFor(p => p.Bedrooms)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be greater than 0.")
                .NotNull();


            RuleFor(p => p)
                .Must(PropertyNameAndLocationUnique).WithMessage("There is an property with the exact name and location, they must be unique.")
                .Must(PropertyTypeExists).WithMessage("Property type must exist.")
                .Must(ListingTypeExists).WithMessage("Listing type must exist.")
                .Must(UserExists).WithMessage("User must exist.");
            this.repository = repository;
        }

        private bool UserExists(CreatePropertyCommand command)
        {
            return true;
            //return repository.DoesUserExists(command.UserId).Result;
        }

        private bool ListingTypeExists(CreatePropertyCommand command)
        {
            return repository.DoesListingTypeExists(command.ListingTypeId).Result;
        }

        private bool PropertyTypeExists(CreatePropertyCommand command)
        {
            return repository.DoesPropertyTypeExists(command.PropertyTypeId).Result;
        }

        private bool PropertyNameAndLocationUnique(CreatePropertyCommand command)
        {
            return !repository.IsPropertyNameAndLocationUnique(command.PropertyName, command.Location).Result;
        }
    }
}
