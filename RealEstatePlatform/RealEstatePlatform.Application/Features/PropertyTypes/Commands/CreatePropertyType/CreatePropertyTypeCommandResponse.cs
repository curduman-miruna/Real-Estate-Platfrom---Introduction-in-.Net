using RealEstatePlatform.Application.Responses;

namespace RealEstatePlatform.Application.Features.PropertyTypes.Commands.CreatePropertyType
{
    public class CreatePropertyTypeCommandResponse : BaseResponse
    {
        public CreatePropertyTypeCommandResponse() : base()
        {
        }

        public CreatePropertyTypeDto PropertyType { get; set; }
    }
}
