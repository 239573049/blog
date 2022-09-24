using Blog.Module.Base;

namespace Blog.Module;

/// <summary>
/// 博客文章类型
/// </summary>
public class BlogTypes : Entity
{
    /// <summary>
    /// 类型名称
    /// </summary>
    public string? Name { get; set; }
}
