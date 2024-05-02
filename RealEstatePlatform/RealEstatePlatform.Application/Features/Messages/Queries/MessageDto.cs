namespace RealEstatePlatform.Application.Features.Messages.Queries
{
    public class MessageDto
    {
        public Guid MessageId { get; set; }
        public string MessageContent { get; set; } = default!;
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
