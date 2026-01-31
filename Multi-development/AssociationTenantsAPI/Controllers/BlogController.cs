using AssociationBusiness.Association;
using AssociationEntities.CustomModels;
using AssociationEntities.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssociationAPI.Controllers
{
    /// <summary>
    /// This Controller created for sample for 3 layer architecture with out mediatr
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogBusiness _blogBusiness;
        public BlogController(IBlogBusiness blogBusiness)
        {
            _blogBusiness = blogBusiness;
        }

        /// <summary>
        /// Get All Blogs
        /// </summary> 
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetAllBlogs()
        {
            var data = _blogBusiness.GetAllBlogs();

            return Ok(data);
        }

        /// <summary>
        /// Get All Blogs By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllBlogs(int Id)
        {
            var data = _blogBusiness.GetAllBlogs();

            return Ok(data);
        }

        /// <summary>
        /// Method to Create Blog
        /// </summary>
        /// <param name="createBloggingRequest"></param>
        /// <returns></returns>
        [HttpPost("CreateBlogImages")]
        public async Task<IActionResult> CreateBlogImages([FromBody] List<BlogImageModel> blogImages)
        {
            try
            {
                var createBlogImage = new List<BlogImage>();
                foreach (var image in blogImages)
                {
                    createBlogImage.Add(new BlogImage()
                    {
                        BlogId = image.BlogId,
                        TenantId = image.TenantId,
                        ImageId = image.ImageId,
                        CreatedDate = DateTime.Now,
                        UpdatedDate= DateTime.Now,
                        ImageUrl = image.ImageUrl,
                        Position = image.Position,
                        Text = image.Text,
                        Udf = image.Udf
                    });
                }

                var data = await _blogBusiness.CreateBlogImages(createBlogImage);

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
