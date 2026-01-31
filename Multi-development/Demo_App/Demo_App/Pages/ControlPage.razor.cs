using AssociationBusiness.Handlers;
using AssociationEntities.Models.Api;
using Azure.Core;
using Demo_App.Data;
using Demo_App.Models;
using Demo_App.Services;
using Demo_App.Services.Blogging;
using Demo_App.Services.Events;
using Demo_App.Services.Pages;
using MediatR.NotificationPublishers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Syncfusion.Blazor.RichTextEditor;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.ObjectiveC;
using Syncfusion.Blazor.FileManager;
using Demo_App.Components;
//using AssociationRepository.Association;
using System.Web.Helpers;
using System.Text.Json;
using System.Globalization;
using Newtonsoft.Json.Serialization;

namespace Demo_App.Pages
{
    public partial class ControlPage : ComponentBase
    {
        // Injected services
        [Inject]
        public ICustomMenuService customMenuService { get; set; }

        [Inject]
        public IPageService pageService { get; set; }

        [Inject]

        public IRowService rowService { get; set; }
        [Inject]
        public IContainerService containerService { get; set; }

        [Inject]
        public IComponentService componentService { get; set; }
        [Inject]

        public IComponentPropertyService componentPropertyService { get; set; }
        [Inject]
        public IEventService eventService { get; set; }

        [Inject]
        public IBlogPageService blogPageService { get; set; }

        [Inject]
        private NavigationManager Navigation { get; set; }

        [Inject]
        private IJSService JSService { get; set; }

        [Inject]
        private IFileManager FileManagerService { get; set; }

        // Parameters
        [Parameter] public string TenantId { get; set; }
        [Parameter] public string PageId { get; set; }



