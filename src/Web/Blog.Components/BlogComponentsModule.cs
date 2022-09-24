using Blazored.LocalStorage;
using Blog.Components.Apis;
using Microsoft.Extensions.DependencyInjection;
using Token.EventBus;
using Token.Module;
using Token.Module.Attributes;

namespace Blog.Components;

[DependOn(typeof(TokenEventBusModule))]
public class BlogComponentsModule : TokenModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddBlazoredLocalStorage();

        // 注入Masa组件服务
        services.AddMasaBlazor();

        services.AddHttpClient(string.Empty)
            .ConfigureHttpClient(async (services, x) =>
            {
                x.BaseAddress = new Uri("http://localhost:5169");
            });

        // 注入Api服务
        services.AddScoped<BlogTypeApi>();
        services.AddScoped<BlogApi>();
        services.AddScoped<UserApi>();
    }
}
