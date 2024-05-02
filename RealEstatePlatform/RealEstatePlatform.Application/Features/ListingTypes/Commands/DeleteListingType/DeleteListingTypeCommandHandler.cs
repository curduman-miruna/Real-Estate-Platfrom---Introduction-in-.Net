using RealEstatePlatform.Application.Persistence;
using MediatR;


namespace RealEstatePlatform.Application.Features.ListingTypes.Commands.DeleteListingType
{
    public class DeleteListingTypeCommandHandler : IRequestHandler<DeleteListingTypeCommand, DeleteListingTypeCommandResponse>
    {
        private readonly IListingTypeRepository repository;

        public DeleteListingTypeCommandHandler(IListingTypeRepository repository)
        {
            this.repository = repository;
        }

        public async Task<DeleteListingTypeCommandResponse> Handle(DeleteListingTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteAsync(request.ListingTypeId);
            if (!result.IsSuccess)
            {
                return new DeleteListingTypeCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { result.Error }
                };
            }
            return new DeleteListingTypeCommandResponse
            {
                Success = true
            };
        }
    }
}
