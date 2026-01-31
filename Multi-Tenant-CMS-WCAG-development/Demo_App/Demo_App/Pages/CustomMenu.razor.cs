using AssociationBusiness.Handlers;
using AssociationEntities.Common;
using AssociationEntities.Models;
using AssociationEntities.Models.Api;
using Demo_App.Data;
using Demo_App.Services.Blogging;
using Demo_App.Services.Pages;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Navigations;


namespace Demo_App.Pages
{
    public partial class CustomMenu : ComponentBase
    {
        [Inject]
        public ICustomMenuService customMenuService { get; set; }

        [Inject]
        public IPageService pageService { get; set; }

        [Parameter]
        public string TenantId { get; set; }
        public int tenantId = 0;
        public Models.CustomMenuModel formData = new Models.CustomMenuModel();
        public IEnumerable<Models.CustomMenuModel> Menus = new List<Models.CustomMenuModel>();
        public List<CustomMenuModel> PushMenusForUpdate = new List<CustomMenuModel>();
        public List<MenuDropdownModel> DropdownList = new List<MenuDropdownModel>();
        public List<MenuDragDropModel> dragDropListModel = new List<MenuDragDropModel>();
        private MenuDragDropModel draggedItem;
        SfTreeView<MenuDragDropModel> tree;
        private MenuDragDropModel itemToEdit;
        SfContextMenu<MenuItem> menu;
        public string[] selectedNodes = Array.Empty<string>();
        private MenuDragDropModel newItem = new MenuDragDropModel();
        private bool showAddForm = false;
        string selectedId;
        private int idCounter = 0; // Counter for ID
        private MenuDragDropModel selectedNode;
        public string ErrorMessage = "";
        public bool isSpinnerLoad = false;
        private bool IsPopupVisible = false;
        private bool IsDataVisible = false;
        public string selectedValue = "";
        public int position = 0;
        private string searchTerm = "";
        private bool IsMenuMapped { get; set; } = false;
        private CombinedMenuModel selectedItem { get; set; }
        private string SelectedMenu { get; set; } = "Select Menu";
        private string DisplayStyle { get; set; } = "none";
        private string SearchKeyword { get; set; } = "";
        // Add this property to your model or create a separate property


        public IEnumerable<Models.PageModel> controlPages = new List<Models.PageModel>();

        /// <summary>
        /// On Initialized Method
        /// </summary>
        /// <returns></returns>
        /// 
        //private IEnumerable<PageModel> pages;
        //private List<PageModel> pages = new List<PageModel>();
        //private int selectedPageId;
        protected override async Task OnInitializedAsync()
        {
            tenantId = !string.IsNullOrEmpty(TenantId) ? Convert.ToInt32(TenantId) : 0;
            //await JSRuntime.InvokeVoidAsync("showSpinner");
            await FetchCustomMenu();
            await FetchPages();
            //await JSRuntime.InvokeVoidAsync("hideSpinner");
            //pages = (ienumerable<pagemodel>)await pageservice.GetAllPages(tenantid);
            //pages = await pageService.GetAllPages(TenantId);
            //var pageModelsNew = await PageService.GetAllPages(TenantId);
            //pages = pageModelsNew.ToList();
            CombinedMenus = (from d in DropdownList
                             join c in controlPages on d.MenuId equals c.MenuId
                             select new CombinedMenuModel
                             {
                                 MenuId = d.MenuId,
                                 MenuName = d.MenuName,
                                 ResourcePath = c.ResourcePath,
                                 ConcatenatedData = $"{d.MenuName}/{c.ResourcePath}" // Concatenate MenuName and ResourcePath
                             }).ToList();
        }
        public List<CombinedMenuModel> CombinedMenus { get; set; }
        public class CombinedMenuModel
        {
            public int MenuId { get; set; }
            public string MenuName { get; set; } = "";
            public string ResourcePath { get; set; } = "";
            public string ConcatenatedData { get; set; } = "";// New property for concatenated data


        }

        public async Task FetchPages()
        {
            controlPages = await pageService.GetAllPages(tenantId);

        }

        private string dropdownDisplay = "none";

        //public async Task FetchCustomMenu()
        //{
        //    //Menus = await customMenuService.GetAllMenus(tenantId);

