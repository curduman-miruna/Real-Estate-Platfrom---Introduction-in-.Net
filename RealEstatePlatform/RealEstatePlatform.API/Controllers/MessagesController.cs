using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePlatform.Application.Features.Messages.Commands.CreateMessage;
using RealEstatePlatform.Application.Features.Messages.Queries.GetAll;
using RealEstatePlatform.Application.Features.Messages.Queries.GetById;

namespace RealEstatePlatform.API.Controllers
{
    public class MessagesController : ApiControllerBase
    {

        private readonly IMediator mediator;

        public MessagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected override ISender Mediator => mediator;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateMessageCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllMessagesQuery());
            return Ok(result);
        }

        [Authorize(Roles = "User, Admin, Agent")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdMessageQuery(id));
            return Ok(result);
        }

    }
       
}
