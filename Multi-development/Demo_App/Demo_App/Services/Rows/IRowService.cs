using AssociationBusiness.Handlers;
using AssociationEntities.CustomModels;
using Demo_App.Models;


namespace Demo_App.Services
{
    public interface IRowService
    {
        Task<IEnumerable<RowModel>> GetAllRowsByPageId(int PageId);
        Task<int> CreateRow(CreateRowRequest createRowRequest);
        Task DeletePageByRowId(int Id);

    }
}
