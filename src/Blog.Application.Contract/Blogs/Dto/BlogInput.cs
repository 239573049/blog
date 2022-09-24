using Blog.Application.Contract.Base;

namespace Blog.Application.Contract.Blogs.Dto;

/// <summary>
/// 博客分页
/// </summary>
public class BlogInput : PageInput
{
    /// <summary>
    /// 搜索关键词
    /// </summary>
    public string? Keyword { get; set; }

    /// <summary>
    /// 博客类型Id
    /// </summary>
    public Guid? TypeId { get; set; }
}
