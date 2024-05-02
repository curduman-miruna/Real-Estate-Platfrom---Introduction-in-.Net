using RealEstatePlatform.Application.Responses;


namespace RealEstatePlatform.Application.Features.PropertyTypes.Commands.UpdatePropertyType
{
    public class UpdatePropertyTypeCommandResponse : BaseResponse
    {
        public UpdatePropertyTypeCommandResponse() : base()
        {
        }

        public UpdatePropertyTypeDto PropertyType { get; set; }
    }
}