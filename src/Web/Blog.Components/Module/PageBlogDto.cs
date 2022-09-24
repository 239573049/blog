using Blog.Components.Module.Base;

namespace Blog.Components.Module;

public class PageBlogDto : EntityDto
{
    /// <summary>
    /// 博客标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 浏览量
    /// </summary>
    public long PageView { get; set; }

    /// <summary>
    /// 点赞数量
    /// </summary>
    public long Like { get; set; }


    /// <summary>
    /// 文章类型Id
    /// </summary>
    public Guid? TypeId { get; set; }

    /// <summary>
    /// 作者Id
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// 导航属性用户
    /// </summary>
    public BlogUserDto? Author { get; set; }

    /// <summary>
    /// 导航属性文章类型
    /// </summary>
    public BlogTypeDto? Type { get; set; }
}
