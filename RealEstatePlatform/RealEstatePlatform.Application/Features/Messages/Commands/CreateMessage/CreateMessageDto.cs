namespace RealEstatePlatform.Application.Features.Messages.Commands.CreateMessage
{
    public class CreateMessageDto
    {
        public Guid MessageId { get; set; }
        public string MessageContent { get; set; } = null!;
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
    }
}
