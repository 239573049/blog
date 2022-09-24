using Blog.Module.Base;

namespace Blog.Module;

public class BlogLikes : Entity
{
    /// <summary>
    /// 用户Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 博客文章id
    /// </summary>
    public Guid BlogId { get; set; }

    /// <summary>
    /// 导航属性用户
    /// </summary>
    public virtual Users? User { get; set; }

    /// <summary>
    /// 导航属性博客文章
    /// </summary>
    public virtual Blogs? Blog { get; set; }
}
