using AssociationEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssociationRepository.Association;
using AssociationEntities.CustomModels;
using System.Linq.Expressions;
using System.Dynamic;
using System.Reflection;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Xml.Linq;

namespace AssociationRepository.Association
{

    public class BlogPagesRepository : IBlogPagesRepository
    {
        private readonly AssociationContext _associationContext;


        public BlogPagesRepository(AssociationContext associationContext)
        {
            _associationContext = associationContext;
        }

        /// <summary>
        /// Retrieves a collection of blog posts based on the provided filters.
        /// </summary>
        /// <param name="blogFilters">The filters to apply to the blog posts.</param>

        public async Task<List<BlogPost>> GetAllBlogPages(BlogFilters blogFilters)
        {
            var data = _associationContext.BlogPosts.Where(x => x.TenantId == blogFilters.TenantId && x.ScopeType == "public" && (x.ValidFrom == null || (x.ValidFrom != null && x.ValidFrom >= DateTime.Now)) && (x.ValidTo == null || (x.ValidTo != null && x.ValidTo <= DateTime.Now))).Include(x => x.BlogAttributes).AsQueryable();


            // Apply filters based on non-null parameters
            if (blogFilters.fromDate != null)
            {
                data = data.Where(e => e.ValidFrom >= blogFilters.fromDate || e.PublishedOn >= blogFilters.fromDate);
            }

            if (blogFilters.toDate != null)
            {
                data = data.Where(e => e.ValidFrom <= blogFilters.toDate);
            }

            if (blogFilters.SearchFields != null && blogFilters.SearchFields.Any() && !string.IsNullOrEmpty(blogFilters.Keyword))
            {
                string filterExpression = string.Join(" OR ", blogFilters.SearchFields.Select(field => $"{field}.Contains(@0)"));
                data = data.Where(filterExpression, blogFilters.Keyword);
            }

            if ((blogFilters.TagIds != null && blogFilters.TagIds.Any()) ||
    (blogFilters.DivisionsIds != null && blogFilters.DivisionsIds.Any()) ||
    (blogFilters.DisciplinesIds != null && blogFilters.DisciplinesIds.Any()) ||
    (blogFilters.CreatorIds != null && blogFilters.CreatorIds.Any()))
            {
                data = data.Where(e => e.BlogAttributes
                                        .Any(b => (blogFilters.TagIds != null && blogFilters.TagIds.Any() && blogFilters.TagIds.Contains(b.AttributeTitle)) ||
                                                  (blogFilters.DivisionsIds != null && blogFilters.DivisionsIds.Any() && blogFilters.DivisionsIds.Contains(b.AttributeTitle)) ||
                                                  (blogFilters.DisciplinesIds != null && blogFilters.DisciplinesIds.Any() && blogFilters.DisciplinesIds.Contains(b.AttributeTitle)) ||
                                                  (blogFilters.CreatorIds != null && blogFilters.CreatorIds.Contains(b.AttributeTitle))));
            }

            // Apply ordering and pagination
            if (blogFilters.OrderBy != null)
            {
                data = data.OrderBy(blogFilters.OrderBy);

            }
            if (blogFilters.Page != null && blogFilters.PageSize != null)
            {
                data = data.Skip((blogFilters.Page.Value - 1) * blogFilters.PageSize.Value).Take(blogFilters.PageSize.Value);
            }

            // Execute the query and fetch data
            var blogPosts = await data.ToListAsync();

            return blogPosts;
        }
        /// <summary>
        /// Retrieves a blog post by its unique identifier.
        /// </summary>
        /// <param name="Id">The unique identifier of the blog post.</param>

        public BlogPost GetBlogPageById(int Id)
        {
            //Fetching data based on tenant ID
            var data = _associationContext.BlogPosts.Where(x => x.Bpid == Id).Include(x => x.BlogAttributes).FirstOrDefault();
            return data;
        }
        /// <summary>
        /// Deletes a blog post by its unique identifier.
        /// </summary>
        /// <param name="Id">The unique identifier of the blog post to delete.</param>
        public void DeleteBlogPageById(int Id)
        {
            var blog = _associationContext.BlogPosts.FirstOrDefault(u => u.Bpid == Id);

            if (blog != null)
            {
                _associationContext.BlogPosts.Remove(blog);
                _associationContext.SaveChanges();
            }
        }

        /// <summary>
        /// Creates a new blog page with the provided details.
        /// </summary>
        /// <param name="blogPost">The blog post to create.</param>
        public async Task<int> CreateBlogPage(BlogPost blogPost)
        {
            // Set the publication date of the blog post to the current date and time.
            blogPost.PublishedOn = DateTime.Now;

            // Set the creation date of the blog post to the current date and time.
            blogPost.CreatedAt = DateTime.Now;

            // Generate a unique identifier for the blog post's link using a new GUID and convert it to a string.
            blogPost.Link = Guid.NewGuid().ToString();

            _associationContext.BlogPosts.Add(blogPost);

            var data = await _associationContext.SaveChangesAsync();

            if (blogPost.BlogAttributes != null && blogPost.BlogAttributes.Any())
            {
                foreach (var item in blogPost.BlogAttributes)
                {
                    item.BpId = blogPost.Bpid;
                }

                _associationContext.BlogAttributes.AddRange(blogPost.BlogAttributes);
                await _associationContext.SaveChangesAsync();
            }



            return blogPost.Bpid;
        }


        /// <summary>
        /// Updates an existing blog page with the provided details.
        /// </summary>
        /// <param name="blogPost">The updated details of the blog post.</param>
        public async Task<int> UpdateBlogPage(BlogPost blogPost)
        {
            var blog = _associationContext.BlogPosts.FirstOrDefault(u => u.Bpid == blogPost.Bpid);

            if (blog != null)
            {
                blog.Title = blogPost.Title;
                blog.ScopeType = blogPost.ScopeType;
                blog.Description = blogPost.Description;
                blog.UpdatedAt = DateTime.Now;
                blog.ValidFrom = blogPost.ValidFrom;
                blog.ValidTo = blogPost.ValidTo;
                blog.HeaderImage = blogPost.HeaderImage;

                var blogAttributes = _associationContext.BlogAttributes.Where(x => x.BpId == blogPost.Bpid);
                _associationContext.RemoveRange(blogAttributes);

                if (blogPost.BlogAttributes != null && blogPost.BlogAttributes.Any())
                {
                    foreach (var item in blogPost.BlogAttributes)
                    {
                        item.BpId = blogPost.Bpid;
                    }

                    _associationContext.BlogAttributes.AddRange(blogPost.BlogAttributes);

                }

                await _associationContext.SaveChangesAsync();
            }
            return 1;
        }

        /// <summary>
        /// Retrieves suggestions for blog filter options based on the provided TenantId.
        /// </summary>
        /// <param name="TenantId">The identifier of the tenant.</param>
        public Task<List<BlogFilterSuggestions>> GetBlogFilterSuggestions(int TenantId)
        {
            var query =
               (from BlogPosts in _associationContext.BlogPosts
                join BlogAttributes in _associationContext.BlogAttributes on BlogPosts.Bpid equals BlogAttributes.BpId
                select new BlogFilterSuggestions
                {
                    FilterType = BlogAttributes.AttributeType,
                    FilterValue = BlogAttributes.AttributeTitle

                }).Distinct();

            return query.ToListAsync();
        }
    }
}

