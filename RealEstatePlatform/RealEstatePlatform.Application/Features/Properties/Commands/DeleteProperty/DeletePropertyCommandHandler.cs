using MediatR;
using RealEstatePlatform.Application.Persistence;

namespace RealEstatePlatform.Application.Features.Properties.Commands.DeleteProperty
{
    public class DeletePropertyCommandHandler : IRequestHandler<DeletePropertyCommand, DeletePropertyCommandResponse>
    {
        public readonly IPropertyRepository repository;
        public readonly IImageRepository imageRepository;

        public DeletePropertyCommandHandler(IPropertyRepository repository, IImageRepository imageRepository)
        {
            this.repository = repository;
            this.imageRepository = imageRepository;
        }

        public async Task<DeletePropertyCommandResponse> Handle(DeletePropertyCommand request, CancellationToken cancellationToken)
        {
            // Get the list of image IDs associated with the property
            var imageIdsResult = await imageRepository.GetAllAsync();
            imageIdsResult.Value.Where(i => i.PropertyId == request.PropertyId).ToList();

            // Delete the property
            var propertyDeleteResult = await repository.DeleteAsync(request.PropertyId);

            if (!propertyDeleteResult.IsSuccess)
            {
                // Handle error while deleting property
                return new DeletePropertyCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { propertyDeleteResult.Error }
                };
            }

            // Delete each image associated with the property
            foreach (var imageId in imageIdsResult.Value)
            {
                var imageDeleteResult = await imageRepository.DeleteAsync(imageId.ImageId);

                if (!imageDeleteResult.IsSuccess)
                {
                    // Handle error while deleting an image
                    return new DeletePropertyCommandResponse
                    {
                        Success = false,
                        ValidationsErrors = new List<string> { imageDeleteResult.Error }
                    };
                }
            }

            return new DeletePropertyCommandResponse
            {
                Success = true
            };
        }


    }
}
