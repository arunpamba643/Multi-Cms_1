using AssociationEntities.Models;
using System;

namespace AssociationRepository.Association
{
    /// <summary>
    /// Page Repository
    /// </summary>
    public class PageRepository : IPageRepository
    {
        private readonly AssociationContext _associationContext;
        public PageRepository(AssociationContext associationContext)
        {
            _associationContext = associationContext;
        }

        /// <summary>
        ///  Method to Get All Pages
        /// </summary>
        /// <returns></returns>
        public List<Page> GetAlPages(int TenantId)
        {
            var data = _associationContext.Pages.Where(x => x.TenantId == TenantId).ToList();

            return data;
        }

        /// <summary>
        ///  Method to Get Page by Id
        /// </summary>
        /// <returns></returns>
        public Page GetPageById(int Id)
        {
            var data = _associationContext.Pages.Where(x => x.Id == Id).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// Method to create Pages
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageUrl"></param>
        /// <returns></returns>
        public async Task<int> CreatePage(Page page, string pageUrl)
        {
            try
            {
                if (page.MenuId > 0)
                {

                    var Menu = _associationContext.CustomMenus.Where(x => x.MenuId == page.MenuId).FirstOrDefault();
                    Menu.PageUrl = pageUrl + "/" + page.ResourcePath;
                }

                _associationContext.Pages.Add(page);
                var data = await _associationContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }
            return page.Id;
        }

        /// <summary>
        /// Method to Update Page
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public async Task<int> UpdatePage(Page updatePage, string pageUrl)
        {
            var page = _associationContext.Pages.FirstOrDefault(u => u.Id == updatePage.Id);


            try
            {

                if (page != null)
                {
                    if ((bool)updatePage.IsLandingPage && updatePage.PageScopeType == "public")
                    {
                        var pages = _associationContext.Pages.Where(u => u.TenantId == page.TenantId && u.IsLandingPage == true && u.Id != updatePage.Id).ToList();
                        pages.ForEach(x => x.IsLandingPage = false);
                        await _associationContext.SaveChangesAsync();
                    }

                    page.PageTitle = updatePage.PageTitle;
                    page.ResourcePath = updatePage.ResourcePath;
                    page.PageScopeType = updatePage.PageScopeType;
                    page.IsLandingPage = updatePage.IsLandingPage;
                    page.IsHomePage = updatePage.IsHomePage;
                    page.MenuId = updatePage.MenuId;

                    if (page.MenuId != 0 && page.PageScopeType == "public")
                    {
                        var menu = _associationContext.CustomMenus.FirstOrDefault(x => x.MenuId == page.MenuId);
                        menu.PageUrl = pageUrl;
                        await _associationContext.SaveChangesAsync();
                    }

                    await _associationContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }


            return updatePage.Id;
        }


        /// <summary>
        /// Delete Page by Id
        /// </summary>
        /// <param name="Id"></param>
        public async Task DeletePageById(int Id)
        {
            var page = _associationContext.Pages.FirstOrDefault(u => u.Id == Id);


            if (page != null)
            {
                _associationContext.Pages.Remove(page);
                await _associationContext.SaveChangesAsync();
            }
        }



        /// <summary>
        /// Method to check if any page mapped to a menu
        /// </summary>
        /// <param name="ResourcePath"></param>
        /// <param name="TenantId"></param>
        /// <param name="pageId"></param>
        public bool CheckIfResourcePathAvailable(string ResourcePath, int TenantId, int pageId)
        {
            var data = _associationContext.Pages.Where(x => x.TenantId == TenantId && x.Id != pageId).Select(x => x.ResourcePath).ToList().Contains(ResourcePath);
            return !data;
        }

        /// <summary>
        /// Method to check if any page mapped to a menu
        /// </summary>
        /// <param name="MenuId"></param>
        Page IPageRepository.CheckIfPageMapped(int MenuId, int PageId)
        {
            Page page = _associationContext.Pages.FirstOrDefault(x => x.MenuId != 0 && x.MenuId == MenuId && x.Id != PageId && x.PageScopeType == "public") ?? new Page();
            return page;
        }
    }
}
