using MediatR;
using RealEstatePlatform.Application.Persistence;

namespace RealEstatePlatform.Application.Features.PropertyTypes.Queries.GetAll
{
    public class GetAllPropertyTypesQueryHandler : IRequestHandler<GetAllPropertyTypesQuery, GetAllPropertyTypesResponse>
    {
        private readonly IPropertyTypeRepository repository;
        public GetAllPropertyTypesQueryHandler(IPropertyTypeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<GetAllPropertyTypesResponse> Handle(GetAllPropertyTypesQuery request, CancellationToken cancellationToken)
        {
            GetAllPropertyTypesResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.PropertyTypes = result.Value.Select(pt => new PropertyTypeDto
                {
                    PropertyTypeId = pt.PropertyTypeId,
                    PropertyTypeName = pt.PropertyTypeName
                }).ToList();
            }
            return response;
        }
    }
}
