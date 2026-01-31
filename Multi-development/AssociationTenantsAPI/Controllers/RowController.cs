using AssociationBusiness.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace AssociationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RowController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public RowController(IMediator mediator)
        {

            _mediator = mediator;
        }

        [HttpGet("GetAllRowsByPageId/{PageId}")]
        public async Task<IActionResult> GetAllRowByPageId(int PageId)
        {
            var data = await _mediator.Send(new GellAllRowsRequest() { PageId = PageId });
            return Ok(data.AsEnumerable());
        }

        [HttpPost("CreateRow")]
        public async Task<IActionResult> CreateRow([FromBody] CreateRowRequest createRowRequest)
        {
            try
            {
                var data = await _mediator.Send(createRowRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("DeleteRowById/{Id}")]
        public async Task<IActionResult> DeletePageId(int Id)
        {
            await _mediator.Send(new DeleteRowByIdRequest () { Id = Id });
            return Ok();
        }

    }
}
