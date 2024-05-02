using RealEstatePlatform.Application.Features.ListingTypes.Commands.CreateListingType;
using RealEstatePlatform.Application.Features.ListingTypes.Queries.GetAll;
using RealEstatePlatform.Application.Features.ListingTypes.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RealEstatePlatform.API.Controllers;
using RealEstatePlatform.Application.Features.ListingTypes.Commands.DeleteListingType;
using RealEstatePlatform.Application.Features.ListingTypes.Commands.UpdateListingType;
using Microsoft.AspNetCore.Authorization;

namespace RealEstatePlatform.API.Controllers
{
    public class ListingTypesController : ApiControllerBase
    {
        private readonly IMediator mediator;

        public ListingTypesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected override ISender Mediator => mediator;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateListingTypeCommand command)
        {
            var result = await Mediator.Send(command);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

       
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleteEventCommand = new DeleteListingTypeCommand() { ListingTypeId = id };
            await Mediator.Send(deleteEventCommand);
            return NoContent();
        }

 
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllListingTypesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdListingTypeQuery(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(UpdateListingTypeCommand command)
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
