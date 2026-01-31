using AssociationBusiness.Handlers;
//using AssociationEntities.CustomModels;
//using AssociationEntities.Models;
using WebsiteDemo.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ScopeType = WebsiteDemo.Models.ScopeType;
using AssociationEntities.Models;
//using AssociationEntities.CustomModels;
//using AssociationEntities.CustomModels;

namespace WebsiteDemo.Services
{
    public class BlogPageService : IBlogPageService
    {
        private readonly HttpClient _httpClient;
        List<BlogPagesModel> blogPages = new List<BlogPagesModel>();
        public BlogPageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }




        public async Task<IEnumerable<BlogPagesModel>> GetAllBlogPages(AssociationEntities.CustomModels.BlogFilters blogFilters)
        {
            var query = "?";

            if (blogFilters.TenantId != null)
                query += $"TenantId={Uri.EscapeDataString(blogFilters.TenantId.ToString())}&";

            //if (blogFilters.fromDate != null)
            //    query += $"from={Uri.EscapeDataString(blogFilters.fromDate.ToString())}&";

            //if (blogFilters.Page != null)
            //    query += $"page={blogFilters.Page}&";

            //if (blogFilters.PageSize != null)
            //    query += $"pagesize={blogFilters.PageSize}&";

            //if (!string.IsNullOrEmpty(blogFilters.OrderBy))
            //    query += $"orderby={Uri.EscapeDataString(blogFilters.OrderBy)}&";

            //if (!string.IsNullOrEmpty(blogFilters.Keyword))
            //    query += $"keyword={Uri.EscapeDataString(blogFilters.Keyword)}&";

            //if (blogFilters.SearchFields != null && blogFilters.SearchFields.Any())
            //    query += $"searchfields={Uri.EscapeDataString(string.Join(",", blogFilters.SearchFields))}&";

            if (blogFilters.DivisionsIds != null && blogFilters.DivisionsIds.Any())
                query += $"DivisionsIds={Uri.EscapeDataString(blogFilters.DivisionsIds.Trim())}&";

            if (blogFilters.DisciplinesIds != null && blogFilters.DisciplinesIds.Any())
                query += $"DisciplinesIds={Uri.EscapeDataString(blogFilters.DisciplinesIds.Trim())}&";

            if (blogFilters.TagIds != null && blogFilters.TagIds.Any())
                query += $"TagIds={Uri.EscapeDataString(blogFilters.TagIds.Trim())}&";

            //if (blogFilters.CreatorIds != null && blogFilters.CreatorIds.Any())
            //    query += $"CreatorIds={Uri.EscapeDataString(string.Join(",", blogFilters.CreatorIds).TrimEnd(','))}";

            // Remove the trailing '&' if any
            query = query.TrimEnd('&');


            try
            {
                var blogs = await _httpClient.GetFromJsonAsync<List<BlogPagesModel>>("api/BlogPage/GetAllBlogPages" + query.ToString());
                return blogs;
            }
            catch (Exception ex)
            {

            }
            return blogPages;
        }

        public async Task<List<BlogList>> GetAllBlogsFromAPI()
        {
            List<BlogList> BlogList = new List<BlogList>();
            try
            {
                var data = await _httpClient.GetAsync("https://api.search.sportcloud.de/api/blogentry?page=1&pagesize=25&orderbytype=descending&orderby=CreatedAt");

                if (data.IsSuccessStatusCode)
                {
                    string response = await data.Content.ReadAsStringAsync();
                    var jsonResponse = (JObject)JsonConvert.DeserializeObject(response);
                    var items = jsonResponse["items"];



                    foreach (var item in items)
                    {
                        BlogList Blog = new BlogList();
                        Blog.BlogId = Convert.ToInt32(item["$id"]);
                        Blog.BlogName = item["title"].ToString();
                        Blog.ScopeType = item["scopeType"].ToString();
                        foreach (var component in item["components"])
                        {
                            if (component["componentType"].ToString() == "image")
                            {
                                Blog.BlogImage = component["imageUrl"].ToString();
                            }
                            else if (component["componentType"].ToString() == "text")
                            {
                                Blog.BlogDescription = component["content"].ToString();
                            }
                        }
                        //string ComponentName = item["title"]["components"].ToString();

                        BlogList.Add(Blog);
                    }
                }

            }
            catch (Exception ex)
            {

            }
            return BlogList;
        }



        public BlogPagesModel GetBlogPageById(int Id)
        {
            BlogPagesModel customBlogPageModel = new BlogPagesModel();
            var menu = _httpClient.GetFromJsonAsync<AssociationEntities.Models.BlogPost>("api/BlogPage/GetBlogPageById/" + Id).Result;
            if (menu != null)
            {
                customBlogPageModel = new BlogPagesModel()
                {
                    BpId = Convert.ToInt32(menu.Bpid),
                    Title = menu.Title,
                    Description = menu.Description,
                    Tags = menu.Tags,
                    TenantId = Convert.ToInt32(menu.TenantId),
                    HeaderImage = menu.HeaderImage
                };
            }
            return customBlogPageModel;
        }


    }
}
