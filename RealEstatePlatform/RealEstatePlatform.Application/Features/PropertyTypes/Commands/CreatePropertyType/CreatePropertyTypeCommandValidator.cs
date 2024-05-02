using FluentValidation;
using RealEstatePlatform.Application.Persistence;

namespace RealEstatePlatform.Application.Features.PropertyTypes.Commands.CreatePropertyType
{
    public class CreatePropertyTypeCommandValidator : AbstractValidator<CreatePropertyTypeCommand>
    {
        private IPropertyTypeRepository repository;

        public CreatePropertyTypeCommandValidator(IPropertyTypeRepository repository)
        {
            RuleFor(p => p.PropertyTypeName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
            RuleFor(p=>p).Must(PrpertyTypeNameUnique).WithMessage("Property type name must be unique.");
            this.repository = repository;
        }

        private bool PrpertyTypeNameUnique(CreatePropertyTypeCommand command)
        {
            
            return !repository.IsPropertyTypeNameUnique(command.PropertyTypeName).Result;
        }
    }
}
