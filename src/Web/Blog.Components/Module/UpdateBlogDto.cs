namespace Blog.Components.Module;

public class UpdateBlogDto
{

    public Guid Id { get; set; }

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
