using AssociationBusiness.Handlers;
using AssociationEntities.Models.Api;
using MediatR;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace AssociationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomMenuController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        public CustomMenuController(ILogger<CustomMenuController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Method to Get All Menu
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllMenus/{TenantId}")]
        public async Task<IActionResult> GetAllMenus(int TenantId)
        {
            var data = await _mediator.Send(new GetAllMenuRequest() { TenantId = TenantId});

            return Ok(data.AsEnumerable());
        }

        /// <summary>
        /// Method to Get Menu By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenuById/{Id}")]
        public async Task<IActionResult> GetMenuById(int Id)
        {
            var data = await _mediator.Send(new GetMenuById() { Id = Id });
            return Ok(data);
        }

        /// <summary>
        /// Method to Create Menu
        /// </summary>
        /// <param name="createBloggingRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] CreateMenuRequest createMenuRequest)
        {
            try
            {
                var data = await _mediator.Send(createMenuRequest);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Method to Delete Menus By Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("DeleteMenuById/{Id}")]
        public async Task<IActionResult> DeleteMenuById(int Id)
        {
            await _mediator.Send(new DeleteMenuById() { Id = Id });
            return Ok();
        }

        ///// <summary>
        ///// Method to Create Menu
        ///// </summary>
        ///// <param name="createMenuRequest"></param>
        ///// <returns></returns>
        //[HttpPost("BulkUpdateMenu")]
        //public async Task<IActionResult> BulkUpdateMenu([FromBody] BulkUpdateMenuRequest createMenuRequest)
        //{
        //    try
        //    {
        //        var data = await _mediator.Send(createMenuRequest);

        //        return Ok(data);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpPost("UpdateMenu")]
        public async Task<IActionResult> UpdateMenu([FromBody] updateMenuRequest updateMenuRequest )
        {
            await _mediator.Send(updateMenuRequest);
            return Ok();
        }
    }
}
