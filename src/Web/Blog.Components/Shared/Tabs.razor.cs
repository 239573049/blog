using Blazored.LocalStorage;
using Blog.Components.Apis;
using Blog.Components.Module;
using Blog.Components.Pages;
using Microsoft.AspNetCore.Components;
using Token.EventBus;

namespace Blog.Components.Shared;

public partial class Tabs
{
    /// <summary>
    /// 博客类型
    /// </summary>
    private List<BlogTypeDto> BogTypeDtos = new();

    [Inject]
    public BlogTypeApi BlogTypeApi { get; set; } = null!;

    [Parameter]
    public RenderFragment ChildContent { get; set; }

    [Inject]
    public IKeyLocalEventBus<BlogTypeDto> KeyLocalEventBus { get; set; }

    [Inject]
    private ISyncLocalStorageService LocalStorageService { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    private bool isAuthorization = false;

    private async Task GetTypeAsync()
    {
        BogTypeDtos = await BlogTypeApi.GetListAsync();
        var type = new BlogTypeDto()
        {
            Name = "推荐",
            Id = null
        };

        BogTypeDtos.Insert(0, type);

        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
        {
            var token = LocalStorageService.GetItemAsString("token");

            isAuthorization = !string.IsNullOrEmpty(token);

            await GetTypeAsync();
        }
        await base.OnAfterRenderAsync(firstRender);
    }


    private async void OnClick(BlogTypeDto blog)
    {
        // 发布事件
        await KeyLocalEventBus.PublishAsync(nameof(Home), blog);
    }

    private void LogOut()
    {
        LocalStorageService.RemoveItem("token");

        isAuthorization = false;

        StateHasChanged();
    }

    private void Login()
    {
        NavigationManager.NavigateTo("/login");
    }
}
