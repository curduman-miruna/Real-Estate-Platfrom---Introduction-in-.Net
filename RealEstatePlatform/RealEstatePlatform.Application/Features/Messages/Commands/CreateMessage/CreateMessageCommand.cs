using MediatR;

namespace RealEstatePlatform.Application.Features.Messages.Commands.CreateMessage
{
    public class CreateMessageCommand : IRequest<CreateMessageCommandResponse>
    {
        public string MessageContent { get; set; } = default!;
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
    }
}
