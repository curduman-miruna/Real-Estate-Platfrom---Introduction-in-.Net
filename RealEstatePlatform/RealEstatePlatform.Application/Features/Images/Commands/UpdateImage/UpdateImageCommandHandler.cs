using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Entities;
using MediatR;
using RealEstatePlatform.Domain.Common;
using System.Diagnostics;

namespace RealEstatePlatform.Application.Features.Images.Commands.UpdateImage
{
    public class UpdateImageCommandHandler : IRequestHandler<UpdateImageCommand, UpdateImageCommandResponse>
    {
        private readonly IImageRepository repository;

        public UpdateImageCommandHandler(IImageRepository repository)
        {
            this.repository = repository;
        }

        public async Task<UpdateImageCommandResponse> Handle(UpdateImageCommand request, CancellationToken cancellationToken)
        {

            var image = await repository.FindByIdAsync(request.ImageId);
            if (!image.IsSuccess)
            {
                return new UpdateImageCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { image.Error }
                };
            }

            request.ImageData ??= image.Value.ImageData;
            if (request.PropertyId == default)
                request.PropertyId = image.Value.PropertyId;

            var validator = new UpdateImageCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return new UpdateImageCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            image.Value.Update(request.ImageData, request.PropertyId);
            var result = await repository.UpdateAsync(image.Value);

            return new UpdateImageCommandResponse
            {
                Success = true,
                Image= new UpdateImageDto
                {
                   ImageId= image.Value.ImageId,
                   ImageData = image.Value.ImageData,
                   PropertyId = image.Value.PropertyId,
                }
            };

        }

    }
}
