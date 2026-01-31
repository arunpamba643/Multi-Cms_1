
using AssociationEntities.Models;

namespace AssociationRepository.Association
{
    /// <summary>
    /// Blog Repository
    /// </summary>
    public class BlogRepository : IBlogRepository
    {
        private readonly AssociationContext _associationContext;
        public BlogRepository(AssociationContext associationContext)
        {
            _associationContext = associationContext;
        }

        /// <summary>
        ///  Method to Get All Blogs
        /// </summary>
        /// <returns></returns>
        public List<Blog> GetAllBlogs()
        {
            var data = _associationContext.Blogs.ToList();
            return data;
        }

        /// <summary>
        ///  Method to Get Blog by Id
        /// </summary>
        /// <returns></returns>
        public Blog GetBlogById(int Id)
        {
            var data = _associationContext.Blogs.Where(x => x.TenantId == Id).OrderByDescending(x => x.BlogId).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// Method to create Blog
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        public async Task<int> CreateBlog(Blog blog)
        {


            blog.PublishedDateTime = DateTime.Now;
            _associationContext.Blogs.Add(blog);
            var data = await _associationContext.SaveChangesAsync();
            return blog.BlogId;
        }

        public async Task<bool> CheckTenantBlogExists(int tenantId, int blogId)
        {
            var data = _associationContext.Blogs.Where(x => x.TenantId == tenantId && x.BlogId == blogId).FirstOrDefault();
            if (data != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Method to Update blog
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        public async Task<int> UpdateBlog(Blog blog)
        {
            var blogdata = _associationContext.Blogs.FirstOrDefault(u => u.BlogId == blog.BlogId);

            if (blogdata != null)
            {
                blogdata.UpdatedDateTime = DateTime.Now;
                blogdata.HeaderBackgroundColor = blog.HeaderBackgroundColor;
                blogdata.HeaderFontColor = blog.HeaderFontColor;
                blogdata.Logo = blog.Logo;
                blogdata.Banner = blog.Banner;
                blogdata.BrandName = blog.BrandName;
                blogdata.Heading = blog.Heading;
                blogdata.FooterBackgroundColor = blog.FooterBackgroundColor;
                blogdata.TextContent = blog.TextContent;
                blogdata.FooterText = blog.FooterText;
                blogdata.ContentFontColor = blog.ContentFontColor;
                blogdata.HeaderFontStyle = blog.HeaderFontStyle;
                blogdata.ContentFontStyle = blog.ContentFontStyle;
                blogdata.FooterFontStyle = blog.FooterFontStyle;
                blogdata.ContentFontSize = blog.ContentFontSize;
                blogdata.MainMenuFontStyle = blog.MainMenuFontStyle;
                blogdata.MainMenuFontSize = blog.MainMenuFontSize;
                blogdata.MainMenuColor = blog.MainMenuColor;
                blogdata.SubMenuColor = blog.SubMenuColor;
                blogdata.SubMenuFontSize = blog.SubMenuFontSize;
                blogdata.SubMenuFontStyle = blog.SubMenuFontStyle;
                blogdata.SubChildFontStyle = blog.SubChildFontStyle;
                blogdata.SubChildMenuColor = blog.SubChildMenuColor;
                blogdata.SubChildMenuFontSize = blog.SubChildMenuFontSize;
                blogdata.Facebook = blog.Facebook;
                blogdata.LinkedIn = blog.LinkedIn;
                blogdata.Twitter = blog.Twitter;
                blogdata.Instagram = blog.Instagram;
                blogdata.WhatsApp = blog.WhatsApp;
                blogdata.FooterFontColor = blog.FooterFontColor;
                await _associationContext.SaveChangesAsync();

            }
            return blogdata.BlogId;
        }

        /// <summary>
        /// Method to create Blog Image
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        public async Task<int> CreateBlogImage(BlogImage blog)
        {
            _associationContext.BlogImages.Add(blog);
            var data = await _associationContext.SaveChangesAsync();
            return data;
        }

        /// <summary>
        /// Method to create Blog Images
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        public async Task<int> CreateBlogImages(List<BlogImage> blog)
        {
            try
            {
                if (blog.Count() > 0 && blog.Where(x => x.ImageId == 0).Count() > 0)
                {
                    var images = blog.Where(x => x.ImageId == 0).Where(x => x.BlogId != 0 && x.TenantId != 0).ToList();
                    _associationContext.BlogImages.AddRange(images);
                    var data = _associationContext.SaveChanges();
                    if (data != null)
                    {

                    }
                }
                if (blog.Where(x => x.ImageId != 0).Count() > 0)
                {
                    var images = blog.Where(x => x.ImageId != 0).Where(x => x.BlogId != 0 && x.TenantId != 0).ToList();
                    _associationContext.BlogImages.AddRange(images);
                    await _associationContext.SaveChangesAsync();
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        /// <summary>
        /// Method to Update blog
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        public async Task<int> UpdateBlogImage(BlogImage blog)
        {
            var blogdata = _associationContext.BlogImages.FirstOrDefault(u => u.ImageId == blog.ImageId);

            if (blogdata != null)
            {
                blogdata.ImageUrl = blog.ImageUrl;
                blogdata.Text = blog.Text;
                blogdata.Position = blog.Position;
                blogdata.Udf = blog.Udf;
                await _associationContext.SaveChangesAsync();
            }
            return 1;
        }

        /// <summary>
        ///  Method to Get All Blog Images
        /// </summary>
        /// <returns></returns>
        public List<BlogImage> GetAllBlogImages(int BlogId, int TenantId)
        {
            var data = _associationContext.BlogImages.Where(x => x.BlogId == BlogId && x.TenantId == TenantId).ToList();
            return data;
        }

        /// <summary>
        /// Method to Delete Blog Image By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task DeleteBlogImageById(int Id)
        {
            var blogImage = _associationContext.BlogImages.FirstOrDefault(u => u.ImageId == Id);

            if (blogImage != null)
            {
                _associationContext.BlogImages.Remove(blogImage);
                await _associationContext.SaveChangesAsync();
            }
        }
    }
}