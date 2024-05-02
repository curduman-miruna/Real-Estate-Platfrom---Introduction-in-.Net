using MediatR;
using RealEstatePlatform.Application.Persistence;

namespace RealEstatePlatform.Application.Features.PropertyTypes.Commands.DeletePropertyType
{
    public class DeletePropertyTypeCommandHandler : IRequestHandler<DeletePropertyTypeCommand, DeletePropertyTypeCommandResponse>
    {
        public readonly IPropertyTypeRepository repository;

        public DeletePropertyTypeCommandHandler(IPropertyTypeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeletePropertyTypeCommandResponse> Handle(DeletePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteAsync(request.PropertyTypeId);
            if (!result.IsSuccess)
            {
                return new DeletePropertyTypeCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }
            return new DeletePropertyTypeCommandResponse
            {
                Success = true
            };
        }
    }
}
