using RealEstatePlatform.Application.Responses;

namespace RealEstatePlatform.Application.Features.Properties.Commands.CreateProperty
{
    public class CreatePropertyCommandResponse : BaseResponse
    {
        public CreatePropertyCommandResponse() : base()
        {
        }

        public CreatePropertyDto Property { get; set; }
    }
    
}
