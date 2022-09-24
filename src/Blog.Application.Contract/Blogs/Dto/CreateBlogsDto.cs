namespace Blog.Application.Contract.Blogs.Dto;

/// <summary>
/// 创建博客文章Dto
/// </summary>
public class CreateBlogsDto 
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
    /// 文章类型Id
    /// </summary>
    public Guid TypeId { get; set; }
}
