using MediatR;
using Microsoft.Extensions.Logging;
using RealEstatePlatform.Application.Contracts;
using RealEstatePlatform.Application.Models;
using RealEstatePlatform.Application.Persistence;
using RealEstatePlatform.Domain.Entities;

namespace RealEstatePlatform.Application.Features.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, CreateMessageCommandResponse>
    {
        private readonly IMessageRepository repository;

        public CreateMessageCommandHandler(IMessageRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CreateMessageCommandResponse> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateMessageCommandValidator(repository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new CreateMessageCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }
            //Trimit mail, persoanei care a primit mesaj
            var message = Message.Create(request.MessageContent, request.SenderUsername, request.ReceiverUsername);
            if(!message.IsSuccess)
            {
                return new CreateMessageCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { message.Error }
                };
            }

            await repository.AddAsync(message.Value);


            return new CreateMessageCommandResponse
            {
                Success = true,
                Message = new CreateMessageDto
                {
                    MessageId = message.Value.MessageId,
                    MessageContent = message.Value.MessageContent,
                    SenderUsername = message.Value.SenderUsername,
                    ReceiverUsername = message.Value.ReceiverUsername
                }
            };

            
        }
       
    }
}
