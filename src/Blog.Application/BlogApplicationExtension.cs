using Blog.Application.Blogs;
using Blog.Application.Contract.Blogs;
using Blog.Application.Contract.User;
using Blog.Application.Users;
using Blog.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Application;

public static class BlogApplicationExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        // 注入AutoMapper的配置指定当前程序集 会扫描基础Profile的类
        services.AddAutoMapper(typeof(BlogApplicationExtension));

        // 注入User服务
        services.AddTransient<IUserServer, UserServer>();

        // 注入Blog服务
        services.AddTransient<IBlogServer, BlogServer>();

        // 注入BlogType服务
        services.AddTransient<IBlogTypeService, BlogTypeService>();

        // 注入封装的EfCore服务
        services.AddEntityFrameworkCore();
    }
}
