using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Entities;
using MediatR;

namespace RealEstatePlatform.Application.Features.PropertyTypes.Commands.UpdatePropertyType
{
    public class UpdatePropertyTypeCommandHandler : IRequestHandler<UpdatePropertyTypeCommand, UpdatePropertyTypeCommandResponse>
    {
        private readonly IPropertyTypeRepository repository;

        public UpdatePropertyTypeCommandHandler(IPropertyTypeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdatePropertyTypeCommandResponse> Handle(UpdatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            var propertyType = await repository.FindByIdAsync(request.PropertyTypeId);
            if(!propertyType.IsSuccess)
            {
               return new UpdatePropertyTypeCommandResponse
               {
                   Success = false,
                   ValidationsErrors = new List<string> { propertyType.Error }
               };
            }
            request.PropertyTypeName ??= propertyType.Value.PropertyTypeName;
            var validator = new UpdatePropertyTypeCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
            {
                return new UpdatePropertyTypeCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            propertyType.Value.Update(request.PropertyTypeName);

            var result = await repository.UpdateAsync(propertyType.Value);

            return new UpdatePropertyTypeCommandResponse
            {
                Success = result.IsSuccess,
                PropertyType = new UpdatePropertyTypeDto
                {
                    PropertyTypeId = propertyType.Value.PropertyTypeId,
                    PropertyTypeName = propertyType.Value.PropertyTypeName
                }
            };

        }
    }
}
