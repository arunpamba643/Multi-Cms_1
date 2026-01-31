using Demo_App.Services.BlogPage;
using Microsoft.AspNetCore.Components;


namespace Demo_App.Pages
{

    public partial class BlogListPage : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public BlogService BlogService { get; set; }

        private List<BlogService.BlogPost> BlogPosts { get; set; }

        protected override void OnInitialized()
        {
            BlogPosts = BlogService.GetBlogPosts();
        }

        private void EditBlogPost(BlogService.BlogPost post)
        {
            NavigationManager.NavigateTo($"/BlogPage/EditBlog/{post.Id}");
        }


        private void DeleteBlogPost(BlogService.BlogPost post)
        {
            BlogService.DeleteBlogPost(post.Id);
            BlogPosts.Remove(post); // Remove from the local list to update UI
        }

        private void UpdateBlogPost(BlogService.BlogPost updatedPost)
        {
            BlogService.UpdateBlogPost(updatedPost);
        }
        // No additional logic needed in this code-behind file as the UI directly accesses BlogService to get blog posts
    }

    
    //public partial class BlogListPage : ComponentBase
    //{
    //    private List<BlogPost> BlogPosts = new List<BlogPost>
    //    {
    //        new BlogPost { Title = "Sample Blog Post 1", Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit." },
    //        new BlogPost { Title = "Sample Blog Post 2", Content = "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
    //        new BlogPost { Title = "Sample Blog Post 3", Content = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat." }
    //    };

    //    public class BlogPost
    //    {
    //        public string Title { get; set; }
    //        public string Content { get; set; }
    //    }
    //}
}
