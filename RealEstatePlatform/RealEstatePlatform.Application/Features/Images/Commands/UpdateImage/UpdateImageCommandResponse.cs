using RealEstatePlatform.Application.Responses;


namespace RealEstatePlatform.Application.Features.Images.Commands.UpdateImage
{
    public class UpdateImageCommandResponse : BaseResponse
    {
        public UpdateImageCommandResponse() : base()
        {
        }

        public UpdateImageDto Image { get; set; }
    }
}