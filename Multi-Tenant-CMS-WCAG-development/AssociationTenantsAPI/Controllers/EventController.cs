using AssociationBusiness.Handlers;
using AssociationEntities.CustomModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AssociationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllEvents")]
        public async Task<ActionResult<List<EventModel>>> GetEvents([FromQuery] GetEventsRequest request)
        {
            var rows = await _mediator.Send(request);

            if (rows == null)
            {
                return NotFound();
            }

            return Ok(rows);
        }

    }
}
