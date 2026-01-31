using AssociationBusiness.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AssociationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        public PageController(ILogger<CustomMenuController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Method to Get All Pages
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllPages/{TenantId}")]
        public async Task<IActionResult> GetAllPages(int TenantId)
        {
            var data = await _mediator.Send(new GetAllPageRequest() { TenantId = TenantId });

            return Ok(data.AsEnumerable());
        }

        /// <summary>
        /// Method to Get Page By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPageById/{Id}")]
        public async Task<IActionResult> GetPageById(int Id)
        {
            var data = await _mediator.Send(new GetPageById() { Id = Id });
            return Ok(data);
        }

        /// <summary>
        /// Method to Create Page
        /// </summary>
        /// <param name="createPageRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreatePage([FromBody] CreatePageRequest createPageRequest)
        {
            try
            {
                var data = await _mediator.Send(createPageRequest);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Method to Delete Page By Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("DeletePageById/{Id}")]
        public async Task<IActionResult> DeletePageById(int Id)
        {
            await _mediator.Send(new DeletePageById() { Id = Id });
            return Ok();
        }


        /// <summary>
        /// Method to Update Page
        /// </summary>
        /// <returns></returns>
        [HttpPost("UpdatePage")]
        public async Task<IActionResult> UpdatePage(UpatePageRequest upatePageRequest)
        {
            await _mediator.Send(upatePageRequest);
            return Ok();
        }


        /// <summary>
        /// Method to check if page is mapped to a menu
        /// </summary>
        /// <returns></returns>
        [HttpGet("CheckIfPageMappedToMenu/{menuId}/{pageId}")]
        public async Task<IActionResult> ChekcIfPageMapped(int menuId, int pageId)
        {
            var data = await _mediator.Send(new CheckIfPageMappedRequest { MenuId = menuId, pageId = pageId });
            return Ok(data);
        }

        /// <summary>
        /// Method to check if resource path is available
        /// </summary>
        /// <returns></returns>
        [HttpGet("CheckIfResourcePathAvailable/{TenantId}/{ResourcePath}/{PageId}")]
        public async Task<IActionResult> CheckIfResourcePathAvailable(int TenantId, string ResourcePath, int PageId)
        {
            var data = await _mediator.Send(new CheckIfResourcePathAvailableRequest { TenantId = TenantId, ResourcePath = ResourcePath, PageId = PageId });
            return Ok(data);
        }

    }
}
