using AssociationBusiness.Handlers;
using AssociationBusiness.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AssociationAPI.Controllers
{
    /// <summary>
    /// Created Mediatr with Onion Architecture
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BloggingController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        public BloggingController(ILogger<BloggingController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Method to Get All Bloggings
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllBloggings")]
        public async Task<IActionResult> GetAllBloggings()
        {
            try
            {
                var data = await _mediator.Send(new GetAllBlogRequest());

                await _mediator.Publish(new AuditRequest() { Message = "Successfully published" });
                return Ok(data.AsEnumerable());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Method to Get All Blog Images
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllBlogImages/{TenantId}/{BlogId}")]
        public async Task<IActionResult> GetAllBlogImages(int TenantId, int BlogId)
        {
            try
            {
                var data = await _mediator.Send(new GetAllBlogImagesRequest() { BlogId = BlogId,TenantId = TenantId });

                return Ok(data.AsEnumerable());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Method to Get Bloggings By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBloggingById/{Id}")]
        public async Task<IActionResult> GetBloggingById(int Id)
        {
            try
            {
                var data = await _mediator.Send(new GetBloggingById() { Id = Id });

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Method to Create Blog
        /// </summary>
        /// <param name="createBloggingRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateBlogging([FromBody] CreateBloggingRequest createBloggingRequest)
        {
            try
            {
                var data = await _mediator.Send(createBloggingRequest);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Method to Create Blog
        /// </summary>
        /// <param name="createBloggingRequest"></param>
        /// <returns></returns>
        [HttpPost("CreateBlogImage")]
        public async Task<IActionResult> CreateBlogImage([FromBody] CreateBlogImageRequest createBlogImage)
        {
            try
            {
                var data = await _mediator.Send(createBlogImage);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Method to Delete Blog Image By Id
        /// </summary>
        /// <returns></returns>
        [HttpDelete("DeleteBlogImageById/{Id}")]
        public async Task<IActionResult> DeleteBlogImageById(int Id)
        {
            await _mediator.Send(new DeleteBlogImageById() { Id = Id });
            return Ok();
        }

    }
}
