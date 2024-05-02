using MediatR;
using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Entities;

namespace RealEstatePlatform.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, CreatePropertyCommandResponse>
    {
        private readonly IPropertyRepository repository;

        public CreatePropertyCommandHandler(IPropertyRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreatePropertyCommandResponse> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreatePropertyCommandValidator(repository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validationResult.IsValid)
            {
                return new CreatePropertyCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var property = Property.Create(request.PropertyName, request.Description, request.Location, request.Price, request.Area, request.Bedrooms, request.Bathrooms, request.Username);
            if (property.IsSuccess)
            {
                property.Value.AttachPropertryType(request.PropertyTypeId);
                property.Value.AttachListingType(request.ListingTypeId);

                await repository.AddAsync(property.Value);

                return new CreatePropertyCommandResponse
                {
                    Success = true,
                    Property = new CreatePropertyDto
                    {
                        PropertyId = property.Value.PropertyId,
                        PropertyName = property.Value.PropertyName,
                        Description = property.Value.Description,
                        Location = property.Value.Location,
                        Price = property.Value.Price,
                        PropertyTypeId = property.Value.PropertyTypeId,
                        ListingTypeId = property.Value.ListingTypeId,
                        Area = property.Value.Area,
                        Bedrooms = property.Value.Bedrooms,
                        Bathrooms = property.Value.Bathrooms,
                        Username = property.Value.Username

                    }
                };
            }
            return new CreatePropertyCommandResponse
            {
                Success = false,
                ValidationsErrors = new List<string> { property.Error }
            };
        }
    }
}
