using AssociationEntities.Models;

namespace AssociationRepository.Association
{
    public interface IRowRepository
    {
        IEnumerable<Row> GetAllRowsByPageId(int PageId);
        Task DeleteRowById(int RowId);
        Task<int> CreateRow(Row Item);


    }
}
