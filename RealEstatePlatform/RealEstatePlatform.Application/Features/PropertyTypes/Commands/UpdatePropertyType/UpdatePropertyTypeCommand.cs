using MediatR;

namespace RealEstatePlatform.Application.Features.PropertyTypes.Commands.UpdatePropertyType
{
    public class UpdatePropertyTypeCommand : IRequest<UpdatePropertyTypeCommandResponse>
    {
        public Guid PropertyTypeId { get; set; }
        public string PropertyTypeName { get; set; } = default!;
    }
}
