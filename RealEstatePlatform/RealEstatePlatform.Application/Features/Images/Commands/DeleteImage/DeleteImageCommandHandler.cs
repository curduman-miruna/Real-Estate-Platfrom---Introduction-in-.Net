using RealEstatePlatform.Application.Persistence;
using MediatR;

namespace RealEstatePlatform.Application.Features.Images.Commands.DeleteImage
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, DeleteImageCommandResponse>
    {
        private readonly IImageRepository repository;

        public DeleteImageCommandHandler(IImageRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteImageCommandResponse> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteAsync(request.PropertyId);
            if (!result.IsSuccess)
            {
                return new DeleteImageCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }
            return new DeleteImageCommandResponse
            {
                Success = true
            };
        }
    }
}
