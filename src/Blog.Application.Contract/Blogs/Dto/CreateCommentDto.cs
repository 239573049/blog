namespace Blog.Application.Contract.Blogs.Dto;

public class CreateCommentDto
{
    /// <summary>
    /// 评论内容
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// 指定博客Id
    /// </summary>
    public Guid BlogId { get; set; }
}
