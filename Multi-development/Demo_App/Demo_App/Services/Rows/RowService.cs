using AssociationBusiness.Handlers;
using AssociationEntities.CustomModels;
using Demo_App.Models;
using System.Collections.Generic;

namespace Demo_App.Services
{
    public class RowService : IRowService
    {
        private readonly HttpClient _httpClient;
        public RowService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Retrieves all rows associated with a specified page asynchronously.
        /// </summary>
        /// <param name="PageId">The ID of the page to retrieve rows for.</param>
        public async Task<IEnumerable<RowModel>> GetAllRowsByPageId(int pageId)
        {
           
            var rows = await _httpClient.GetFromJsonAsync<List<RowModel>>($"api/Row/GetAllRowsByPageId/"+ pageId );
            return rows;
        }

        /// <summary>
        /// Creates a row asynchronously based on the provided request.
        /// </summary>
        /// <param name="createRowRequest">The request containing information to create the row.</param>
        public async Task<int> CreateRow(CreateRowRequest createRowRequest)
        {

            try
            {
                HttpResponseMessage data = await _httpClient.PostAsJsonAsync("api/row/createRow", createRowRequest);
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// Deletes a page by its row ID asynchronously.
        /// </summary>
        /// <param name="Id">The ID of the row associated with the page to delete.</param>
        public async Task DeletePageByRowId(int Id)
        {
            try
            {
                var data = await _httpClient.GetAsync("api/Row/DeleteRowById/" + Id);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
