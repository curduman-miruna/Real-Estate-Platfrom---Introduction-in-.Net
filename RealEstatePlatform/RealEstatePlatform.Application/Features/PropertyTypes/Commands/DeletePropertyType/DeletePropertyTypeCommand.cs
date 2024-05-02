using MediatR;

namespace RealEstatePlatform.Application.Features.PropertyTypes.Commands.DeletePropertyType
{
    public class DeletePropertyTypeCommand : IRequest<DeletePropertyTypeCommandResponse>
    {
        public Guid PropertyTypeId { get; set; }
    }
}
