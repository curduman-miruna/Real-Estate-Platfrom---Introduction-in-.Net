namespace RealEstatePlatform.App.ViewModels
{
    public class MessageViewModel
    {
        public Guid MessageId { get; set; }
        public string MessageContent { get; set; } = null!;
        public string SenderUsername { get; set; }
        public string ReceiverUsername { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
