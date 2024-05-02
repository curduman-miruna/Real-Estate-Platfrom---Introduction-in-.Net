using FluentValidation;

namespace RealEstatePlatform.Application.Features.Images.Commands.UpdateImage
{
    public class UpdateImageCommandValidator : AbstractValidator<UpdateImageCommand>
    {
        public UpdateImageCommandValidator()
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
