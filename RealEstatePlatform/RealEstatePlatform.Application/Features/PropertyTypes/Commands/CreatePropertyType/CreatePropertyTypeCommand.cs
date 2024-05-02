using MediatR;

namespace RealEstatePlatform.Application.Features.PropertyTypes.Commands.CreatePropertyType
{
    public class CreatePropertyTypeCommand : IRequest<CreatePropertyTypeCommandResponse>
    {
        public string PropertyTypeName { get; set; } = default!;
    }
}
