using WebsiteDemo.Models;
using AssociationEntities.Models;
 
namespace WebsiteDemo.Services
{
    public  interface IRowService
    {
        Task<IEnumerable<Row>> GetAllRowsByPageId(int PageId);
    }
}
