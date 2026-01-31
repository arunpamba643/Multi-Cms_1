using AssociationBusiness.Handlers;
using AssociationEntities.CustomModels;
using AssociationEntities.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AssociationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPageController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;

        public BlogPageController(ILogger<CustomMenuController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Method to Get All Blog Pages
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllBlogPages")]
        public async Task<IActionResult> GetAllBlogPages([FromQuery] GetBlogPagesRequest getBlogPagesRequest)
        {
            var data = await _mediator.Send(getBlogPagesRequest);

            return Ok(data.AsEnumerable());
        }

        /// <summary>
        /// Method to Get Blog Page By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBlogPageById/{Id}")]
        public async Task<IActionResult> GetBlogPageById(int Id)
        {
            var data = await _mediator.Send(new GetBlogPageById() { Id = Id });
            return Ok(data);
        }

        /// <summary>
        /// Method to Create Blog Page
        /// </summary>
        /// <param name="createPageRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateBlogPage([FromBody] CreateBlogPageRequest createBlogPageRequest)
        {
            try
            {
                var data = await _mediator.Send(createBlogPageRequest);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Method to Delete Page
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteBlogPageById/{Id}")]
        public async Task<IActionResult> DeleteBlogPageById(int Id)
        {
            await _mediator.Send(new DeleteBlogPageById() { Id = Id });
            return Ok();
        }

        /// <summary>
        /// Method to update Page
        /// </summary>
        /// <param name="updateBlogPageRequest"></param>
        /// <returns></returns>
        [HttpPost("UpdateBlogPage")]
        public async Task<IActionResult> UpdateBlogPage([FromBody] UpdateBlogPageRequest updateBlogPageRequest)
        {
            await _mediator.Send(updateBlogPageRequest);
            return Ok();
        }

        /// <summary>
        /// Retrieves suggestions for blog filter options based on the provided TenantId.
        /// </summary>
        /// <param name="TenantId">The identifier of the tenant.</param>
        [HttpGet("GetBlogFilterSuggestions/{TenantId}")]
        public async Task<IActionResult> GetBlogFilterSuggestions(int TenantId)
        {
            var data = await _mediator.Send(new GetBlogFilterSuggestions() { TenantId = TenantId });
            return Ok(data);
        }



    }
}