        // Private fields
        private int tenantId = 0;
        private int pageId = 0;
        private bool isEdit = false;
        private bool isPageCreated = false;
        private IEnumerable<Models.CustomMenuModel> Menus = new List<Models.CustomMenuModel>();
        private List<Models.CustomMenuModel> DropdownList = new List<Models.CustomMenuModel>();
        private Models.PageModel formData = new Models.PageModel { MenuName = "Please Select" }; // Initialize with default menu name
        private List<string> BlogTagSuggestions = new List<string>();
        private bool isMenuMapped = false;
        private string selectedMenuName { get; set; }
        private PageModel existedPage = new PageModel();
        private bool isContentVisible = false;
        private bool isDialogVisible = false;
        private bool isResourcePathExists = true;
        private String base64Image = "";
        private String HTMLContent = "";
        private IEnumerable<Models.RowModel> rowsA = new List<Models.RowModel>();
        private List<String> EventTypes = new List<String>();
        private string searchEventTerm = "";
        private string searchBlogTerm = "";
        private string searchTerm = "";
        private string tempImg = "";
        private Honoree tempHonoree = new Honoree();
        private List<Role> tempRoles = new List<Role>();
        private List<EnumItem> EnumItemList { get; set; }
        private List<EventModel> Events = new List<EventModel>();
        private List<BlogPagesModel> BlogList = new List<BlogPagesModel>();
        private string[] ImagesList = default(string[]);
        private Component currentComponent { get; set; } //= new Component();
        private IEnumerable<EventModel> filteredEvents => Events.Where(item => item.EventName.Contains(searchEventTerm, StringComparison.OrdinalIgnoreCase));
        private IEnumerable<Contact> filteredItems => Data.Where(item => item.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        private IEnumerable<Honoree> filteredHonoreeItems => HonoreeData.Where(item => item.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
        private IEnumerable<BlogPagesModel> filteredBlogs => BlogList.Where(item => item.Title.Contains(searchBlogTerm, StringComparison.OrdinalIgnoreCase));
        private List<AssociationEntities.CustomModels.BlogFilterSuggestions> blogFilterSuggestions = new List<AssociationEntities.CustomModels.BlogFilterSuggestions>();

        private List<string> BlogTags = new List<string>();
        private List<string> BlogDiscipline = new List<string>();
        private List<string> BlogDivision = new List<string>();

        private List<string> SelectedBlogTags = new List<string>();
        private List<string> SelectedBlogDiscipline = new List<string>();
        private List<string> SelectedBlogDivision = new List<string>();

        //fileshare props
        private string newFolderName;
        private List<Folder> folders = new List<Folder>();
        private Folder selectedFolder;
        private string uploadedFileName;
        private IBrowserFile fileToUpload;
        private Role tempRole = new Role() { };


        public string[] Items = new string[] { "Show in public" };

        public List<ToolBarItemModel> MenuItems = new List<ToolBarItemModel>(){
        new ToolBarItemModel() { Name = "Show in Public", Text="Show in Public", TooltipText="Test Tooltip", PrefixIcon="e-icons e-check" },

    };
        SfFileManager<FileManagerDirectoryContent>? FileManager;

        //public void OnMenuClick(ToolbarClickEventArgs<FileManagerDirectoryContent> args)
        //{
        //    // Here, you can customize your code.
        //    args.Item.Text = "hi";
        //}

        public string selectedFilePath { get; set; }
        public string selectedFileName { get; set; }
        public string SelectedFileDetailsjson { get; set; }

        public void FileSelection(FileSelectionEventArgs<FileManagerDirectoryContent> args)
        {
            var SelectedFiles = args.FileDetails;
            if (!string.IsNullOrEmpty(SelectedFiles.Name))
            {
                selectedFileName = SelectedFiles.Name;

                SelectedFileDetailsjson = "{\"fileName\": \"" + SelectedFiles.Name + "\", " +
                    "\"path\": \"" + SelectedFiles.Path + "\", " +
            "\"fileSize\": \"" + SelectedFiles.Size + "\", " +
            "\"dateModified\": \"" + SelectedFiles.DateModified + "\", " +
            "\"dateCreated\": \"" + SelectedFiles.DateCreated + "\", " +
            "\"hasChild\": \"" + SelectedFiles.HasChild + "\", " +
            "\"isFile\": \"" + SelectedFiles.IsFile + "\", " +
            "\"type\": \"" + SelectedFiles.Type + "\", " +
            "\"id\": \"" + SelectedFiles.Id + "\", " +
            "\"filterPath\": \"" + SelectedFiles.FilterPath + "\", " +
            "\"filterId\": \"" + SelectedFiles.FilterId + "\", " +
            "\"parentId\": \"" + SelectedFiles.ParentId + "\", " +
            "\"caseSensitive\": \"" + SelectedFiles.CaseSensitive + "\", " +
            "\"showHiddenItems\": \"" + SelectedFiles.ShowHiddenItems + "\"}";

            }
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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // Call the JavaScript function only on the first render
                //await JSRuntime.InvokeVoidAsync("window.loadFileSharingTree()");
                //await JSService.RenderFileTree();
            }
        }

        // Class to hold folder information
        public class Folder
        {
            public string Name { get; set; }
            public List<FileItem> Files { get; set; } = new List<FileItem>();
        }

        // Class to hold file information
        public class FileItem
        {
            public string Name { get; set; }
            public string Path { get; set; }
        }

        // Create a new folder
        private void CreateFolder()
        {
            if (!string.IsNullOrWhiteSpace(newFolderName))
            {
                folders.Add(new Folder { Name = newFolderName });
                newFolderName = string.Empty;
            }
        }

        // Select a folder to upload files
        private void SelectFolder(Folder folder)
        {
            selectedFolder = folder;
        }

        // File selection handler
        private async Task UploadFile(InputFileChangeEventArgs e)
        {
            fileToUpload = e.File;
            uploadedFileName = fileToUpload.Name;
        }

        private void RemoveFile(FileItem file)
        {
            if (selectedFolder != null)
            {
                selectedFolder.Files.Remove(file);
            }
        }

        // Delete a folder
        private void DeleteFolder(Folder folder)
        {
            if (folders.Contains(folder))
            {
                folders.Remove(folder);
                if (selectedFolder == folder)
                {
                    selectedFolder = null; // Clear selected folder if it's deleted
                }
            }
        }

        // Save the uploaded file to the selected folder
        private async Task SaveFile()
        {
            if (selectedFolder != null && fileToUpload != null)
            {
                // Use folder name as part of the file path
                var folderName = selectedFolder.Name;
                var path = $"{folderName}/{uploadedFileName}"; // E.g., folder1/filename

                selectedFolder.Files.Add(new FileItem { Name = path, Path = path });

                fileToUpload = null;
            }
        }
        //fileshare props

        //fileshare display props
        private List<FolderModel> folders1;
        private FolderModel selectedFolder1 = null;
        private bool isFolderView = true;

        private void ToggleFolderView()
        {
            isFolderView = !isFolderView;
            if (!isFolderView)
            {
                // Optionally, select a folder for demonstration purposes
                // For example: selectedFolder1 = folders1.First();
            }
        }

        private void OpenFolder(FolderModel folder)
        {
            selectedFolder1 = folder;
        }

        private class FolderModel
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string AvatarImageUrl { get; set; }
            public DateTime DateCreated { get; set; }
            public List<FileModel> Files { get; set; }
        }

        private class FileModel
        {
            public string FileName { get; set; }
        }

        //fileshare display props

        // EnumItem class
        public class EnumItem
        {
            public ComponentType EnumValue { get; set; }
            public string DisplayName { get; set; }
        }

