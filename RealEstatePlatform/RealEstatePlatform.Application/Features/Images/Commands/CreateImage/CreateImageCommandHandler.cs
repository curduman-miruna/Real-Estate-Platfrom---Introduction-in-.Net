using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Entities;
using MediatR;

namespace RealEstatePlatform.Application.Features.Images.Commands.CreateImage
{
    public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, CreateImageCommandResponse>
    {
        private readonly IImageRepository repository;

        public CreateImageCommandHandler(IImageRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateImageCommandResponse> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateImageCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
            {
                return new CreateImageCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var image = Image.Create(request.ImageData,request.PropertyId);
            if (!image.IsSuccess)
            {
                return new CreateImageCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { image.Error }
                };
            }

            await repository.AddAsync(image.Value);

            return new CreateImageCommandResponse
            {
                Success = true,
                Image = new CreateImageDto
                {
                    ImageId = image.Value.ImageId,
                    ImageData = image.Value.ImageData,
                    PropertyId = image.Value.PropertyId
                }
            };
        }
    }
}
