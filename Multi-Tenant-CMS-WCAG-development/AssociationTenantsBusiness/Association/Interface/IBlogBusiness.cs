using AssociationEntities.Models;

namespace AssociationBusiness.Association
{
    public interface IBlogBusiness
    {
        List<Blog> GetAllBlogs();

        Task<int> CreateBlogImages(List<BlogImage> blogImages);
    }
}
