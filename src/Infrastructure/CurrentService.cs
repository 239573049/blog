using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;


public class CurrentService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentService(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 是否授权
    /// </summary>
    /// <returns></returns>
    public bool? IsAuthenticated()
    {
        return _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated;
    }

    /// <summary>
    /// 获取用户Id
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BusinessExceptions">未登录</exception>
    public Guid GetUserId()
    {
        var id = GetClaimValueByType(Constant.Id)?.FirstOrDefault();

        if(string.IsNullOrWhiteSpace(id))
        {
            throw new BusinessExceptions("未登录", 401);
        }

        return Guid.Parse(id);
    }

    private IEnumerable<string>? GetClaimValueByType(string claimType)
    {
        return _httpContextAccessor.HttpContext?.User.Claims?.Where(item => item.Type == claimType)
            .Select(item => item.Value);
    }
}

public static class CurrentExtension
{
    public static void AddCurrent(this IServiceCollection services)
    {
        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient<CurrentService>();
    }
}