using Blog.Application.Contract.User;
using Blog.Application.Contract.User.Dto;
using Blog.HttpApi.Host.Options;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blog.HttpApi.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserServer _userServer;
    private readonly JwtOptions _jwtOptions;
    public UserController(IUserServer userServer, IOptions<JwtOptions> jwtOptions)
    {
        this._userServer = userServer;
        this._jwtOptions = jwtOptions.Value;
    }

    /// <summary>
    /// 注册账号
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task CreateUserAsync(CreateUserDto input) =>
     await _userServer.CreateUserAsync(input);

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<string> LoginAsync(LoginDto input)
    {
        // 通过账号密码获取用户信息
        var user = await _userServer.LoginAsync(input);

        // 添加Claim
        var claims = new[]
        {
            new Claim(Constant.Id, user.Id.ToString()),
            new Claim(ClaimTypes.Role,user.Role) // 设置用户权限
        };

        // 加密
        var cred = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey!)),
            SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            _jwtOptions.Issuer, // 签发者
            _jwtOptions.Audience, // 接收者
            claims, // payload
            expires: DateTime.Now.AddMinutes(_jwtOptions.ExpireMinutes), // 过期时间
            signingCredentials: cred); // 令牌

        // 签发token
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return token;
    }

    /// <summary>
    /// 更新用户信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    [Authorize]
    public async Task UpdateAsync(UpdateUserDto input)
    {
        await _userServer.UpdateAsync(input);
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize]
    public async Task DeleteAsync(Guid id)
    {
        await _userServer.DeleteAsync(id);
    }

    /// <summary>
    /// 获取当前账号详情
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<UserDto> GetAsync()
    {
        return await _userServer.GetAsync();
    }
}
