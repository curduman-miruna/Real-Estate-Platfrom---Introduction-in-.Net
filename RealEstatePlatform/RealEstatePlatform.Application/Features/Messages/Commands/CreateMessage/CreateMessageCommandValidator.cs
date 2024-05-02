using FluentValidation;
using RealEstatePlatform.Application.Persistence;

namespace RealEstatePlatform.Application.Features.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
    {
        private readonly IMessageRepository repository;

        public CreateMessageCommandValidator(IMessageRepository repository)
        {
            RuleFor(p => p.MessageContent)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(p => p.SenderUsername)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(p => p.ReceiverUsername)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

        }

    }
}
