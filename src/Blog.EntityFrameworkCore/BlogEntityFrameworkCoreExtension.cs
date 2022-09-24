using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.EntityFrameworkCore;
public static class BlogEntityFrameworkCoreExtension
{
    public static void AddEntityFrameworkCore(this IServiceCollection services)
    {
        var configure = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
        services.AddDbContext<BlogDbContext>
        (options => options.UseMySql(configure["ConnectionStrings:Default"], new MySqlServerVersion(new Version(8, 0, 10))));
    }
}
