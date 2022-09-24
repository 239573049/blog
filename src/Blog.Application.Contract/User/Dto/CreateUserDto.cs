using System.ComponentModel.DataAnnotations;

namespace Blog.Application.Contract.User.Dto;

public class CreateUserDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    [MinLength(6,ErrorMessage ="用户名长度不能小于六位")]
    public string Username { get; set; } = null!;

    /// <summary>
    /// 密码
    /// </summary>
    [MinLength(6, ErrorMessage = "密码长度不能小于六位")]
    public string Password { get; set; } = null!;

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? EMail { get; set; }
}