        /// <summary>
        /// Represents a list of toolbar items to Rich Text Editor
        /// </summary>

        private List<ToolbarItemModel> Tools = new List<ToolbarItemModel>()
        {
            new ToolbarItemModel() { Command = ToolbarCommand.Bold },
            new ToolbarItemModel() { Command = ToolbarCommand.Italic },
            new ToolbarItemModel() { Command = ToolbarCommand.Underline },
            new ToolbarItemModel() { Command = ToolbarCommand.Separator },
            new ToolbarItemModel() { Command = ToolbarCommand.Formats },
            new ToolbarItemModel() { Command = ToolbarCommand.Alignments },
            new ToolbarItemModel() { Command = ToolbarCommand.Separator },
            new ToolbarItemModel() { Command = ToolbarCommand.CreateLink },
            new ToolbarItemModel() { Command = ToolbarCommand.Image },
            new ToolbarItemModel() { Command = ToolbarCommand.CreateTable },
            new ToolbarItemModel() { Command = ToolbarCommand.Separator },
            new ToolbarItemModel() { Command = ToolbarCommand.SourceCode },
            new ToolbarItemModel() { Command = ToolbarCommand.Separator },
            new ToolbarItemModel() { Command = ToolbarCommand.Undo },
            new ToolbarItemModel() { Command = ToolbarCommand.Redo }
        };



        /// <summary>
        /// On Initialized Method
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            // Dummy folder data
            folders1 = new List<FolderModel>
        {
            new FolderModel
            {
                Id = Guid.NewGuid(),
                Name = "Folder 1",
                AvatarImageUrl = "https://via.placeholder.com/80",
                DateCreated = DateTime.Now.AddDays(-10),
                Files = new List<FileModel>
                {
                    new FileModel { FileName = "File1.txt" },
                    new FileModel { FileName = "File2.txt" }
                }
            },
            new FolderModel
            {
                Id = Guid.NewGuid(),
                Name = "Folder 2",
                AvatarImageUrl = "https://via.placeholder.com/80",
                DateCreated = DateTime.Now.AddDays(-8),
                Files = new List<FileModel>
                {
                    new FileModel { FileName = "File3.txt" },
                    new FileModel { FileName = "File4.txt" }
                }
            },
            new FolderModel
            {
                Id = Guid.NewGuid(),
                Name = "Folder 3",
                AvatarImageUrl = "https://via.placeholder.com/80",
                DateCreated = DateTime.Now.AddDays(-6),
                Files = new List<FileModel>
                {
                    new FileModel { FileName = "File5.txt" },
                    new FileModel { FileName = "File6.txt" }
                }
            },
            new FolderModel
            {
                Id = Guid.NewGuid(),
                Name = "Folder 4",
                AvatarImageUrl = "https://via.placeholder.com/80",
                DateCreated = DateTime.Now.AddDays(-4),
                Files = new List<FileModel>
                {
                    new FileModel { FileName = "File7.txt" },
                    new FileModel { FileName = "File8.txt" }
                }
            }
        };
            tenantId = !string.IsNullOrEmpty(TenantId) ? Convert.ToInt32(TenantId) : 0;
            pageId = !string.IsNullOrEmpty(PageId) ? Convert.ToInt32(PageId) : 0;
            if (pageId != 0)
            {
                formData = await pageService.GetPageById(pageId);
                isPageCreated = true;
            }

            await FetchCustomMenu();
            Console.WriteLine("Let's Come work");
            EnumItemList = Enum.GetValues(typeof(ComponentType))
                         .Cast<ComponentType>()
                         .Select(e => new EnumItem { EnumValue = e, DisplayName = e.ToString() })
                         .ToList();

            await FetchRows();

            await GetEventList();
            EventTypes.AddRange(Events.Select(x => x.EventType).Distinct());

            await GetBlogsList();
            BlogTagSuggestions.RemoveAll(s => string.IsNullOrEmpty(s));

            FetchBlogSuggestions();
        }

        // Handles the submission of form data
        private async Task HandleValidSubmit()
        {
            // Check if the resource path already exists
            //isResourcePathExists = await pageService.CheckIfResourcePathAvailable(tenantId, formData.ResourcePath, pageId);
            //if (!isResourcePathExists)
            //{
            //    return;
            //}

            if (formData.MenuId > 0)
            {
                // Check if a page already exists for the selected menu
                existedPage = await pageService.CheckIfPageMapped(formData.MenuId, formData.Id);
            }

            // Check if the page scope type is public and If a page exists, set the selected menu name and flag indicating menu mapping
            if (formData.PageScopeType == "public" && existedPage.Id != 0)
            {
                selectedMenuName = DropdownList.FirstOrDefault(x => x.MenuId == formData.MenuId).MenuName;
                isMenuMapped = true;
                return;
            }

            else
            {
                // If the page scope type is not public, create a new page
                CreatePage();
            }
        }

