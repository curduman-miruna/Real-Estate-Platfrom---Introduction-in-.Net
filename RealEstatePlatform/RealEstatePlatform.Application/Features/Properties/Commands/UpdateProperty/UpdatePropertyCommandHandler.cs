using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Entities;
using MediatR;
using RealEstatePlatform.Domain.Common;
using System.Diagnostics;

namespace RealEstatePlatform.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, UpdatePropertyCommandResponse>
    {
        private readonly IPropertyRepository repository;

        public UpdatePropertyCommandHandler(IPropertyRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdatePropertyCommandResponse> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {

            var property = await repository.FindByIdAsync(request.PropertyId);
            if(!property.IsSuccess)
            {
                return new UpdatePropertyCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { property.Error }
                };
            }
            
            request.PropertyName ??= property.Value.PropertyName;
            request.Description ??= property.Value.Description;
            if (request.Price <= 0)
                request.Price = property.Value.Price;
            if (request.PropertyTypeId == default)
               request.PropertyTypeId = property.Value.PropertyTypeId;
            if (request.ListingTypeId == default)
               request.ListingTypeId = property.Value.ListingTypeId;
            if (request.Area == default || request.Area <=0)
               request.Area = property.Value.Area;
            if (request.Bedrooms == default || request.Bedrooms<0)
               request.Bedrooms = property.Value.Bedrooms;
            if (request.Bathrooms == default || request.Bathrooms<0)
                request.Bathrooms = property.Value.Bathrooms;
            

            var validator = new UpdatePropertyCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
            {
                return new UpdatePropertyCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            property.Value.Update(request.PropertyName, request.Description, request.ListingTypeId, request.PropertyTypeId, request.Location,  request.Price, request.Area, request.Bedrooms, request.Bathrooms);
            var result = await repository.UpdateAsync(property.Value);

            return new UpdatePropertyCommandResponse
            {
                Success = true,
                Property = new UpdatePropertyDto
                {
                    PropertyId = property.Value.PropertyId,
                    PropertyName = property.Value.PropertyName,
                    Description = property.Value.Description,
                    Location = property.Value.Location,
                    Price = property.Value.Price,
                    Area = property.Value.Area,
                    ListingTypeId = property.Value.ListingTypeId,
                    PropertyTypeId = property.Value.PropertyTypeId,
                    Bedrooms = property.Value.Bedrooms,
                    Bathrooms = property.Value.Bathrooms,
                    Username = property.Value.Username
                }
            };
           
        }
        
    }
}
