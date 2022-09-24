using Blog.Module.Base;

namespace Blog.Module;

/// <summary>
/// 用户
/// </summary>
public class Users : Entity
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; } = null!;

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = null!;

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? EMail { get; set; }

    /// <summary>
    /// 角色
    /// </summary>
    public RoleType Role { get; set; }
}
