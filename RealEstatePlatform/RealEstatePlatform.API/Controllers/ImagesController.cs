using RealEstatePlatform.Application.Features.Images.Commands.CreateImage;
using RealEstatePlatform.Application.Features.Images.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;
using RealEstatePlatform.Application.Features.Images.Queries;
using RealEstatePlatform.Application.Features.Images.Commands.DeleteImage;
using RealEstatePlatform.Application.Features.Images.Commands.UpdateImage;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using RealEstatePlatform.Application.Features.Images.Queries.GetImagesByPropertyId;

namespace RealEstatePlatform.API.Controllers
{
    public class ImagesController : ApiControllerBase
    {

        private readonly IMediator mediator;

        public ImagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected override ISender Mediator => mediator;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreateImageCommand command)
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
            var deleteEventCommand = new DeleteImageCommand() { PropertyId = id };
            await Mediator.Send(deleteEventCommand);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllImagesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await Mediator.Send(new GetByIdImageQuery(id));
            return Ok(result);
        }

        [HttpGet("/propertyId/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetImagesByPropertyId(Guid id)
        {
            var result = await Mediator.Send(new GetImagesByPropertyIdQuery
            {
                PropertyId = id
            });
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update(UpdateImageCommand command)
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
