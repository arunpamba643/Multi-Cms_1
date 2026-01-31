using AssociationBusiness.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace AssociationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContainerController(ILogger<CustomMenuController> logger, IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateContainer")]
        public async Task<IActionResult> CreateContainer([FromBody] CreateContainerRequest createContainerRequest)
        {
            try
            {
                var data = await _mediator.Send(createContainerRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("DeleteContainerByRowId/{Id}")]
        public async Task<IActionResult> DeleteContainerByRowId(int Id)
        {

            await _mediator.Send(new DeleteContainerByRowIdRequest() { Id = Id });
            return Ok();
        }

        [HttpGet("GetAllContainersById/{RowId}")]
        public async Task<IActionResult> GetAllContainersByRowId(int RowId)
        {
            var data = await _mediator.Send(new GetAllContainersByIdRequest() { Id = RowId });

            return Ok(data.AsEnumerable());
        }

    }
}
