using AssociationEntities.CustomModels;
using AssociationEntities.Models;
using AssociationRepository.Association;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssociationBusiness.Handlers
{
    
    public class GetBlogPageById : IRequest<BlogPagesModel>
    {
        public int Id { get; set; }
    }
    public class GetBlogPageByIdHandler : IRequestHandler<GetBlogPageById, BlogPagesModel>
    {
        private readonly IBlogPagesRepository _blogPagesRepository;
        private readonly IMapper _mapper;
        public GetBlogPageByIdHandler(IBlogPagesRepository blogPagesRepository, IMapper mapper)
        {
            _blogPagesRepository = blogPagesRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Handler for retrieving a blog page by its unique identifier.
        /// </summary>
        public Task<BlogPagesModel> Handle(GetBlogPageById request, CancellationToken cancellationToken)
        {
            var data = _blogPagesRepository.GetBlogPageById(request.Id);
            var blog = _mapper.Map<BlogPagesModel>(data);
            return Task.FromResult(blog);
        }

    }
}
