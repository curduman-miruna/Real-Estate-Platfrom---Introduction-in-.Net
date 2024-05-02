using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Application.Features.Images.Queries.GetAll;

namespace RealEstatePlatform.Application.Features.Images.Queries.GetImagesByPropertyId
{
    public class GetImagesByPropertyIdQueryHandler : IRequestHandler<GetImagesByPropertyIdQuery, GetImagesByPropertyIdResponse>
    {
        private readonly IImageRepository repository;
        public GetImagesByPropertyIdQueryHandler(IImageRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetImagesByPropertyIdResponse> Handle(GetImagesByPropertyIdQuery request, CancellationToken cancellationToken)
        {
            GetImagesByPropertyIdResponse getImagesByPropertyIdResponse = new();
            var result = await repository.GetAllAsync();
            var filteredList = result.Value.Where(i => i.PropertyId == request.PropertyId).ToList();
            if (result.IsSuccess)
            {
                getImagesByPropertyIdResponse.Images = filteredList.Select(i => new ImageDto
                {
                    ImageId = i.ImageId,
                    PropertyId = i.PropertyId,
                    ImageData = i.ImageData
                }).ToList();
            }
            return getImagesByPropertyIdResponse;
        }

      
    }
}
