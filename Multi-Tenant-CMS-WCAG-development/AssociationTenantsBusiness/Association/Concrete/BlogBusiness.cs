using AssociationEntities.Models;
using AssociationRepository.Association;

namespace AssociationBusiness.Association
{
    public class BlogBusiness : IBlogBusiness
    {
        private readonly IBlogRepository _blogRepository;
        public BlogBusiness(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public Task<int> CreateBlogImages(List<BlogImage> blogImages)
        {
            return _blogRepository.CreateBlogImages(blogImages);
        }

        public List<Blog> GetAllBlogs()
        {
            var data = _blogRepository.GetAllBlogs();
            return data;
        }

    }
}
