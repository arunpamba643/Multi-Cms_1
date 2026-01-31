using AssociationEntities.Models;

namespace AssociationRepository.Association
{
    /// <summary>
    /// Menu Repository
    /// </summary>
    public class CustomMenuRepository : ICustomMenuRepository
    {
        private readonly AssociationContext _associationContext;
        public CustomMenuRepository(AssociationContext associationContext)
        {
            _associationContext = associationContext;
        }

        /// <summary>
        ///  Method to Get All Menus
        /// </summary>
        /// <returns></returns>
        public List<CustomMenu> GetAllMenus(int TenantId)
        {
            var data = _associationContext.CustomMenus.Where(x => x.TenantId == TenantId).ToList();
            return data;
        }

        /// <summary>
        ///  Method to Get Menu by Id
        /// </summary>
        /// <returns></returns>
        public CustomMenu GetMenuById(int Id)
        {
            var data = _associationContext.CustomMenus.Where(x => x.MenuId == Id).FirstOrDefault();
            return data;
        }

        /// <summary>
        /// Method to create Menus
        /// </summary>
        /// <param name="customMenu"></param>
        /// <returns></returns>
        public async Task<int> CreateMenu(CustomMenu customMenu, int PageId)
        {
            try
            {

                _associationContext.CustomMenus.Add(customMenu);
                var data = await _associationContext.SaveChangesAsync();

                if (PageId > 0)
                {
                    var page = _associationContext.Pages.Where(x => x.Id == customMenu.MenuId).FirstOrDefault();
                    page.MenuId = PageId;
                    await _associationContext.SaveChangesAsync();
                }

                return data;
            }
            catch (Exception ex)
            {

            }
            return 0;
        }

        /// <summary>
        /// Method to Update Menu
        /// </summary>
        /// <param name="customMenu"></param>
        /// <returns></returns>
        public async Task<int> UpdateMenu(CustomMenu customMenu, int PageId)
        {
            var menu = _associationContext.CustomMenus.FirstOrDefault(u => u.MenuId == customMenu.MenuId);
            try
            {


                if (menu != null)
                {
                    if (menu.Position != customMenu.Position || menu.ParentMenuId != customMenu.ParentMenuId)
                    {
                        int? currentParentId = 0;
                        int currentPosition = 0;
                        int updatedPosition = customMenu.Position;
                        int? previousParentId = 0;
                        var childs = _associationContext.CustomMenus.Where(x => x.ParentMenuId == customMenu.ParentMenuId).OrderBy(x => x.Position).Skip(updatedPosition - 1).ToList();

                        updatedPosition = updatedPosition + 1;
                        foreach (var item in childs.Where(x => x.MenuId != customMenu.MenuId))
                        {
                            item.Position = updatedPosition++;
                        }

                        currentParentId = menu.ParentMenuId;
                        currentPosition = menu.Position;
                        previousParentId = menu.ParentMenuId;

                        if (currentParentId != customMenu.ParentMenuId)
                        {
                            var oldChilds = _associationContext.CustomMenus.Where(x => x.ParentMenuId == currentParentId).OrderBy(x => x.Position).Skip(currentPosition - 1).ToList();
                            foreach (var item in oldChilds.Where(x => x.MenuId != customMenu.MenuId))
                            {
                                item.Position = currentPosition++;
                            }
                        }
                    }

                    menu.MenuName = customMenu.MenuName;
                    menu.ParentMenuId = customMenu.ParentMenuId;
                    menu.Position = customMenu.Position;
                    menu.PageUrl = customMenu.PageUrl;

                    if (PageId > 0)
                    {
                        var page = _associationContext.Pages.Where(x => x.Id == PageId).FirstOrDefault();
                        page.MenuId = menu.MenuId;
                    }

                    await _associationContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

            }

            return 1;
        }

        /// <summary>
        /// Method to Bulk Update Menu
        /// </summary>
        /// <param name="customMenu"></param>
        /// <returns></returns>
        public async Task<int> BulkUpdateMenu(List<CustomMenu> customMenu)
        {
            var MenuIds = customMenu.Select(x => x.MenuId).ToList();

            var entitiesToUpdate = _associationContext.CustomMenus
                .Where(e => MenuIds.Contains(e.MenuId))
                .ToList();

            foreach (var entity in entitiesToUpdate)
            {
                var menu = customMenu.Where(x => x.MenuId == entity.MenuId).FirstOrDefault();
                if (menu != null)
                {
                    entity.ParentMenuId = menu.ParentMenuId;
                    entity.MenuName = menu.MenuName;
                }
            }
            await _associationContext.SaveChangesAsync();

            return 1;
        }

        /// <summary>
        /// Delete Menu by Id
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteMenuById(int Id)
        {
            try
            {


                List<CustomMenu> menus = new List<CustomMenu>();
                var menu = _associationContext.CustomMenus.FirstOrDefault(u => u.MenuId == Id);

                if (menu != null)
                {
                    menus.Add(menu);
                    var nextElements = _associationContext.CustomMenus.Where(x => x.ParentMenuId == menu.ParentMenuId && x.MenuId != menu.MenuId && x.Position > menu.Position).OrderBy(x => x.Position).ToList();

                    if (nextElements.Count() > 0)
                    {
                        foreach (var item in nextElements)
                        {
                            item.Position = menu.Position++;
                        }
                    }



                }
                //if (menu != null)
                //{
                //    var allMenus = _associationContext.CustomMenus.Where(x=>x.TenantId == menu.TenantId);
                //    var childrenElements = allMenus.Where(x => x.ParentMenuId == menu.MenuId  );

                //    if (childrenElements != null)
                //    {
                //        menus.AddRange(childrenElements);
                //        foreach (var item in childrenElements)
                //        {
                //            var grandChildren = allMenus.Where(x => x.ParentMenuId == item.MenuId);
                //            if (grandChildren != null)
                //            {
                //                menus.AddRange(grandChildren);
                //            }
                //        }
                //    }
                //}

                if (menus.Count > 0)
                {


                    _associationContext.CustomMenus.RemoveRange(menus);
                    _associationContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
