using MediatR;
using RealEstatePlatform.Application.Persistence;

namespace RealEstatePlatform.Application.Features.Properties.Queries.GetAll
{
    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, GetAllPropertiesResponse>
    {
        private readonly IPropertyRepository repository;

        public GetAllPropertiesQueryHandler(IPropertyRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllPropertiesResponse> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            GetAllPropertiesResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Properties = result.Value.Select(p => new PropertyDto
                {
                    PropertyId = p.PropertyId,
                    PropertyName = p.PropertyName,
                    Description = p.Description,
                    Location = p.Location,
                    Price = p.Price,
                    Area = p.Area,
                    PropertyTypeId = p.PropertyTypeId,
                    ListingTypeId = p.ListingTypeId,
                    Bedrooms = p.Bedrooms,
                    Bathrooms = p.Bathrooms,
                    Username = p.Username
                   
                }).ToList();
            }
            return response;
        }

    }
}
