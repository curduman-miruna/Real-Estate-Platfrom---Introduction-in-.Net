using FluentValidation;

namespace RealEstatePlatform.Application.Features.Images.Commands.CreateImage
{
    public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
    {
        public CreateImageCommandValidator()
        {
            RuleFor(expression: x => x.ImageData)
                .NotEmpty().WithMessage("Image data is required.")
                .NotNull()
                .MaximumLength(1000000).WithMessage("{PropertyName} must not exceed 100000 characters.");
            RuleFor(expression: x => x.PropertyId)
                .NotEmpty().WithMessage("Property id is required.")
                .NotNull();
        }
    }
}
