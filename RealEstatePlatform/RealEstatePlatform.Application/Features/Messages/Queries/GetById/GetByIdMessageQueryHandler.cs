using MediatR;
using RealEstatePlatform.Application.Persistence;

namespace RealEstatePlatform.Application.Features.Messages.Queries.GetById
{
    public class GetByIdMessageQueryHandler : IRequestHandler<GetByIdMessageQuery, MessageDto>
    {
        private readonly IMessageRepository repository;
        public GetByIdMessageQueryHandler(IMessageRepository repository)
        {
            this.repository = repository;
        }

        public async Task<MessageDto> Handle(GetByIdMessageQuery request, CancellationToken cancellationToken)
        {
            var message = await repository.FindByIdAsync(request.Id);
            if (message.IsSuccess)
            {
                return new MessageDto
                {
                    MessageId = message.Value.MessageId,
                    MessageContent = message.Value.MessageContent,
                    SenderUsername = message.Value.SenderUsername,
                    ReceiverUsername = message.Value.ReceiverUsername
                };
            }
            return new MessageDto();
        }
    }
}
