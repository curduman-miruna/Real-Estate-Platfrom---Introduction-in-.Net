using RealEstatePlatform.Application.Persistence;
using MediatR;

namespace RealEstatePlatform.Application.Features.Images.Queries.GetById
{
    public class GetByIdImageHandler : IRequestHandler<GetByIdImageQuery, ImageDto>
    {
        private readonly IImageRepository repository;

        public GetByIdImageHandler(IImageRepository repository)
        {
            this.repository = repository;
        }
        public async Task<ImageDto> Handle(GetByIdImageQuery request, CancellationToken cancellationToken)
        {
            var Image = await repository.FindByIdAsync(request.Id);
            if (Image.IsSuccess)
            {
                return new ImageDto
                {
                    ImageId = Image.Value.ImageId,
                    ImageData = Image.Value.ImageData,
                    PropertyId = Image.Value.PropertyId
                };
            }
            return new ImageDto();
        }
    }
}