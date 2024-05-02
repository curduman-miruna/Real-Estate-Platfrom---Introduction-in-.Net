using RealEstatePlatform.Application.Responses;


namespace RealEstatePlatform.Application.Features.Properties.Commands.UpdateProperty
{
    public class UpdatePropertyCommandResponse : BaseResponse
    {
        public UpdatePropertyCommandResponse() : base()
        {
        }

        public UpdatePropertyDto Property { get; set; }
    }
}