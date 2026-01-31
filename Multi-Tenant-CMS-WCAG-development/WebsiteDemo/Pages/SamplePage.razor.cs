using Microsoft.AspNetCore.Components;
using WebsiteDemo.Services;
using AssociationEntities.Models.Api;
using WebsiteDemo.Services.Blogging;
using AssociationEntities.Models;
using AssociationBusiness.Handlers;
using WebsiteDemo.Services.Pages;
using System.Resources;
using System;
using Microsoft.JSInterop;
using Syncfusion.Blazor.Charts.Chart.Internal;
using Syncfusion.Blazor.Gantt;
using System.Collections.Generic;
using WebsiteDemo.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Globalization;
using Syncfusion.Blazor.FileManager;

namespace WebsiteDemo.Pages
{
    public class Crumb
    {
        public string Text { get; set; }
        public string Link { get; set; }
    }
    public partial class SamplePage : ComponentBase
    {
        [Parameter]
        public string TenantId { get; set; }
        [Parameter]
        public string ParentMenu { get; set; }
        [Parameter]
        public string Menuname { get; set; }
        [Parameter]
        public string ResourcePath { get; set; }
        [Parameter]
        public string ChildMenu { get; set; }
        [Parameter]
        public string ChildMenuName { get; set; }
        [Inject]
        public IRowService rowService { get; set; }
        [Inject]
        public IPageService pageService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public IBlogPageService blogPageService { get; set; }
        [Inject]
        public ICustomMenuService customMenuService { get; set; }
        [Inject]
        private IFileManager FileManagerService { get; set; }
        [Inject]
        private IJSService JSService { get; set; }
        private int pageId { get; set; }
        private IEnumerable<CustomMenuModel> MenuData = new List<CustomMenuModel>();
        private List<Crumb> breadcrumbs = new List<Crumb>();
        private int tenantId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            tenantId = Convert.ToInt32(TenantId);
            await GetPageId();
            MenuData = await customMenuService.GetAllMenus(tenantId);

            StateHasChanged();
        }

        private void GenerateBreadCrumbs(int menuId)
        {

            breadcrumbs.Clear();

            var menu = MenuData.Where(x => x.MenuId == menuId).FirstOrDefault();
            if (menu != null)
            {
                breadcrumbs.Add(new Crumb { Text = menu.MenuName, Link = menu.PageUrl });
                var parentMenu = MenuData.Where(x => x.MenuId == menu.ParentMenuId).FirstOrDefault();
                if (parentMenu != null)
                {
                    breadcrumbs.Add(new Crumb { Text = parentMenu.MenuName, Link = parentMenu.PageUrl });
                    var parentMenu2 = MenuData.Where(x => x.MenuId == parentMenu.ParentMenuId).FirstOrDefault();
                    if (parentMenu2 != null)
                    {
                        breadcrumbs.Add(new Crumb { Text = parentMenu2.MenuName, Link = parentMenu2.PageUrl });

                    }
                }
            }
            breadcrumbs.ForEach(x => x.Link = "/Content/" + tenantId + "/" + x.Link);
            breadcrumbs.Reverse();

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await FetchRows();
                //await JSRuntime.InvokeVoidAsync("insertAds");
                //await JSRuntime.InvokeVoidAsync("adjustComponentHeight");
            }

        }

        protected IEnumerable<Row> rows = new List<Row>();
        public async Task FetchRows()
        {
            rows = await rowService.GetAllRowsByPageId(pageId);
            await InvokeAsync(StateHasChanged);
        }


        /// <summary>
        /// Retrieves the value of a specified property from the component's properties. If the property does not exist, returns an empt string.
        /// </summary>
        /// <param name="component">The component from which to retrieve the property value.</param>
        /// <param name="key">The key of the property to retrieve.</param>

        string GetValueOrDefault(Component component, string key) =>
            component.ComponentProperties.FirstOrDefault(x => x.Key == key)?.Value ?? "";
        public async Task GetPageId()
        {
            var data = await pageService.GetAllPages(tenantId);
            if (ResourcePath != null)
            {
                pageId = data.Where(x => x.ResourcePath == ResourcePath && x.pageScopeType == "public").Select(x => x.Id).FirstOrDefault();
            }
            else
            {
                pageId = data.Where(x => x.TenantId == tenantId && x.IsLandingPage == true && x.pageScopeType == "public").Select(x => x.Id).FirstOrDefault();
                if (pageId == null)
                {
                    pageId = data.Where(x => x.TenantId == tenantId && x.pageScopeType == "public").Select(x => x.Id).FirstOrDefault();

                }
            }
            GenerateBreadCrumbs(data.Where(x => x.Id == pageId).Select(x => x.MenuId).FirstOrDefault());
        }

        private string GetGridClass(int numberOfColumns)
        {
            if (numberOfColumns <= 4)
            {
                return numberOfColumns switch
                {
                    1 => "grid-cols-1",
                    2 => "sm:grid-cols-1 md:grid-cols-2",
                    3 => "sm:grid-cols-1 md:grid-cols-3",
                    4 => "sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-2 xl:grid-cols-4",
                    _ => "grid-cols-1"
                };
            }
            else
            {
                return "sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-4";
            }
        }

        private string GetFontSizeClass(int columnCount)
        {
            switch (columnCount)
            {
                case 1:
                    return "text-base";
                case 2:
                    return "text-lg";
                case 3:
                    return "text-xl";
                case 4:
                    return "text-sm"; // Adjust this for smaller font size
                                      // Add more cases as needed
                default:
                    return "text-base";
            }
        }

        private BlogPagesModel GetBlogDetailsById(string BlogId)
        {
            BlogPagesModel data = blogPageService.GetBlogPageById(Convert.ToInt32(BlogId));
            if (data == null)
                return new BlogPagesModel();
            return data;
        }

        public async void DownloadFile(FileSharing selectedItem)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DateFormatString = "dd-MM-yyyy HH:mm:ss",
                Culture = CultureInfo.InvariantCulture
            };

            DownloadFileDetails fileDetails = JsonConvert.DeserializeObject<DownloadFileDetails>(selectedItem.FileDetailsJson, settings);

            FileManagerDirectoryContent downloadData = new FileManagerDirectoryContent
            {
                Action = null,
                CaseSensitive = false,
                Data = null,
                DateCreated = fileDetails.DateCreated,
                DateModified = fileDetails.DateModified,
                FilterId = fileDetails.Id,
                FilterPath = fileDetails.Path,
                HasChild = fileDetails.HasChild,
                Id = fileDetails.Id,
                IsFile = fileDetails.IsFile,
                Name = fileDetails.FileName,
                Names = null,
                NewName = null,
                ParentId = fileDetails.ParentId,
                PreviousName = null,
                RenameFiles = null,
                SearchString = null,
                ShowHiddenItems = false,
                Size = fileDetails.FileSize,
                TargetData = null,
                TargetPath = null,
                Type = fileDetails.Type
            };

            object objReq = new
            {
                Action = "download",
                Path = fileDetails.Path,
                Names = new string[] { fileDetails.Id },
                Data = new FileManagerDirectoryContent[] { downloadData }
            };


            JsonSerializerSettings rspSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.Indented
            };
            string json = JsonConvert.SerializeObject(objReq, rspSettings);

            FileDownloadResponse rsp = await FileManagerService.DownloadFile(json);
            await JSService.DownloadFile(rsp.fileName, rsp.data);
        }
    }
}
