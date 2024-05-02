using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstatePlatform.Application.Features.PropertyTypes.Commands.CreatePropertyType;
using RealEstatePlatform.Application.Features.PropertyTypes.Commands.DeletePropertyType;
using RealEstatePlatform.Application.Features.PropertyTypes.Commands.UpdatePropertyType;
using RealEstatePlatform.Application.Features.PropertyTypes.Queries.GetAll;
using RealEstatePlatform.Application.Features.PropertyTypes.Queries.GetById;

namespace RealEstatePlatform.API.Controllers
{
    public class PropertyTypesController : ApiControllerBase
    {
        private readonly IMediator mediator;

        public PropertyTypesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected override ISender Mediator => mediator;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreatePropertyTypeCommand command)
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
            var result = await Mediator.Send(new GetAllPropertyTypesQuery());
            return Ok(result);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdPropertyTypeQuery(id));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deletePropertyTypeCommand = new DeletePropertyTypeCommand() { PropertyTypeId = id };
            await Mediator.Send(deletePropertyTypeCommand);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(UpdatePropertyTypeCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
