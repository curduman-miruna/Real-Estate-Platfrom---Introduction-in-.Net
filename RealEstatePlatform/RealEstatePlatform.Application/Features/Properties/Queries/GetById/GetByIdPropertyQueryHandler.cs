using MediatR;
using RealEstatePlatform.Application.Persistence;

namespace RealEstatePlatform.Application.Features.Properties.Queries.GetById
{
    public class GetByIdPropertyQueryHandler : IRequestHandler<GetByIdPropertyQuery, PropertyDto>
    {
        private readonly IPropertyRepository repository;
        public GetByIdPropertyQueryHandler(IPropertyRepository repository)
        {
            this.repository = repository;
        }
        public async Task<PropertyDto> Handle(GetByIdPropertyQuery request, CancellationToken cancellationToken)
        {
            var property = await repository.FindByIdAsync(request.Id);
            if (property.IsSuccess)
            {
                return new PropertyDto
                {
                   PropertyId = property.Value.PropertyId,
                   PropertyName = property.Value.PropertyName,
                   Description = property.Value.Description,
                   Price = property.Value.Price,
                   Bathrooms = property.Value.Bathrooms,
                   Bedrooms = property.Value.Bedrooms,
                   Area = property.Value.Area,
                   Username = property.Value.Username,
                   Location = property.Value.Location,
                   PropertyTypeId = property.Value.PropertyTypeId,
                   ListingTypeId = property.Value.ListingTypeId,
                   
                   
                };
            }
            return new PropertyDto();
        }
    }
}
