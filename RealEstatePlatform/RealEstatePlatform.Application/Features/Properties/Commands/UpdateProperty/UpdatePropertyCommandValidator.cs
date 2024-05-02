using FluentValidation;

namespace RealEstatePlatform.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommandValidator : AbstractValidator<UpdatePropertyCommand>
    {
        public UpdatePropertyCommandValidator()
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

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.")
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

        }
    }
}
