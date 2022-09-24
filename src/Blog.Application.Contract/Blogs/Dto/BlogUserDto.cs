using Blog.Application.Contract.Base;

namespace Blog.Application.Contract.Blogs.Dto;

public class BlogUserDto : EntityDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? EMail { get; set; }
}
