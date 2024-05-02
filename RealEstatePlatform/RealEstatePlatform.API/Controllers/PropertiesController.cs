using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstatePlatform.Application.Features.Properties.Commands.CreateProperty;
using RealEstatePlatform.Application.Features.Properties.Commands.DeleteProperty;
using RealEstatePlatform.Application.Features.Properties.Commands.UpdateProperty;
using RealEstatePlatform.Application.Features.Properties.Queries.GetAll;
using RealEstatePlatform.Application.Features.Properties.Queries.GetById;

namespace RealEstatePlatform.API.Controllers
{

    public class PropertiesController : ApiControllerBase
    {
        private readonly IMediator mediator;

        public PropertiesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected override ISender Mediator => mediator;
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreatePropertyCommand command)
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
            var result = await Mediator.Send(new GetAllPropertiesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdPropertyQuery(id));
            return Ok(result);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deletePropertyCommand = new DeletePropertyCommand() { PropertyId = id };
            await Mediator.Send(deletePropertyCommand);
            return NoContent();
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(Guid id, UpdatePropertyCommand command)
        {
            if (id != command.PropertyId)
            {
                return BadRequest("Property ID mismatch");
            }
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
