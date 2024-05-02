using RealEstatePlatform.Application.Persistence;
using MediatR;

namespace RealEstatePlatform.Application.Features.Images.Queries.GetAll
{
    public class GetAllImagesQueryHandler : IRequestHandler<GetAllImagesQuery, GetAllImagesResponse>
    {
        private readonly IImageRepository repository;

        public GetAllImagesQueryHandler(IImageRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllImagesResponse> Handle(GetAllImagesQuery request, CancellationToken cancellationToken)
        {
            GetAllImagesResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Images = result.Value.Select(c => new ImageDto
                {
                    ImageId = c.ImageId,
                    ImageData = c.ImageData,
                    PropertyId = c.PropertyId
                }).ToList();
            }
            return response;
        }
    }
}
