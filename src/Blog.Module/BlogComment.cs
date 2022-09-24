using Blog.Module.Base;

namespace Blog.Module;

/// <summary>
/// 博客文章评论
/// </summary>
public class BlogComments : Entity
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 博客文章Id
    /// </summary>
    public Guid BlogId { get; set; }

    /// <summary>
    /// 评论内容
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// 导航属性用户
    /// </summary>
    public virtual Users? User { get; set; }

    /// <summary>
    /// 导航属性博客
    /// </summary>
    public virtual Blogs? Blogs { get; set; }
}
