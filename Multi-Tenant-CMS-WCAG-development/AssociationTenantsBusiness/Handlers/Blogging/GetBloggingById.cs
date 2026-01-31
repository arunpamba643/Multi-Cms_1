using AssociationEntities.Models.Api;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;

namespace AssociationBusiness.Handlers
{
    /// <summary>
    /// Get Blog By Id
    /// </summary>
    public class GetBloggingById : IRequest<BlogModel>
    {
        public int Id { get; set; }
    }

    /// <summary>
    /// Get Blog By Id Handler
    /// </summary>
    public class GetBloggingByIdHandler : IRequestHandler<GetBloggingById, BlogModel>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        public GetBloggingByIdHandler(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public Task<BlogModel> Handle(GetBloggingById request, CancellationToken cancellationToken)
        {
            try
            {
                var data = _blogRepository.GetBlogById(request.Id);
                var blog = _mapper.Map<BlogModel>(data);
                return Task.FromResult(blog);
            }
            catch
            {
                throw;
            }
        }

    }
}
