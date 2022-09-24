using Blog.Application.Contract.Base;

namespace Blog.Application.Contract.Blogs.Dto;

/// <summary>
/// 博客
/// </summary>
public class BlogDto : EntityDto
{

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// 文章内容
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// 浏览量
    /// </summary>
    public long PageView { get; set; }

    /// <summary>
    /// 文章类型Id
    /// </summary>
    public Guid TypeId { get; set; }

    /// <summary>
    /// 作者Id
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// 点赞数量
    /// </summary>
    public long Like { get; set; }

    /// <summary>
    /// 导航属性文章类型
    /// </summary>
    public BlogTypeDto? Type { get; set; }

    /// <summary>
    /// 导航属性用户
    /// </summary>
    public BlogUserDto? Author { get; set; }


    /// <summary>
    /// 博客评论列表
    /// </summary>
    public  List<BlogCommentsDto> BlogComments { get; set; }
}
