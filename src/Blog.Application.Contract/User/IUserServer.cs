using Blog.Application.Contract.User.Dto;

namespace Blog.Application.Contract.User;

public interface IUserServer
{
    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task CreateUserAsync(CreateUserDto input);

    /// <summary>
    /// 登录账号
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<UserDto> LoginAsync(LoginDto input);

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// 编辑用户
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task UpdateAsync(UpdateUserDto user);

    /// <summary>
    /// 获取当前账号信息
    /// </summary>
    /// <returns></returns>
    Task<UserDto> GetAsync();
}
