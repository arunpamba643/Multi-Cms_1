

namespace Demo_App.Services.BlogPage
{
    public class BlogService
    {
        private List<BlogPost> BlogPosts = new List<BlogPost>();
        private List<string> Divisions = new List<string> { "Foot Ball", "Basket Ball", "Volleyball" };
        private Dictionary<string, List<string>> CategoriesByDivision = new Dictionary<string, List<string>>
        {
            { "Foot Ball", new List<string> { "Goalkeeper", "Striker", "Defender" } },
            { "Basket Ball", new List<string> { "Booster", "Spiker", "Lifter" } },
            { "Volleyball", new List<string> { "Setter", "Blocker", "Libero" } }
        };

        public List<BlogPost> GetBlogPosts()
        {
            return BlogPosts;
        }
        public BlogPost GetBlogPostById(string postId)
        {
            return BlogPosts.FirstOrDefault(post => post.Id == postId);
        }

        public void AddBlogPost(BlogPost blogPost)
        {
            blogPost.Id = Guid.NewGuid().ToString(); // Generate a unique ID for the blog post
            BlogPosts.Add(blogPost);
        }

        public async void UpdateBlogPost(BlogPost updatedPost)
        {
            var existingPost = BlogPosts.FirstOrDefault(p => p.Id == updatedPost.Id);
            if (existingPost != null)
            {
                existingPost.Title = updatedPost.Title;
                existingPost.Content = updatedPost.Content;
                existingPost.Tagging = updatedPost.Tagging;
                existingPost.HeaderImage = updatedPost.HeaderImage;
                existingPost.Division = updatedPost.Division;
                existingPost.Category = updatedPost.Category;
            }



        }

        public void DeleteBlogPost(string postId)
        {
            BlogPosts.RemoveAll(p => p.Id == postId);
        }

        public List<string> GetDistinctDivisions()
        {
            return Divisions;
        }

        public List<string> GetCategoriesByDivision(string division)
        {
            if (CategoriesByDivision.ContainsKey(division))
            {
                return CategoriesByDivision[division];
            }
            return new List<string>();
        }

        public class BlogPost
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public string Tagging { get; set; }
            public string HeaderImage { get; set; }
            public string Division { get; set; }
            public string Category { get; set; }
        }
    }
    //public class BlogService
    //{
    //    private List<BlogPost> BlogPosts = new List<BlogPost>();

    //    public List<BlogPost> GetBlogPosts()
    //    {
    //        return BlogPosts;
    //    }

    //    public void AddBlogPost(BlogPost blogPost)
    //    {
    //        blogPost.Id = Guid.NewGuid().ToString(); // Generate a unique ID for the blog post
    //        BlogPosts.Add(blogPost);
    //    }

    //    public List<string> GetDistinctDivisions()
    //    {
    //        var divisions = new List<string>();
    //        foreach (var post in BlogPosts)
    //        {
    //            if (!divisions.Contains(post.Division))
    //            {
    //                divisions.Add(post.Division);
    //            }
    //        }
    //        return divisions;
    //    }

    //    // Add method to get categories based on division for dropdown
    //    public List<string> GetCategoriesByDivision(string division)
    //    {
    //        var categories = new List<string>();
    //        foreach (var post in BlogPosts)
    //        {
    //            if (post.Division == division && !categories.Contains(post.Category))
    //            {
    //                categories.Add(post.Category);
    //            }
    //        }
    //        return categories;
    //    }

    //    public class BlogPost
    //    {
    //        public string Id { get; set; }
    //        public string Title { get; set; }
    //        public string Content { get; set; }
    //        public string Tagging { get; set; }
    //        public string HeaderImage { get; set; }
    //        public string Division { get; set; }
    //        public string Category { get; set; }
    //    }



    //public class BlogPost
    //{
    //    public string Id { get; set; }
    //    public string Title { get; set; }
    //    public string Content { get; set; }
    //    public string Tags { get; set; }
    //    public string Author { get; set; }
    //}
}

