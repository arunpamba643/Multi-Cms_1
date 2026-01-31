using AssociationBusiness.Handlers;
using WebsiteDemo.Models;

namespace WebsiteDemo.Services.Pages
{
    public interface IPageService
    {
        Task<IEnumerable<PageModel>> GetAllPages(int tenantId);
 
    }
}