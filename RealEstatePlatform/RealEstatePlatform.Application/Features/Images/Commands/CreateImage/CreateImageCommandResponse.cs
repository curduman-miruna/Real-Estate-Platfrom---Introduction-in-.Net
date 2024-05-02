using RealEstatePlatform.Application.Responses;

namespace RealEstatePlatform.Application.Features.Images.Commands.CreateImage
{
    public class CreateImageCommandResponse : BaseResponse
    {
        public CreateImageCommandResponse() : base()
        {
        }

        public CreateImageDto Image { get; set; }
    }
}