        //    DropdownList.AddRange(Menus.Where(x => x.ParentMenuId == 0).ToList());
        //    DropdownList.AddRange(Menus.Where(x => x.ParentMenuId != 0).ToList());
        //    DropdownList.ForEach(y => y.MenuName = (y.ParentMenuId != 0 ? (Menus.Where(x => x.MenuId == y.ParentMenuId).Select(x => x.MenuName + "/").FirstOrDefault()) : "") + y.MenuName);
        //}


        /// <summary>
        /// Method to Create or Update the Menu
        /// </summary>
        /// <returns></returns>
        private async Task HandleValidSubmit()
        {
            try
            {


                if (formData.MenuId > 0)
                {
                    if (IsMenuMapped)
                    {
                        // Copy data from existing page to update request model
                        CreatePageRequest updateMenuRequest = new CreatePageRequest()
                        {
                            PageTitle = existedPage.PageTitle,
                            IsHomePage = existedPage.IsHomePage,
                            IsLandingPage = existedPage.IsLandingPage,
                            ResourcePath = existedPage.ResourcePath,
                            PageScopeType = "draft",
                            MenuId = existedPage.MenuId,
                            TenantId = tenantId,
                            Id = existedPage.Id,
                            pageUrl = DropdownList.Where(x => x.MenuId == formData.MenuId).Select(x => x.MenuName).FirstOrDefault() + "/" + existedPage.ResourcePath ?? ""
                        };

                        // Create or update the page
                        var updatePageId = await pageService.CreatePage(updateMenuRequest);
                        existedPage = new(); // Reset existedPage
                        IsMenuMapped = false;
                    }

                    updateMenuRequest updatedMenu = new updateMenuRequest
                    {
                        Position = formData.Position,
                        ParentMenuId = formData.ParentMenuId,
                        MenuName = formData.MenuName,
                        MenuId = formData.MenuId,

                        //                        PageUrl = DropdownList.Where(x => x.MenuId == formData.MenuId).Select(x => x.MenuName).FirstOrDefault() + "/" + controlPages.Where(x => x.Id == Convert.ToInt32(formData.PageUrl)).Select(x => x.ResourcePath).FirstOrDefault() ?? "",

                        PageId = formData.PageId,
                        PageUrl = DropdownList.Where(x => x.MenuId == formData.MenuId).Select(x => x.MenuName).FirstOrDefault() + "/" + controlPages.Where(x => x.Id == formData.PageId).Select(x => x.ResourcePath).FirstOrDefault() ?? "",

                    };
                    await customMenuService.UpdateMenu(updatedMenu);
                    await FetchCustomMenu();
                    StateHasChanged();
                    IsDataVisible = false;
                    return;
                }


                ErrorMessage = "";

                // Checking the limit of the parent child categories if exceeds returning the error message
                if (selectedId != "0")
                {
                    var countOfChilds = Menus.Where(x => x.ParentMenuId == Convert.ToInt32(selectedId) && formData.MenuId != x.MenuId).Count();
                    if (countOfChilds >= Configurations.SubItemsLimit)
                    {
                        CloseCreateandUpdatePopup();
                        ErrorMessage = "Child Categories should not be exceeded : " + Configurations.SubItemsLimit;
                        return;
                    }
                }
                else
                {
                    var countOfparents = Menus.Where(x => x.ParentMenuId == 0).Count();
                    if (countOfparents >= Configurations.ParentItemList)
                    {
                        CloseCreateandUpdatePopup();
                        ErrorMessage = "Parent Categories should not be exceeded : " + Configurations.ParentItemList;
                        return;
                    }
                }

                // Copy data from form details to API Request model

                if (selectedItem == null)
                {
                    selectedItem = new CombinedMenuModel { MenuId = 0 };
                }

                formData.Position = Menus.Where(x => x.ParentMenuId == Convert.ToInt32(selectedId)).OrderByDescending(x => x.Position).Select(x => x.Position).FirstOrDefault();

                CreateMenuRequest createMenuRequest = new CreateMenuRequest()
                {
                    Position = formData.Position + 1,
                    ParentMenuId = Convert.ToInt32(selectedId),
                    MenuName = formData.MenuName,
                    MenuId = formData.MenuId,
                    PageId = formData.PageId,
                    PageUrl = DropdownList.Where(x => x.MenuId == formData.MenuId).Select(x => x.MenuName).FirstOrDefault() + "/" + controlPages.Where(x => x.Id == formData.PageId).Select(x => x.ResourcePath).FirstOrDefault() ?? "",
                    TenantId = tenantId
                };
                await customMenuService.CreateMenu(createMenuRequest);
                formData = new();

                // After every insert or update we need get latest records 
                await FetchCustomMenu();
                IsDataVisible = false;
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Get All Menus
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Models.CustomMenuModel>> FetchCustomMenu()
        {
            Menus = await customMenuService.GetAllMenus(tenantId);
            DropdownList = new List<MenuDropdownModel>();
            DropdownList.Add(new MenuDropdownModel() { MenuId = 0, MenuName = "Please Select" });
            DropdownList.AddRange(Menus.Where(x => x.ParentMenuId == 0).Select(x => new MenuDropdownModel()
            {
                MenuId = x.MenuId,
                MenuName = x.MenuName,
                ParentMenuId = x.ParentMenuId
            }).ToList());

            DropdownList.AddRange(Menus.Where(x => x.ParentMenuId != 0).Select(x => new MenuDropdownModel()
            {
                MenuId = x.MenuId,
                MenuName = x.MenuName,
                ParentMenuId = x.ParentMenuId
            }).ToList());



            DropdownList.Where(x => x.ParentMenuId != 0).ToList().ForEach(y => y.MenuName = (y.ParentMenuId != 0 ? (Menus.Where(x => x.MenuId == y.ParentMenuId).Select(x => x.MenuName).FirstOrDefault() + "/" + y.MenuName) : y.MenuName));
            dragDropListModel = new List<MenuDragDropModel>();
            foreach (var item in Menus.Where(x => x.ParentMenuId == 0).OrderBy(x => x.Position))
            {
                dragDropListModel.Add(new MenuDragDropModel()
                {
                    Id = item.MenuId.ToString(),
                    Name = item.MenuName,
                    Link = item.PageUrl,
                    ParentId = item.ParentMenuId.ToString(),
                    Position = item.Position,
                    Children = Menus.Where(x => x.ParentMenuId == item.MenuId).OrderBy(x => x.Position).Select(y => new MenuDragDropModel()
                    {
                        Id = y.MenuId.ToString(),
                        Name = y.MenuName,
                        Link = y.PageUrl,
                        ParentId = y.ParentMenuId.ToString(),
                        Position = y.Position,
                        Children = Menus.Where(c => c.ParentMenuId == y.MenuId).OrderBy(x => x.Position).Select(x => new MenuDragDropModel()
                        {
                            Id = x.MenuId.ToString(),
                            Name = x.MenuName,
                            Link = x.PageUrl,
                            ParentId = x.ParentMenuId.ToString(),
                            Position = x.Position
                        }).ToList()
                    }).ToList()
                });
            }
            isSpinnerLoad = true;
            return Menus;
        }

        /// <summary>
        /// Get All Menus binding menus to another tree view model
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Models.CustomMenuModel>> FetchCustomMenuData()
        {
            Menus = await customMenuService.GetAllMenus(tenantId);
            dragDropListModel = new List<MenuDragDropModel>();
            foreach (var item in Menus.Where(x => x.ParentMenuId == 0).OrderBy(x => x.Position))
            {
                dragDropListModel.Add(new MenuDragDropModel()
                {
                    Id = item.MenuId.ToString(),
                    Name = item.MenuName,
                    Link = item.PageUrl,
                    ParentId = item.ParentMenuId.ToString(),
                    Children = Menus.Where(x => x.ParentMenuId == item.MenuId).OrderBy(x => x.Position).Select(y => new MenuDragDropModel()
                    {
                        Id = y.MenuId.ToString(),
                        Name = y.MenuName,
                        Link = y.PageUrl,
                        ParentId = y.ParentMenuId.ToString(),
                        Children = Menus.Where(c => c.ParentMenuId == y.MenuId).OrderBy(x => x.Position).Select(x => new MenuDragDropModel()
                        {
                            Id = x.MenuId.ToString(),
                            Name = x.MenuName,
                            Link = x.PageUrl,
                            ParentId = x.ParentMenuId.ToString()
                        }).ToList()
                    }).ToList()
                });
            }
            return Menus;
        }

        /// <summary>
        /// To Get the next position of the child based on the parent menu change
        /// </summary>
        /// <param name="e"></param>
        private void HandleMainMenuChange(Microsoft.AspNetCore.Components.ChangeEventArgs e)
        {
            ErrorMessage = "";
            selectedValue = e.Value.ToString();
            formData.ParentMenuId = Convert.ToInt32(e.Value);
            int position = Menus.Where(x => x.ParentMenuId == formData.ParentMenuId).OrderByDescending(x => x.Position).Select(x => x.Position).FirstOrDefault();
            formData.Position = position + 1;
        }

        /// <summary>
        /// Find Menu Item by Id
        /// </summary>
        /// <param name="list"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private MenuDragDropModel FindItemById(List<MenuDragDropModel> list, string id)
        {
            foreach (var item in list)
            {
                if (item.Id == id) return item;
                var foundChild = FindItemById(item.Children, id);
                if (foundChild != null) return foundChild;
            }
            return null;
        }

        private async Task NodeEdited(NodeEditEventArgs args)
        {
            try
            {


                var CurrentNodeData = args.NodeData;
                var MenuId = Convert.ToInt32(CurrentNodeData.Id);
                var CurrentItem = FindItemById(dragDropListModel, CurrentNodeData.Id);
                var NewText = args.NewText.Trim();
                int parentId = CurrentNodeData.ParentID == "" ? 0 : Convert.ToInt32(CurrentNodeData.ParentID);
                updateMenuRequest updatedMenu = new updateMenuRequest { MenuId = MenuId, ParentMenuId = parentId, Position = CurrentItem.Position };
                if (NewText == "")
                {
                    args.Cancel = true;
                    ErrorMessage = "TreeView item text should not be empty";
                    StateHasChanged();
                }
                else if (NewText != args.OldText)
                {
                    updatedMenu.MenuName = NewText;
                    //await pageService.UpdatePageInfo(updatedMenu);
                    await customMenuService.UpdateMenu(updatedMenu);
                    StateHasChanged();
                }
                else
                {
                    ErrorMessage = null;
                }

            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// This is the source node means dragged node <-> dragged menu
        /// </summary>
        /// <param name="args"></param>
        private void OnNodeDragging(DragAndDropEventArgs args)
        {
            var menu = Menus.FirstOrDefault(x => x.MenuId == Convert.ToInt32(args.DraggedNodeData.Id));
            draggedItem = new MenuDragDropModel()
            {
                Id = menu.MenuId.ToString(),
                Name = menu.MenuName,
                ParentId = menu.ParentMenuId.ToString(),
                Link = menu.PageUrl,
                Position = menu.Position

            };
        }

        /// <summary>
        /// Dropped node means target node all drag and drop functionality in the below method
        /// </summary>
        /// <param name="args"></param>
        private async void OnNodeDragStop(DragAndDropEventArgs args)
        {
            ErrorMessage = "";
            var sourceItem = draggedItem;
            var targetItem = args.DroppedNodeData;
            // We are accepting only 3 level dropdown if exceeds quit the process.
            if (args.DropLevel > 3 || sourceItem == null || sourceItem.Id == targetItem.ParentID)
            {
                ErrorMessage = args.DropLevel > 3 ? "Cannot exceed maximum of 3 levels" : "";
                args.Cancel = true;
                return;
            }



            string dropChange = args.DropIndicator;

            Models.CustomMenuModel data = Menus.Where(x => x.MenuId.ToString() == sourceItem.Id).FirstOrDefault();
            if (data != null && targetItem != null)
            {
                data.ParentMenuId = !string.IsNullOrEmpty(targetItem.ParentID) ? Convert.ToInt32(targetItem.ParentID) : 0;

                // Checking the limit of the child categories if exceeds returning the error message

                int sourceId = !string.IsNullOrEmpty(sourceItem.Id) ? Convert.ToInt32(sourceItem.Id) : 0;
                var countOfChilds = Menus.Where(x => x.ParentMenuId == data.ParentMenuId && x.MenuId != sourceId).Count();
                if (countOfChilds >= Configurations.SubItemsLimit)
                {

                    ErrorMessage = "Child Categories should not be exceeded : " + Configurations.SubItemsLimit;
                    args.Cancel = true;
                    await FetchCustomMenuData();
                    //StateHasChanged();
                    await InvokeAsync(StateHasChanged);
                    args.Cancel = true;
                    return;
                }

            }
            else
            {
                data.ParentMenuId = 0;
            }


            int initialdroppedIndex = Convert.ToInt32(args.DropIndex + 1);
            int droppedIndex = Convert.ToInt32(args.DropIndex + 1);


            foreach (var item in Menus.Where(x => x.ParentMenuId == data.ParentMenuId).ToList().Skip(initialdroppedIndex))
            {
                item.Position = droppedIndex++;
            }
            int parentMenuId = 0;
            if (targetItem != null)
            {
                if (!string.IsNullOrEmpty(dropChange))
                {
                    // drop-next means with in the parent
                    if (dropChange == "e-drop-next")
                        parentMenuId = Convert.ToInt32(targetItem.ParentID);
                    else if (dropChange == "e-drop-in")  // different parent
                        parentMenuId = Convert.ToInt32(targetItem.Id);
                }

                CreateMenuRequest createMenuRequest = new CreateMenuRequest()
                {
                    ParentMenuId = parentMenuId,
                    MenuName = draggedItem.Name,
                    Position = initialdroppedIndex,
                    MenuId = Convert.ToInt32(draggedItem.Id)
                };

                SelectedMenu.Equals(createMenuRequest);

                await customMenuService.CreateMenu(createMenuRequest);
                await FetchCustomMenuData();

            }

            StateHasChanged();

            // Reset the dragged item
            draggedItem = null;
        }

        /// <summary>
        /// This method will trigger while edit or delete the menus
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public async Task MenuSelect(MenuEventArgs<MenuItem> args)
        {

            string selectedText;
            selectedText = args.Item.Text;
            int Id = Convert.ToInt32(this.selectedId);
            if (selectedText == "Edit")
            {
                formData = Menus.Where(c => c.MenuId == Id).FirstOrDefault();
                formData.PageId = controlPages.Where(x => x.MenuId == Id).Select(x => x.Id).FirstOrDefault();
                IsDataVisible = true;
                ErrorMessage = "";
            }
            else if (selectedText == "Remove")
            {
                await customMenuService.DeleteMenuById(Id);
            }

            await FetchCustomMenuData();
        }

        /// <summary>
        /// Triggers when TreeView Node is selected
        /// </summary>
        /// <param name="args"></param>
        public void OnSelect(NodeSelectEventArgs args)
        {
            this.selectedId = args.NodeData.Id;
        }

        /// <summary>
        /// Triggers when TreeView node is clicked
        /// </summary>
        /// <param name="args"></param>
        public async Task nodeClicked(NodeClickEventArgs args)
        {
            selectedId = args.NodeData.Id;
            selectedNodes = new string[] { args.NodeData.Id };
        }

        /// <summary>
        /// Method to show "Are you sure" popup
        /// </summary>
        public void ShowPopup()
        {
            IsPopupVisible = true;
        }

        /// <summary>
        /// Method to Close  "Are you sure" popup
        /// </summary>
        private void ClosePopup()
        {
            IsPopupVisible = false;
        }

        /// <summary>
        ///Method to show the popup for Create or Update
        /// </summary>
        public void ShowCreateandUpdatePopup()
        {
            IsDataVisible = true;
            formData.MenuName = "";
            formData.ParentMenuId = 0;
            formData.PageUrl = "";
            ErrorMessage = "";
        }

        /// <summary>
        /// Method to Close the popup for Create or Update
        /// </summary>
        private void CloseCreateandUpdatePopup()
        {
            IsDataVisible = false;

        }
        private Demo_App.Models.PageModel existedPage = new Demo_App.Models.PageModel();

        private async void CheckIfMenuMapped(Microsoft.AspNetCore.Components.ChangeEventArgs e)
        {
            existedPage = await pageService.CheckIfPageMapped(formData.MenuId, Convert.ToInt32(e.Value));

            if (existedPage != null && existedPage.Id > 0)
            {
                IsMenuMapped = true;
            }
            else
            {
                IsMenuMapped = false;
            }

        }
    }
}