        // Creates a new page
        private async void CreatePage()
        {
            // If the menu is already mapped, update the existing page
            if (isMenuMapped)
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
            }

            // Copy data from form to create request model
            CreatePageRequest createMenuRequest = new CreatePageRequest()
            {
                PageTitle = formData.PageTitle,
                IsHomePage = formData.IsHomePage,
                ResourcePath = formData.ResourcePath,
                PageScopeType = formData.PageScopeType,
                IsLandingPage = formData.IsLandingPage,
                MenuId = formData.MenuId,
                TenantId = tenantId,
                Id = pageId,
                pageUrl = DropdownList.Where(x => x.MenuId == formData.MenuId).Select(x => x.MenuName).FirstOrDefault().ToString() + "/" + formData.ResourcePath ?? ""
            };

            // Create the page
            var CurrentPageId = await pageService.CreatePage(createMenuRequest);
            formData = new(); // Reset formData


            if (!isPageCreated)
            {
                // Redirect to page list
                Navigation.NavigateTo($"/ControlPage/{tenantId}/{CurrentPageId}");
            }
            else
            {
                Navigation.NavigateTo($"/PageList/{tenantId}");

            }
        }
        // Initializes the component when parameters are set
        protected async override Task OnParametersSetAsync()
        {
            tenantId = !string.IsNullOrEmpty(TenantId) ? Convert.ToInt32(TenantId) : 0;
            pageId = !string.IsNullOrEmpty(PageId) ? Convert.ToInt32(PageId) : 0;
            if (pageId != 0)
            {
                formData = await pageService.GetPageById(pageId);
                isPageCreated = true;
            }



            await FetchRows();

        }

        /// <summary>
        /// Fetches custom menus asynchronously and constructs a dropdown list of menus with hierarchical names.
        /// </summary>
        public async Task FetchCustomMenu()
        {
            Menus = await customMenuService.GetAllMenus(tenantId);
            DropdownList = new List<Models.CustomMenuModel>();
            DropdownList.Add(new Models.CustomMenuModel() { MenuId = 0, MenuName = "Please Select" });
            DropdownList.AddRange(Menus.Where(x => x.ParentMenuId == 0).ToList());
            DropdownList.AddRange(Menus.Where(x => x.ParentMenuId != 0).ToList());
            DropdownList.ForEach(y => y.MenuName = (y.ParentMenuId != 0 ? (Menus.Where(x => x.MenuId == y.ParentMenuId).Select(x => x.MenuName + "/").FirstOrDefault()) : "") + y.MenuName);
        }


        /// <summary>
        /// Retrieves the value of a specified property from the component's properties. If the property does not exist, returns an empt string.
        /// </summary>
        /// <param name="component">The component from which to retrieve the property value.</param>
        /// <param name="key">The key of the property to retrieve.</param>

        string GetValueOrDefault(Component component, string key) =>
            component.componentProperties.FirstOrDefault(x => x.Key == key)?.Value ?? "";

        /// <summary>
        /// Returns the CSS class for defining grid columns based on the number of columns.
        /// </summary>
        /// <param name="numberOfColumns">The number of columns in the grid.</param>
        private string GetGridClass(int numberOfColumns)
        {
            if (numberOfColumns <= 4)
            {
                return numberOfColumns switch
                {
                    1 => " grid-cols-1",
                    2 => " grid-cols-2",
                    3 => " grid-cols-3",
                    4 => " grid-cols-4",
                    _ => "grid-cols-1"
                };
            }
            else
            {
                return "sm:grid-cols-1 md:grid-cols-2 lg:grid-cols-4";
            }
        }

        /// <summary>
        /// Returns the CSS class for defining font sizes based on the number of columns.
        /// </summary>
        /// <param name="columnCount">The number of columns.</param>
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
        // Sample data
        private List<Contact> Data { get; set; } = new List<Contact>
        {
            new Contact
            {
                Id = 1,
                Name = "John Doe",
                PhoneNumber = "+1234567890",
                EmailAddress = "john@doe.com",
                AvatarImageUrl = "https://res.cloudinary.com/dzax35hss/image/upload/v1707464247/samples/man-on-a-street.jpg"
            },
            new Contact
            {
                Id = 2,
                Name = "Alice Smith",
                PhoneNumber = "+1987654321",
                EmailAddress = "alice@smith.com",
                AvatarImageUrl = "https://res.cloudinary.com/dzax35hss/image/upload/v1707464247/samples/man-on-a-street.jpg"
            },
            new Contact
            {
                Id = 3,
                Name = "Bob Johnson",
                PhoneNumber = "+1122334455",
                EmailAddress = "bob@example.com",
                AvatarImageUrl = "https://res.cloudinary.com/dzax35hss/image/upload/v1707464247/samples/man-on-a-street.jpg"
            },
                        new Contact
            {
                Id = 4,
                Name = "Emily Brown",
                PhoneNumber = "+15556667777",
                EmailAddress = "emily@example.com",
                AvatarImageUrl = "https://res.cloudinary.com/dzax35hss/image/upload/v1707464247/samples/man-on-a-street.jpg"
            },new Contact
            {
                Id = 5,
                Name = "Michael Davis",
                PhoneNumber = "+18889990000",
                EmailAddress = "michael@example.com",
                AvatarImageUrl = "https://res.cloudinary.com/dzax35hss/image/upload/v1707464247/samples/man-on-a-street.jpg"
            }
        };

        //sample data for honorees
        private List<Honoree> HonoreeData { get; set; } = new List<Honoree>
        {
            new Honoree
            {
                Id = 1,
                Name = "John Doe",
                PhoneNumber = "+1234567890",
                EmailAddress = "john@doe.com",
                AvatarImageUrl = "https://res.cloudinary.com/dzax35hss/image/upload/v1707464247/samples/man-on-a-street.jpg",
                Description = "This is some sample description to display about the honoree in the detailed page",
                Date = DateTime.Now
            },
            new Honoree
            {
                Id = 2,
                Name = "Alice Smith",
                PhoneNumber = "+1987654321",
                EmailAddress = "alice@smith.com",
                AvatarImageUrl = "https://res.cloudinary.com/dzax35hss/image/upload/v1707464247/samples/man-on-a-street.jpg",
                Description = "This is some sample description to display about the honoree in the detailed page",
                Date = DateTime.Now
            },
            new Honoree
            {
                Id = 3,
                Name = "Bob Johnson",
                PhoneNumber = "+1122334455",
                EmailAddress = "bob@example.com",
                AvatarImageUrl = "https://img.freepik.com/premium-photo/young-happy-businessman-with-thumb-up_1277677-357.jpg?size=626&ext=jpg",
                Description = "This is some sample description to display about the honoree in the detailed page",
                Date = DateTime.Now
            },
                        new Honoree
            {
                Id = 4,
                Name = "Emily Brown",
                PhoneNumber = "+15556667777",
                EmailAddress = "emily@example.com",
                AvatarImageUrl = "https://res.cloudinary.com/dzax35hss/image/upload/v1707464247/samples/man-on-a-street.jpg",
                Description = "This is some sample description to display about the honoree in the detailed page",
                Date = DateTime.Now
            },new Honoree
            {
                Id = 5,
                Name = "Michael Davis",
                PhoneNumber = "+18889990000",
                EmailAddress = "michael@example.com",
                AvatarImageUrl = "https://res.cloudinary.com/dzax35hss/image/upload/v1707464247/samples/man-on-a-street.jpg",
                Description = "This is some sample description to display about the honoree in the detailed page",
                Date = DateTime.Now
            }
        };

        private void AppendImageUrl(ComponentPropertyModel component, string imageUrl, List<ImageData> existingImages)
        {
            // If the new URL is not empty, append it to Image.Value
            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                if (string.IsNullOrEmpty(component.Value))
                {
                    var firstImage = new List<ImageData>() { new ImageData() { Id = 1, ImageUrl = imageUrl } };
                    component.Value = JsonConvert.SerializeObject(firstImage);
                }
                else
                {
                    existingImages.Add(new ImageData() { Id = existingImages.Count() + 1, ImageUrl = imageUrl });
                    component.Value = JsonConvert.SerializeObject(existingImages);
                }
                tempImg = string.Empty;
            }
        }

        private void AppendHonoree(ComponentPropertyModel component, Honoree honoree, List<Honoree> existingHonorees)
        {
            if (honoree != null)
            {
                if (string.IsNullOrEmpty(component.Value))
                {
                    honoree.Id = 1;
                    var firstHonoree = new List<Honoree>() { honoree };
                    component.Value = JsonConvert.SerializeObject(firstHonoree);
                }
                else
                {
                    honoree.Id = existingHonorees.Count() + 1;
                    existingHonorees.Add(honoree);
                    component.Value = JsonConvert.SerializeObject(existingHonorees);
                }
                tempHonoree = new Honoree();
            }
        }

        private void AddAnotherRole(ComponentPropertyModel component, List<Role> rolesData)
        {
            if (tempRole == null)
            {
                tempRole = new Role();
            }
        }

        private void SaveRoles(ComponentPropertyModel component, Role rolesData, List<Role> existingRoles)
        {
            if (rolesData != null)
            {
                if (string.IsNullOrEmpty(component.Value))
                {
                    var firstRole = new List<Role>() { rolesData };
                    component.Value = JsonConvert.SerializeObject(firstRole);
                }
                else
                {
                    existingRoles.Add(rolesData);
                    component.Value = JsonConvert.SerializeObject(existingRoles);
                }
                tempRole = new Role();
            }
        }

        private void UpdateImageUrl(ComponentPropertyModel component, string imageUrl, List<ImageData> existingImages, ImageData imageData)
        {
            if (!string.IsNullOrWhiteSpace(imageUrl))
            {
                if (string.IsNullOrEmpty(component.Value))
                {
                    var firstImage = new List<ImageData>() { new ImageData() { Id = 1, ImageUrl = imageUrl } };
                    component.Value = JsonConvert.SerializeObject(firstImage);
                }
                else
                {
                    var item = existingImages.Where(x => x.Id == imageData.Id).FirstOrDefault();
                    if (item != null)
                    {
                        item.ImageUrl = imageUrl;
                    }
                    component.Value = JsonConvert.SerializeObject(existingImages);
                }
            }
        }

        private void DeleteImageUrl(ComponentPropertyModel component, List<ImageData> existingImages, ImageData imageData)
        {
            existingImages.Remove(imageData);
            component.Value = JsonConvert.SerializeObject(existingImages);
        }

        private void OnInputChange(ComponentPropertyModel Image, List<ImageData> existingImages, ImageData imageData, Microsoft.AspNetCore.Components.ChangeEventArgs e)
        {
            imageData.ImageUrl = e.Value.ToString();
            Image.Value = JsonConvert.SerializeObject(existingImages);
            //StateHasChanged();
        }

        //// Define the Contact class
        private class Contact
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public string EmailAddress { get; set; }
            public string AvatarImageUrl { get; set; }
            //public string Description { get; set; } = "";

        }

        private class Honoree
        {
            public int Id { set; get; }
            public string Name { get; set; }
            public string Description { get; set; }
            public DateTime Date { get; set; }
            public List<Role> Roles { get; set; } = new List<Role>();
            public string AvatarImageUrl { get; set; }
            public string PhoneNumber { set; get; }
            public string EmailAddress { set; get; }

        }

        public class FileSharing
        {
            public string FileName { get; set; }
            public string FileDetailsJson { get; set; }

        }
        private class Role
        {
            public string RoleName { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }


        /// <summary>
        /// Updates the details of a page based on the form data.
        /// </summary>
        private async void updatePageDetails()
        {
            UpatePageRequest page = new UpatePageRequest();
            page.Id = pageId;
            page.PageTitle = formData.PageTitle;
            page.ResourcePath = formData.ResourcePath;
            page.PaddingWidth = 3;
            page.IsLandingPage = formData.IsLandingPage;
            page.IsHomePage = formData.IsHomePage;
            page.menuId = formData.MenuId;
            await pageService.UpdatePageInfo(page);
        }

        /// <summary>
        /// Handles the selection of a contact item from a search result.
        /// </summary>
        private void selectItem(Contact contact)
        {
            searchTerm = "";
            currentComponent.RefValue = contact.Id;

        }

        private void selectItemHonoree(Honoree honoree)
        {
            searchTerm = "";
            currentComponent.RefValue = honoree.Id;
        }

        /// <summary>
        /// Fetches a list of events asynchronously.
        /// </summary>
        private async Task GetEventList()
        {
            Events = await eventService.GetAllEvents();

        }

        /// <summary>
        /// Fetches a list of blogs asynchronously.
        /// </summary>
        private async Task GetBlogsList()
        {
            BlogList = await blogPageService.GetAllBlogPages(new AssociationEntities.CustomModels.BlogFilters { TenantId = tenantId });
        }

        /// <summary>
        /// Handles the selection of an event item from a search result.
        /// </summary>
        private void selectItem(EventModel Event, Component component)
        {
            searchEventTerm = "";
            currentComponent.RefValue = Event.EventId;
            currentComponent.ComponentName = Event.EventName;
        }

        /// <summary>
        /// Handles the selection of a blog item from a search result.
        /// </summary>
        private void selectItem(BlogPagesModel blog, Component component)
        {
            searchBlogTerm = "";
            currentComponent.RefValue = blog.BpId;
            currentComponent.ComponentName = blog.Title;
        }

        /// <summary>
        /// Handles the selection of a component type and sets up the component accordingly.
        /// </summary>
        private async Task selectComponentType(Component component, EnumItem item)
        {
            component.ShowColumnModal = false;
            currentComponent = new Component(component);
            currentComponent.ComponentType = item.DisplayName;

            if (component.IsUpdating && currentComponent.ComponentType != component.ComponentType)
            {
                currentComponent.componentProperties = new List<ComponentPropertyModel>();
                component.IsUpdating = false;
            }



            if (currentComponent.ComponentType == "BlankSpace")
            {
                currentComponent.componentProperties.Add(new ComponentPropertyModel { ComponentId = currentComponent.ComponentId, Key = "BlankSpace", Value = "BlankSpace" });
                ModifyComponent(currentComponent);
            }
            else
            {
                component.ShowPopup = true;
            }
            if (component.ComponentType == "FileShare")
            {
                await JSService.RenderFileTree();
            }

        }

        [JSInvokable("OnNodeClick")]
        public static void OnNodeClick(string nodeId)
        {
            Console.WriteLine($"Node clicked: {nodeId}");
            // Perform any logic you need here
        }

        /// <summary>
        /// Handles the upload of an image file and converts it to base64 format.
        /// </summary>
        private async Task ImageFileUploadHandle(InputFileChangeEventArgs e)
        {
            var image = e.File;
            if (image.ContentType.StartsWith("image/"))
            {
                try
                {

                    var buffer = new byte[image.Size];
                    await image.OpenReadStream().ReadAsync(buffer);
                    base64Image = $"data:{image.ContentType};base64,{Convert.ToBase64String(buffer)}";
                }
                catch (Exception ex)
                {

                }
            }
        }

        /// <summary>
        /// Fetches a list of rows associated with the page asynchronously.
        /// </summary>
        public async Task FetchRows()
        {
            rowsA = await rowService.GetAllRowsByPageId(pageId);
            await InvokeAsync(StateHasChanged);
        }


        public void FetchBlogSuggestions()
        {
            blogFilterSuggestions = blogPageService.GetBlogFilterSuggestions(tenantId);
            if (blogFilterSuggestions != null && blogFilterSuggestions.Any())
            {

                foreach (var item in blogFilterSuggestions)
                {
                    if (item.FilterType == "tag")
                    {
                        BlogTags.Add(item.FilterValue);
                    }
                    else if (item.FilterType == "division")
                    {
                        BlogDivision.Add(item.FilterValue);
                    }
                    else if (item.FilterType == "discipline")
                    {
                        BlogDiscipline.Add(item.FilterValue);
                    }

                }
            }
        }

        /// <summary>
        /// Adds a new row to the page asynchronously.
        /// </summary>
        private async void AddNewRowToPage()
        {

            try
            {
                CreateRowRequest createRowRequest = new CreateRowRequest();
                createRowRequest.PageId = pageId;
                createRowRequest.CreatedOn = DateTime.Now;
                var result = await rowService.CreateRow(createRowRequest);
                await FetchRows();

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Deletes a row from the page asynchronously.
        /// </summary>
        private async Task DeleteRowInPage(int RowId)
        {
            try
            {
                //await rowService.DeletePageByRowId(RowId);
                await rowService.DeletePageByRowId(RowId);
                await FetchRows();

            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Adds a container to the page with the specified layout.
        /// </summary>
        private async void AddContainer(int RowId, int Layout)
        {
            CreateContainerRequest createContainerRequest = new CreateContainerRequest();
            createContainerRequest.CreatedOn = DateTime.Now;
            createContainerRequest.NoofContainers = Layout;
            createContainerRequest.RowId = RowId;
            try
            {
                var containerId = await containerService.CreateContainer(createContainerRequest);
                CreateComponentsRequest createComponentsRequest = new CreateComponentsRequest();
                List<CreateComponentRequest> ComponentList = new List<CreateComponentRequest>();
                for (int i = 0; i < Layout; i++)
                {
                    CreateComponentRequest component = new CreateComponentRequest { OrderNumber = (i + 1), ContainerId = containerId, CreatedOn = DateTime.Now };
                    ComponentList.Add(component);
                }
                createComponentsRequest.ComponentList = ComponentList;
                await componentService.CreateComponents(createComponentsRequest);
                await FetchRows();

            }
            catch (Exception ex)
            {

            }
        }


        /// <summary>
        /// Modifies a component by updating its properties and saving changes asynchronously.
        /// </summary>
        private async void ModifyComponent(Component component)
        {

            if (component == null)
            {
                return;
            }
            UpdateComponentRequest updateComponentRequest = new UpdateComponentRequest
            {
                ComponentType = component.ComponentType,
                UpdateOn = DateTime.Now,
                ComponentId = component.ComponentId
            };
            CreateComponentPropertiesRequest create = new CreateComponentPropertiesRequest();
            create.PropertyList = new List<CreateComponentPropertyRequest>();

            try
            {


                if (component.componentProperties != null && component.componentProperties.Any())
                {
                    await componentPropertyService.DeleteComponentPropertiesByComponentId(component.ComponentId);
                    foreach (var componentProperty in component.componentProperties)
                    {

                        if (componentProperty != null)
                        {
                            create.PropertyList.Add(new CreateComponentPropertyRequest
                            {
                                ComponentId = componentProperty.ComponentId,
                                Key = componentProperty.Key,
                                Value = componentProperty.Value
                            });
                        }
                    }
                }

                await componentService.UpdateComponent(updateComponentRequest);

                if (create.PropertyList != null && create.PropertyList.Any())
                {

                    var additionalProperties = GetComponentProperties(component);
                    if (additionalProperties != null)
                    {
                        create.PropertyList.AddRange(additionalProperties);
                    }
                }
                await componentPropertyService.CreateComponentProperties(create);
                await FetchRows();
                currentComponent = new Component();
                SelectedBlogTags = new();
                SelectedBlogDiscipline = new();
                SelectedBlogDivision = new();
            }
            catch (Exception ex)
            {

            }

        }


        /// <summary>
        /// Retrieves the properties of a given component.
        /// </summary>
        /// <param name="component">The component for which properties are to be retrieved.</param>
        public List<CreateComponentPropertyRequest> GetComponentProperties(Component component)
        {
            // List to hold component properties
            List<CreateComponentPropertyRequest> componentPropertyModel = new List<CreateComponentPropertyRequest>();

            // Mapping of ComponentType to respective data sources
            Dictionary<string, object> dataSourceMap = new Dictionary<string, object>
            {
                {"ContactPerson", Data},
                {"EventDetail", Events},
                {"BlogDetail", BlogList},
                {"Honoree", HonoreeData},
                {"ImageGallery", ImagesList}
            };

            // Check if the component type exists in the data source map
            if (dataSourceMap.TryGetValue(component.ComponentType, out object dataSource))
            {
                // Object to hold component properties
                object componentProperties = null;

                // Retrieve component properties based on component type
                switch (component.ComponentType)
                {
                    case "ContactPerson":
                        componentProperties = ((IEnumerable<Contact>)dataSource).FirstOrDefault(x => x.Id == component.RefValue);
                        break;
                    case "EventDetail":
                        componentProperties = ((IEnumerable<EventModel>)dataSource).FirstOrDefault(x => x.EventId == component.RefValue);
                        break;
                    //case "BlogDetail":
                    //    componentProperties = ((IEnumerable<BlogPagesModel>)dataSource).FirstOrDefault(x => x.BlogId == component.RefValue);
                    //    break;
                    case "FreeText":
                        componentProperties = new FreeTextModel(component.Description);
                        break;
                    case "BlankSpace":
                        componentProperties = new BlankSpaceModel();
                        break;
                    case "Honoree":
                        componentProperties = ((IEnumerable<Honoree>)dataSource).FirstOrDefault(x => x.Id == component.RefValue);
                        break;
                }

                // If component properties are retrieved
                if (componentProperties != null)
                {
                    // Iterate through each property of the component
                    foreach (PropertyInfo obj in componentProperties.GetType().GetProperties())
                    {
                        // Retrieve property value
                        object value = obj.GetValue(componentProperties);

                        // If property value is not null, add to component property model
                        if (value != null)
                        {
                            string key = obj.Name;
                            CreateComponentPropertyRequest c = new CreateComponentPropertyRequest { ComponentId = component.ComponentId, Key = key, Value = value.ToString() };
                            componentPropertyModel.Add(c);
                        }
                    }
                }
            }

            // Return the list of component properties
            return componentPropertyModel;
        }

        /// <summary>
        /// Deletes a component and its associated properties by ID.
        /// </summary>
        /// <param name="Id">The ID of the component to delete.</param>
        private async Task DeleteComponent(int Id)
        {
            try
            {
                // Delete component properties by ID
                await componentPropertyService.DeleteComponentPropertiesByComponentId(Id);

                // Fetch rows to update UI or refresh data
                await FetchRows();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error deleting component: {ex.Message}");
                // Optionally rethrow the exception if you want the calling code to handle it
                // throw;
            }
        }


        private void ComponentSettings(Component component, Container container)
        {
            currentComponent = component;
            component.ShowPopup = true;
            component.ContainerId = container.ContainerId;
            component.IsUpdating = true;
        }


        private BlogPagesModel GetBlogDetailsById(string BlogId)
        {
            BlogPagesModel data = blogPageService.GetBlogPageById(Convert.ToInt32(BlogId));
            if (data == null)
                return new BlogPagesModel();
            return data;
        }


    }


}