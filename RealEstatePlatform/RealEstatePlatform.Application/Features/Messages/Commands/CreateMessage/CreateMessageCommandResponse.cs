using RealEstatePlatform.Application.Responses;

namespace RealEstatePlatform.Application.Features.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandResponse : BaseResponse
    {
        public CreateMessageCommandResponse() : base()
        {
        }
        public CreateMessageDto Message { get; set; }
    }
}
