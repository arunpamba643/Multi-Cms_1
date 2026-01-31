using AssociationEntities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace AssociationRepository.Association
{
    public class RowRepository : IRowRepository
    {
        private readonly AssociationContext _associationContext;
        public RowRepository(AssociationContext associationContext)
        {
            _associationContext = associationContext;
        }

        /// <summary>
        /// Retrieves all rows associated with a specific page from the database.
        /// </summary>
        public IEnumerable<Row> GetAllRowsByPageId(int PageId)
        {

            var rows = _associationContext.Rows.Include(x => x.Containers).ThenInclude(c => c.Components).ThenInclude(cp => cp.ComponentProperties).Where(x => x.PageId == PageId)
    .OrderBy(x => x.RowId)
    .ToList();

            return rows;
        }

        /// <summary>
        /// Deletes a row from the database by its ID.
        /// </summary>
        /// <param name="RowId">The ID of the row to be deleted.</param>
        public async Task DeleteRowById(int RowId)
        {
            try
            {

                var row = _associationContext.Rows.FirstOrDefault(u => u.RowId == RowId);
                if (row != null)
                {
                    _associationContext.Rows.Remove(row);
                    await _associationContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        /// <summary>
        /// Creates a new row in the database.
        /// </summary>
        /// <param name="Item">The row object to be created.</param>
        public async Task<int> CreateRow(Row Item)
        {
            try
            {
                _associationContext.Rows.Add(Item);
                var data = await _associationContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            return Item.RowId;


        }
    }
}
