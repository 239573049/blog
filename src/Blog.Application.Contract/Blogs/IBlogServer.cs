using Blog.Application.Contract.Base;
using Blog.Application.Contract.Blogs.Dto;

namespace Blog.Application.Contract.Blogs;

/// <summary>
/// 博客
/// </summary>
public interface IBlogServer
{
    /// <summary>
    /// 创建博客文章
    /// </summary>
    /// <returns></returns>
    Task CreateAsync(CreateBlogsDto input);

    /// <summary>
    /// 删除博客
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// 更新博客
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateAsync(UpdateBlogDto input);

    /// <summary>
    /// 获取博客详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BlogDto> GetAsync(Guid id);

    /// <summary>
    /// 博客点赞 
    /// 存在点赞将取消点赞
    /// 不存在即点赞
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task LikeAsync(Guid id);

    /// <summary>
    /// 添加博客评论
    /// </summary>
    /// <returns></returns>
    Task CreateCommentAsync(CreateCommentDto input);

    /// <summary>
    /// 删除评论
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteCommentAsync(Guid id);

    /// <summary>
    /// 获取博客推荐信息
    /// </summary>
    /// <returns></returns>
    Task<PageResponseDto<PageBlogDto>> GetBlogListAsync(BlogInput input);

    /// <summary>
    /// 添加浏览量
    /// </summary>
    /// <param name="blogId"></param>
    /// <returns></returns>
    Task AddPageViewAsync(Guid blogId);
}
