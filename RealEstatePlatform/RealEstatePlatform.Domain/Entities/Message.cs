using RealEstatePlatform.Domain.Common;

namespace RealEstatePlatform.Domain.Entities
{
    public class Message : AuditableEntity
    {
        public Message(string messageContent, string senderUsername, string receiverUsername)
        {
            MessageId = Guid.NewGuid();
            MessageContent = messageContent;
            SenderUsername = senderUsername;
            ReceiverUsername = receiverUsername;
        }
        public Guid MessageId { get; private set; }
        public string MessageContent { get; private set; } = null!;
        public string SenderUsername { get; private set; }
        public string ReceiverUsername { get; private set; }

        public static Result<Message> Create(string messageContent, string senderUsername, string receiverUsername)
        {
            if (string.IsNullOrWhiteSpace(messageContent))
                return Result<Message>.Failure("Message content cannot be empty");
            if (string.IsNullOrWhiteSpace(senderUsername))
                return Result<Message>.Failure("Sender id cannot be empty");
            if (string.IsNullOrWhiteSpace(receiverUsername))
                return Result<Message>.Failure("Receiver id cannot be empty");
            return Result<Message>.Success(new Message(messageContent, senderUsername, receiverUsername));
        }
    }
}
