using AssociationBusiness.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AssociationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentPropertyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ComponentPropertyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateComponentProperty")]
        public async Task<IActionResult> CreateCreateComponentProperty([FromBody] CreateComponentPropertyRequest createComponentPropertyRequest)
        {
            try
            {
                await _mediator.Send(createComponentPropertyRequest);
                return Ok(1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("CreateComponentProperties")]
        public async Task<IActionResult> CreateComponentProperties([FromBody] CreateComponentPropertiesRequest createComponentPropertiesRequest)
        {
            try
            {
                var data = await _mediator.Send(createComponentPropertiesRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("DeleteComponentPropertiesByComponentId/{Id}")]
        public async Task<IActionResult> DeleteComponentPropertiesByComponentId(int Id)
        {
            await _mediator.Send(new DeleteComponentPropertiesRequest() { ComponentId = Id });
            return Ok();
        }
    }
}
