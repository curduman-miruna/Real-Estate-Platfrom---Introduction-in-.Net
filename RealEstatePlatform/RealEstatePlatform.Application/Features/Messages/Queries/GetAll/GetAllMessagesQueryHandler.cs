using MediatR;
using RealEstatePlatform.Application.Persistence;

namespace RealEstatePlatform.Application.Features.Messages.Queries.GetAll
{
    public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, GetAllMessagesResponse>
    {
        private readonly IMessageRepository repository;
        public GetAllMessagesQueryHandler(IMessageRepository repository)
        {
            this.repository = repository;
        }

        public async Task<GetAllMessagesResponse> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
        {
            GetAllMessagesResponse response = new();
            var result = await repository.GetAllAsync();
            if (result.IsSuccess)
            {
                response.Messages = result.Value.Select(m => new MessageDto
                {
                    MessageId = m.MessageId,
                    MessageContent = m.MessageContent,
                    SenderUsername = m.SenderUsername,
                    ReceiverUsername = m.ReceiverUsername,
                    CreatedDate = m.CreatedDate
                }).ToList();
            }
            return response;
        }
    }
}
