using System.ComponentModel.DataAnnotations;

namespace Blog.Components.Module;

public class LoginDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Required(ErrorMessage = "用户名不能为空")]
    [MinLength(6, ErrorMessage = "用户名长度不能小于六位")]
    public string Username { get; set; } = null!;

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空")]
    [MinLength(6, ErrorMessage = "密码长度不能小于六位")]
    public string Password { get; set; } = null!;
}
