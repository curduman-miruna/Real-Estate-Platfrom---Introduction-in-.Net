using MediatR;
using RealEstatePlatform.Application.Persistence;

namespace RealEstatePlatform.Application.Features.PropertyTypes.Queries.GetById
{
    public class GetByIdPropertyTypeQueryHandler : IRequestHandler<GetByIdPropertyTypeQuery, PropertyTypeDto>
    {
        private readonly IPropertyTypeRepository repository;
        public GetByIdPropertyTypeQueryHandler(IPropertyTypeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<PropertyTypeDto> Handle(GetByIdPropertyTypeQuery request, CancellationToken cancellationToken)
        {
            var propertyType = await repository.FindByIdAsync(request.Id);
            if (propertyType.IsSuccess)
            {
                return new PropertyTypeDto
                {
                    PropertyTypeId = propertyType.Value.PropertyTypeId,
                    PropertyTypeName = propertyType.Value.PropertyTypeName
                };
            }
            return new PropertyTypeDto();
        }
    }
}
