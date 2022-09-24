using AutoMapper;
using Blog.Application.Contract.Blogs.Dto;
using Blog.Module;

namespace Blog.Application.AutoMapper;

public class BlogAutoMapperProfile : Profile
{
    public BlogAutoMapperProfile()
    {
        CreateMap<CreateBlogsDto, Module.Blogs>();
        CreateMap<BlogDto, Module.Blogs>().ReverseMap();
        CreateMap<Module.Blogs, PageBlogDto>();
        CreateMap<UpdateBlogDto, Module.Blogs>();
        #region 博客类型

        CreateMap<CreateBlogTypeDto, BlogTypes>();
        CreateMap<BlogTypeDto, BlogTypes>()
        .ReverseMap()// 双向配置
        ;
        #endregion

        #region 博客评论

        CreateMap<CreateCommentDto, BlogComments>();
        CreateMap<BlogComments, BlogCommentsDto>();
        #endregion
    }
}
