using AssociationBusiness.Handlers;
using Demo_App.Models;
using Demo_App.Services;
using Microsoft.AspNetCore.Components;
using System.Text;
using Syncfusion.Blazor.RichTextEditor;
using Syncfusion.Blazor.Inputs;
using Syncfusion.Blazor.Buttons;

namespace Demo_App.Pages
{
    public partial class BlogPages : ComponentBase
    {
        [Inject]
        private IBlogPageService BlogPageService { get; set; }

        [Parameter]
        public string TenantId { get; set; }
        private IEnumerable<BlogPagesModel> blogPages;
        private BlogCreatorModel blogCreatorModel;
        private List<string> BlogTagSuggestions = new List<string>();
        private CreateBlogPageRequest newPost = new CreateBlogPageRequest();
        private CreateBlogPageRequest currentPost = new CreateBlogPageRequest();
        private bool isEditing = false;

        private String base64Image = string.Empty;

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

        private async Task ImageFileUploadHandle(UploadChangeEventArgs args)
        {
            try
            {
                foreach (var file in args.Files)
                {
                    var image = file.File;
                    var buffer = new byte[image.Size];
                    await image.OpenReadStream().ReadAsync(buffer);
                    base64Image = $"data:{image.ContentType};base64,{Convert.ToBase64String(buffer)}";

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void BeforeRemovehandler(BeforeRemoveEventArgs args)
        {
            base64Image = string.Empty;
            newPost.HeaderImage = string.Empty;
            currentPost.HeaderImage = string.Empty;
        }




        protected override async Task OnInitializedAsync()
        {
            blogPages = await BlogPageService.GetAllBlogPages(new AssociationEntities.CustomModels.BlogFilters { TenantId = Convert.ToInt32(TenantId) });
        }

        private async Task CreatePost()
        {
            newPost.TenantId = Convert.ToInt32(TenantId);
            if (createBlogTags != null || createBlogDiscipline != null || createBlogDivisions != null)
            {

                newPost.blogAttribute = new List<AssociationEntities.Models.BlogAttribute>();
                if (createBlogTags.Any())
                {
                    newPost.blogAttribute.AddRange(GeneateBlogAttributes(createBlogTags, "tag"));
                }

                if (createBlogDiscipline.Any())
                {
                    newPost.blogAttribute.AddRange(GeneateBlogAttributes(createBlogDiscipline, "discipline"));
                }

                if (createBlogDivisions.Any())
                {
                    newPost.blogAttribute.AddRange(GeneateBlogAttributes(createBlogDivisions, "division"));
                }
            }
            await BlogPageService.CreateBlogPage(newPost);

            ResetBlogForm();
            blogPages = await BlogPageService.GetAllBlogPages(new AssociationEntities.CustomModels.BlogFilters { TenantId = Convert.ToInt32(TenantId) });
            IsDataVisible = false;
        }

        private List<AssociationEntities.Models.BlogAttribute> GeneateBlogAttributes(List<ChipItem> attributes, string attributeType)
        {
            List<AssociationEntities.Models.BlogAttribute> blogAttributes = new List<AssociationEntities.Models.BlogAttribute>();

            foreach (var item in attributes)
            {
                blogAttributes.Add(new AssociationEntities.Models.BlogAttribute { AttributeTitle = item.Text, AttributeType = attributeType });
            }

            return blogAttributes;
        }

        private void ResetBlogForm()
        {

            createBlogTags = new List<ChipItem>();
            createBlogDivisions = new List<ChipItem>();
            createBlogDiscipline = new List<ChipItem>();

            newPost = new CreateBlogPageRequest();

            currentBlogTags = new List<ChipItem>();
            currentBlogDiscipline = new List<ChipItem>();
            currentBlogDivision = new List<ChipItem>();
            currentPost = new CreateBlogPageRequest();

            base64Image = string.Empty;
        }
        private async Task UpdatePost()
        {

            if (currentBlogDiscipline != null || currentBlogDivision != null || currentBlogTags != null)
            {

                currentPost.blogAttribute = new List<AssociationEntities.Models.BlogAttribute>();
                if (currentBlogTags.Any())
                {
                    currentPost.blogAttribute.AddRange(GeneateBlogAttributes(currentBlogTags, "tag"));
                }

                if (currentBlogDiscipline.Any())
                {
                    currentPost.blogAttribute.AddRange(GeneateBlogAttributes(currentBlogDiscipline, "discipline"));
                }

                if (currentBlogDivision.Any())
                {
                    currentPost.blogAttribute.AddRange(GeneateBlogAttributes(currentBlogDivision, "division"));
                }
            }

            await BlogPageService.UpdateBlogPage(currentPost);
            blogPages = await BlogPageService.GetAllBlogPages(new AssociationEntities.CustomModels.BlogFilters { TenantId = Convert.ToInt32(TenantId) });
            currentPost = new CreateBlogPageRequest(); // Clear the form after submission
            isEditing = false;
            IsDataVisible = false;
            ResetBlogForm();

        }

        private void EditPost(BlogPagesModel post)
        {
            currentPost.Bpid = post.BpId;
            currentPost.Title = post.Title;
            currentPost.Description = post.Description;
            currentPost.ValidFrom = post.ValidFrom;
            currentPost.ValidTo = post.ValidTo;
            currentPost.HeaderImage = post.HeaderImage;
            currentPost.ScopeType = (AssociationBusiness.Handlers.ScopeType?)post.ScopeType;

            currentBlogTags = GeneateBlogAttributes(post.BlogAttributes.Where(x => x.AttributeType == "tag").ToList());
            currentBlogDiscipline = GeneateBlogAttributes(post.BlogAttributes.Where(x => x.AttributeType == "discipline").ToList());
            currentBlogDivision = GeneateBlogAttributes(post.BlogAttributes.Where(x => x.AttributeType == "division").ToList());

            isEditing = true;
            IsDataVisible = false;
        }

        private List<ChipItem> GeneateBlogAttributes(List<Demo_App.Models.BlogAttribute> attributes)
        {
            List<ChipItem> blogAttributes = new List<ChipItem>();

            foreach (var item in attributes)
            {
                blogAttributes.Add(new ChipItem { Value = "Tag" + DateTime.Now, Text = item.AttributeTitle });
            }

            return blogAttributes;
        }
        private void CancelEdit()
        {
            //currentPost = new BlogPost();

            ResetBlogForm();
            isEditing = false;
            IsDataVisible = false;
        }

        private async Task DeletePost(int postId)
        {
            await BlogPageService.DeleteBlogPageById(postId);
            blogPages = await BlogPageService.GetAllBlogPages(new AssociationEntities.CustomModels.BlogFilters { TenantId = Convert.ToInt32(TenantId) });
        }

        /* **** New Blog Tag ***** */

        public List<ChipItem> createBlogTags = new List<ChipItem>();
        public string newTag = string.Empty;

        public void AddTag()
        {
            if (!string.IsNullOrEmpty(newTag))
            {
                createBlogTags.Add(new ChipItem { Value = "Tag" + DateTime.Now, Text = newTag, Enabled = true });
                newTag = ""; // Reset the newTag variable
            }
        }

        public void DeleteCreatedBlogTags(ChipDeletedEventArgs args)
        {
            var index = createBlogTags.FindIndex(x => x.Text == args.Text);
            if (index != -1)
            {
                createBlogTags.RemoveAt(index);
            }
        }

        /*******************/

        /* **** New Blog Tag ***** */

        public List<ChipItem> createBlogDivisions = new List<ChipItem>();
        public string newDivision = string.Empty;

        public void AddDivision()
        {
            if (!string.IsNullOrEmpty(newDivision))
            {
                createBlogDivisions.Add(new ChipItem { Value = "Tag" + DateTime.Now, Text = newDivision, Enabled = true });
                newDivision = ""; // Reset the newTag variable
            }
        }

        public void DeleteCreatedBlogDivision(ChipDeletedEventArgs args)
        {
            var index = createBlogDivisions.FindIndex(x => x.Text == args.Text);
            if (index != -1)
            {
                createBlogDivisions.RemoveAt(index);
            }
        }

        /*******************/

        /* **** New Blog Discipline ***** */

        public List<ChipItem> createBlogDiscipline = new List<ChipItem>();
        public string newDiscipline = string.Empty;

        public void AddDiscipline()
        {
            if (!string.IsNullOrEmpty(newDiscipline))
            {
                if (!createBlogDiscipline.Where(x => x.Text.Equals(newDiscipline)).Any())
                {

                    createBlogDiscipline.Add(new ChipItem { Value = "Tag" + DateTime.Now, Text = newDiscipline, Enabled = true });
                    newDiscipline = ""; // Reset the newTag variable
                }
                else
                {
                    newDiscipline = "";
                }
            }
        }

        public void DeleteCreatedBlogDiscipline(ChipDeletedEventArgs args)
        {
            var index = createBlogDiscipline.FindIndex(x => x.Text == args.Text);
            if (index != -1)
            {
                createBlogDiscipline.RemoveAt(index);
            }
        }

        /*******************/

        /* **** Current Blog Tag ***** */

        public List<ChipItem> currentBlogTags = new List<ChipItem>();
        public string currentTag = string.Empty;

        public void AddCurrentTag()
        {
            if (!string.IsNullOrEmpty(currentTag))
            {
                currentBlogTags.Add(new ChipItem { Value = "Tag" + DateTime.Now, Text = currentTag, Enabled = true });
                currentTag = ""; // Reset the newTag variable
            }
        }

        public void DeleteCurrentBlogTags(ChipDeletedEventArgs args)
        {
            var index = currentBlogTags.FindIndex(x => x.Text == args.Text);
            if (index != -1)
            {
                currentBlogTags.RemoveAt(index);
            }
        }

        /*******************/

        /* **** Current Blog Division ***** */

        public List<ChipItem> currentBlogDivision = new List<ChipItem>();
        public string currentDivision = string.Empty;

        public void AddCurrentDivision()
        {
            if (!string.IsNullOrEmpty(currentDivision))
            {
                currentBlogDivision.Add(new ChipItem { Value = "Tag" + DateTime.Now, Text = currentDivision, Enabled = true });
                currentDivision = ""; // Reset the newTag variable
            }
        }

        public void DeleteCurrentBlogDivision(ChipDeletedEventArgs args)
        {
            var index = currentBlogDivision.FindIndex(x => x.Text == args.Text);
            if (index != -1)
            {
                currentBlogDivision.RemoveAt(index);
            }
        }

        /*******************/

        /* **** Current Blog Tag ***** */

        public List<ChipItem> currentBlogDiscipline = new List<ChipItem>();
        public string currentDiscipline = string.Empty;

        public void AddCurrentDiscipline()
        {
            if (!string.IsNullOrEmpty(currentDiscipline))
            {
                currentBlogDiscipline.Add(new ChipItem { Value = "Tag" + DateTime.Now, Text = currentDiscipline, Enabled = true });
                currentDiscipline = ""; // Reset the newTag variable
            }
        }

        public void DeleteCurrentBlogDiscipline(ChipDeletedEventArgs args)
        {
            var index = currentBlogDiscipline.FindIndex(x => x.Text == args.Text);
            if (index != -1)
            {
                currentBlogDiscipline.RemoveAt(index);
            }
        }

        /*******************/


        public List<ChipItem> GenerateChipItems(string chips)
        {
            var list = new List<ChipItem>();

            if (!string.IsNullOrEmpty(chips))
            {
                var tags = chips.Split(";", StringSplitOptions.RemoveEmptyEntries);
                foreach (var tag in tags)
                {
                    list.Add(new ChipItem { Value = "Tag" + DateTime.Now, Text = tag.Trim() });
                }
            }

            return list;
        }

        public string ConcatenateChipItems(List<ChipItem> chipItems)
        {
            StringBuilder tagsBuilder = new StringBuilder();

            foreach (var chipItem in chipItems)
            {
                tagsBuilder.Append(chipItem.Text);
                tagsBuilder.Append(";");
            }

            return tagsBuilder.ToString().TrimEnd(';');
        }


    }

}