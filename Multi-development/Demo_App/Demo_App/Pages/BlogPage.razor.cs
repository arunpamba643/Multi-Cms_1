using Demo_App.Services.BlogPage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Demo_App.Pages
{

    public partial class BlogPage : ComponentBase
    {
        [Inject]
        public BlogService BlogService { get; set; }

        private BlogService.BlogPost NewBlogPost = new BlogService.BlogPost();

        private void CreateBlogPost()
        {
            BlogService.AddBlogPost(NewBlogPost); 
            NewBlogPost = new BlogService.BlogPost(); // Clear input fields after adding
        }

        private async Task HandleFileUpload(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file != null)
            {
                var buffer = new byte[file.Size];
                await file.OpenReadStream().ReadAsync(buffer);
                NewBlogPost.HeaderImage = $"data:{file.ContentType};base64,{Convert.ToBase64String(buffer)}";
            }
        }
    }
    //public partial class BlogPage : ComponentBase
    //{
    //    private List<BlogPost> BlogPosts = new List<BlogPost>();
    //    private BlogPost NewBlogPost = new BlogPost();

    //    private void CreateBlogPost()
    //    {
    //        // Simulate adding a new blog post (replace with actual logic)
    //        NewBlogPost.Id = BlogPosts.Count + 1;
    //        BlogPosts.Add(NewBlogPost);
    //        // For demonstration purposes, we'll just log the new post to the console
    //        Console.WriteLine($"New Blog Post Created: Title - {NewBlogPost.Title}, Content - {NewBlogPost.Content}");
    //        // You would typically save this data to a database or use a service for CRUD operations
    //        // Clear input fields after adding
    //        NewBlogPost = new BlogPost();
    //    }

    //    public class BlogPost
    //    {
    //        public int Id { get; set; }
    //        public string Title { get; set; }
    //        public string Content { get; set; }
    //    }
    //}
}
