using AssociationEntities.Models;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AssociationBusiness.Handlers
{

    /// <summary>
    /// Create Custom Page Request
    /// </summary>
    /// 

    public class CreateBlogPageRequest : IRequest<int>
    {
        public int Bpid { get; set; }
        public int TenantId { get; set; }

        public string Title { get; set; } = null!;

        public ScopeType? ScopeType { get; set; }

        public string? Description { get; set; }
        public List<BlogAttribute> blogAttribute { get; set; }
        public string? Tags { get; set; }
        public string? Division { get; set; }
        public string? Discipline { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public string? HeaderImage { get; set; }
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ScopeType
    {
        Intern,
        Public
    }

    /// <summary>
    /// Create Custom Page Handler
    /// </summary>
    public class CreateBlogPageHandler : IRequestHandler<CreateBlogPageRequest, int>
    {
        private readonly IBlogPagesRepository _blogPagesRepository;
        private readonly IMapper _mapper;

        public CreateBlogPageHandler(IBlogPagesRepository blogPagesRepository, IMapper mapper)
        {
            _blogPagesRepository = blogPagesRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateBlogPageRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var blog = _mapper.Map<BlogPost>(request);

                if (blog.Bpid != 0)
                {
                    await _blogPagesRepository.UpdateBlogPage(blog);
                    return 1;
                }
                else
                {

                    var bpId = await _blogPagesRepository.CreateBlogPage(blog);
                    return bpId;
                }
            }
            catch
            {
                throw;
            }
        }


    }
}
