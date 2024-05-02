using MediatR;
using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Entities;

namespace RealEstatePlatform.Application.Features.PropertyTypes.Commands.CreatePropertyType
{
    public class CreatePropertyTypeCommandHandler : IRequestHandler<CreatePropertyTypeCommand, CreatePropertyTypeCommandResponse>
    {
        private readonly IPropertyTypeRepository repository;

        public CreatePropertyTypeCommandHandler(IPropertyTypeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreatePropertyTypeCommandResponse> Handle(CreatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePropertyTypeCommandValidator(repository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreatePropertyTypeCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var propertyType = PropertyType.Create(request.PropertyTypeName);
            
            if(!propertyType.IsSuccess)
            {
                return new CreatePropertyTypeCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { propertyType.Error }
                };
            }

            await repository.AddAsync(propertyType.Value);

            return new CreatePropertyTypeCommandResponse
            {
                Success = true,
                PropertyType = new CreatePropertyTypeDto
                {
                    PropertyTypeId = propertyType.Value.PropertyTypeId,
                    PropertyTypeName = propertyType.Value.PropertyTypeName
                }
               
            };
        }
    }
}
