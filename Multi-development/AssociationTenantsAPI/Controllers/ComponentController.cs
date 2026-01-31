using AssociationBusiness.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace AssociationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public ComponentController(IMediator mediator)
        {

            _mediator = mediator;
        }

        [HttpGet("GetAllComponentsByContainerId/{Id}")]
        public async Task<IActionResult> GetAllComponentsByContainerId(int Id)
        {

            var data = await _mediator.Send(new GetComponentByContainerIdRequest() { Id = Id });
            return Ok(data.AsEnumerable());
        }

        [HttpGet("DeleteComponentByContainerId/{id}")]

        public async Task<IActionResult> DeleteComponentByContainerId(int Id)
        {

            await _mediator.Send(new DeleteComponentByContainerIdRequest() { Id = Id });
            return Ok();
        }

        [HttpGet("DeleteComponentByComponentId/{id}")]
        public async Task<IActionResult> DeleteComponentByComponentId(int Id)
        {
            await _mediator.Send(new DeleteComponentByComponentIdRequest() { Id = Id });
            return Ok();
        }


        [HttpPost("CreateComponent")]
        public async Task<IActionResult> CreateComponent([FromBody] CreateComponentRequest CreateComponentRequest)
        {
            try
            {
                var data = await _mediator.Send(CreateComponentRequest);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateComponents")]
        public async Task<IActionResult> CreateComponents([FromBody] CreateComponentsRequest createComponentsRequest)
        {
            try
            {
                var data = await _mediator.Send(createComponentsRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UpdateComponent")]
        public async Task<IActionResult> UpdateComponent([FromBody] UpdateComponentRequest updateComponentRequest)
        {
            try
            {
                await _mediator.Send(updateComponentRequest);
                return Ok(1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
