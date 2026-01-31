using AssociationBusiness.Handlers;
using AssociationEntities;
using AssociationEntities.CustomModels;
using AssociationEntities.Models;
using AssociationEntities.Models.Api;
using AutoMapper;

namespace AssociationBusiness.AutoMapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<CreateBloggingRequest, Blog>()
                .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId))
                .ForMember(dest => dest.BlogId, opt => opt.MapFrom(src => src.BlogId))
                .ForMember(dest => dest.HeaderBackgroundColor, opt => opt.MapFrom(src => src.HeaderBackgroundColor))
                .ForMember(dest => dest.HeaderFontColor, opt => opt.MapFrom(src => src.HeaderFontColor))
                .ForMember(dest => dest.Logo, opt => opt.MapFrom(src => src.Logo))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.BrandName))
                .ForMember(dest => dest.Banner, opt => opt.MapFrom(src => src.Banner))
                .ForMember(dest => dest.Heading, opt => opt.MapFrom(src => src.Heading))
                .ForMember(dest => dest.TextContent, opt => opt.MapFrom(src => src.TextContent))
                .ForMember(dest => dest.HeaderFontStyle, opt => opt.MapFrom(src => src.HeaderFontStyle))
                .ForMember(dest => dest.ContentFontStyle, opt => opt.MapFrom(src => src.ContentFontStyle))
                .ForMember(dest => dest.FooterFontStyle, opt => opt.MapFrom(src => src.FooterFontStyle))
                .ForMember(dest => dest.ContentFontSize, opt => opt.MapFrom(src => src.ContentFontSize))
                .ForMember(dest => dest.ContentFontColor, opt => opt.MapFrom(src => src.ContentFontColor))
                .ForMember(dest => dest.FooterBackgroundColor, opt => opt.MapFrom(src => src.FooterBackgroundColor))
                .ForMember(dest => dest.MainMenuFontSize, opt => opt.MapFrom(src => src.MainMenuFontSize))
                .ForMember(dest => dest.MainMenuColor, opt => opt.MapFrom(src => src.MainMenuColor))
                .ForMember(dest => dest.MainMenuFontStyle, opt => opt.MapFrom(src => src.MainMenuFontStyle))
                .ForMember(dest => dest.SubMenuColor, opt => opt.MapFrom(src => src.SubMenuColor))
                .ForMember(dest => dest.SubMenuFontSize, opt => opt.MapFrom(src => src.SubMenuFontSize))
                .ForMember(dest => dest.SubMenuFontStyle, opt => opt.MapFrom(src => src.SubMenuFontStyle))
                .ForMember(dest => dest.SubChildFontStyle, opt => opt.MapFrom(src => src.SubChildFontStyle))
                .ForMember(dest => dest.SubChildMenuColor, opt => opt.MapFrom(src => src.SubChildMenuColor))
                .ForMember(dest => dest.SubChildMenuFontSize, opt => opt.MapFrom(src => src.SubChildMenuFontSize))
                .ForMember(dest => dest.FooterFontColor, opt => opt.MapFrom(src => src.FooterFontColor))
                .ForMember(dest => dest.Facebook, opt => opt.MapFrom(src => src.Facebook))
                .ForMember(dest => dest.LinkedIn, opt => opt.MapFrom(src => src.LinkedIn))
                .ForMember(dest => dest.Twitter, opt => opt.MapFrom(src => src.Twitter))
                .ForMember(dest => dest.Instagram, opt => opt.MapFrom(src => src.Instagram))
                .ForMember(dest => dest.WhatsApp, opt => opt.MapFrom(src => src.WhatsApp))
                .ForMember(dest => dest.FooterText, opt => opt.MapFrom(src => src.FooterText));

            CreateMap<CreateMenuRequest, CustomMenu>()
               .ForMember(dest => dest.MenuName, opt => opt.MapFrom(src => src.MenuName))
               .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.MenuId))
               .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId))
               .ForMember(dest => dest.ParentMenuId, opt => opt.MapFrom(src => src.ParentMenuId))
               .ForMember(dest => dest.PageUrl, opt => opt.MapFrom(src => src.PageUrl))
               .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position));

            CreateMap<CreatePageRequest, Page>()
               .ForMember(dest => dest.PageTitle, opt => opt.MapFrom(src => src.PageTitle))
               .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.MenuId))
               .ForMember(dest => dest.IsHomePage, opt => opt.MapFrom(src => src.IsHomePage))
               .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId))
               .ForMember(dest => dest.ResourcePath, opt => opt.MapFrom(src => src.ResourcePath))
               .ForMember(dest => dest.PageScopeType, opt => opt.MapFrom(src => src.PageScopeType))
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.IsLandingPage, opt => opt.MapFrom(src => src.IsLandingPage));

            CreateMap<CreateComponentPropertyRequest, ComponentProperty>()
                .ForMember(dest => dest.ComponentId, opt => opt.MapFrom(src => src.ComponentId))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Key, opt => opt.MapFrom(src => src.Key));

            CreateMap<updateMenuRequest, CustomMenu>()
                .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.MenuId))
                .ForMember(dest => dest.ParentMenuId, opt => opt.MapFrom(src => src.ParentMenuId))
                .ForMember(dest => dest.PageUrl, opt => opt.MapFrom(src => src.PageUrl))
                .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position))
                .ForMember(dest => dest.MenuName, opt => opt.MapFrom(src => src.MenuName));

            CreateMap<UpatePageRequest, Page>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PageTitle, opt => opt.MapFrom(src => src.PageTitle))
                .ForMember(dest => dest.ResourcePath, opt => opt.MapFrom(src => src.ResourcePath))
                //.ForMember(dest => dest.PaddingWidth, opt => opt.MapFrom(src => src.PaddingWidth))
                .ForMember(dest => dest.MenuId, opt => opt.MapFrom(src => src.menuId))
                .ForMember(dest => dest.IsHomePage, opt => opt.MapFrom(src => src.IsHomePage))
                .ForMember(dest => dest.IsLandingPage, opt => opt.MapFrom(src => src.IsLandingPage));

            CreateMap<CreateComponentPropertiesRequest, ComponentProperitiesRequest>()
           .ForMember(dest => dest.ComponentProperties, opt => opt.MapFrom(src => src.PropertyList.ToList()));

            CreateMap<CreateComponentsRequest, CreateComponents>()
         .ForMember(dest => dest.Components, opt => opt.MapFrom(src => src.ComponentList));

            CreateMap<UpdateComponentRequest, UpdateComponent>()
      .ForMember(dest => dest.ComponentId, opt => opt.MapFrom(src => src.ComponentId))
      .ForMember(dest => dest.UpdateOn, opt => opt.MapFrom(src => src.UpdateOn))
      .ForMember(dest => dest.ComponentType, opt => opt.MapFrom(src => src.ComponentType));

            CreateMap<CreateComponentRequest, Component>()
                .ForMember(dest => dest.ContainerId, opt => opt.MapFrom(src => src.ContainerId))
                .ForMember(dest => dest.ComponentName, opt => opt.MapFrom(src => src.ComponentName))
                .ForMember(dest => dest.ComponentType, opt => opt.MapFrom(src => src.ComponentType))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.UpdatedOn, opt => opt.MapFrom(src => src.UpdatedOn))
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.OrderNumber));

            CreateMap<CreateContainerRequest, Container>()
                .ForMember(dest => dest.RowId, opt => opt.MapFrom(src => src.RowId))
                .ForMember(dest => dest.ContainerName, opt => opt.MapFrom(src => src.ContainerName))
                .ForMember(dest => dest.NoofContainers, opt => opt.MapFrom(src => src.NoofContainers))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn));

            CreateMap<CreateRowRequest, Row>()
                .ForMember(dest => dest.PageId, opt => opt.MapFrom(src => src.PageId))
                .ForMember(dest => dest.OrderNumber, opt => opt.MapFrom(src => src.OrderNumber))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn));

            CreateMap<CreateBlogImageRequest, BlogImage>()
               .ForMember(dest => dest.ImageId, opt => opt.MapFrom(src => src.ImageId))
               .ForMember(dest => dest.BlogId, opt => opt.MapFrom(src => src.BlogId))
               .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId))
               .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
               .ForMember(dest => dest.Udf, opt => opt.MapFrom(src => src.Udf))
               .ForMember(dest => dest.Position, opt => opt.MapFrom(src => src.Position));

            CreateMap<CreateBlogPageRequest, BlogPost>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.ScopeType, opt => opt.MapFrom(src => src.ScopeType))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
            .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline))
            .ForMember(dest => dest.Division, opt => opt.MapFrom(src => src.Division))
            .ForMember(dest => dest.ValidFrom, opt => opt.MapFrom(src => src.ValidFrom))
            .ForMember(dest => dest.ValidTo, opt => opt.MapFrom(src => src.ValidTo))
            .ForMember(dest => dest.HeaderImage, opt => opt.MapFrom(src => src.HeaderImage))
            .ForMember(dest => dest.BlogAttributes, opt => opt.MapFrom(src => src.blogAttribute));

            CreateMap<BlogPost, BlogPagesModel>();


            CreateMap<GetBlogPagesRequest, BlogFilters>();
            //.ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.TagIds) ? src.TagIds.Split(",", StringSplitOptions.RemoveEmptyEntries) : null))
            //.ForMember(dest => dest.DisciplinesIds, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.DisciplinesIds) ? src.DisciplinesIds.Split(",", StringSplitOptions.RemoveEmptyEntries) : null))
            //.ForMember(dest => dest.DivisionsIds, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.DivisionsIds) ? src.DivisionsIds.Split(",", StringSplitOptions.RemoveEmptyEntries) : null));

            CreateMap<GetEventsRequest, EventFilters>();
            CreateMap<CustomMenuModel, CustomMenu>();
            CreateMap<CustomMenu, CustomMenuModel>();
            CreateMap<Blog, BlogModel>();
            CreateMap<Page, PageModel>();
            CreateMap<UpdateBlogPageRequest, BlogPost>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.ScopeType, opt => opt.MapFrom(src => src.ScopeType))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
            .ForMember(dest => dest.Discipline, opt => opt.MapFrom(src => src.Discipline))
            .ForMember(dest => dest.Division, opt => opt.MapFrom(src => src.Division))
            .ForMember(dest => dest.ValidFrom, opt => opt.MapFrom(src => src.ValidFrom))
            .ForMember(dest => dest.ValidTo, opt => opt.MapFrom(src => src.ValidTo))
            .ForMember(dest => dest.HeaderImage, opt => opt.MapFrom(src => src.HeaderImage))
            .ForMember(dest => dest.BlogAttributes, opt => opt.MapFrom(src => src.blogAttribute));



        }
    }
}
