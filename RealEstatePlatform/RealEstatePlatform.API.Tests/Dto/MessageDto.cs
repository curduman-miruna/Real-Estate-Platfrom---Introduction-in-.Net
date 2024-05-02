namespace RealEstatePlatform.API.Tests.Dto
{
    public class MessageDto
    {
        public Guid MessageId { get; set; }
        public string MessageContent { get; set; }
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
    }
}
