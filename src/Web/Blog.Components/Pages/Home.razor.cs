using Blog.Components.Apis;
using Blog.Components.Module;
using Blog.Components.Module.Base;
using Microsoft.AspNetCore.Components;
using Token.EventBus;

namespace Blog.Components.Pages;

public partial class Home
{
    [Inject]
    public BlogApi BlogApi { get; set; } = null!;

    private BlogInput input = new BlogInput();

    private PageResponseDto<PageBlogDto> PageResponse = new();

    [Inject]
    public IKeyLocalEventBus<BlogTypeDto> KeyLocalEventBus { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private async Task GetBlogListAsync()
    {
        PageResponse = await BlogApi.GetListAsync(input);

        await InvokeAsync(StateHasChanged);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
        {
            await GetBlogListAsync();

            await KeyLocalEventBus.Subscribe(nameof(Home), async x =>
            {
                input.TypeId = x.Id;

                await GetBlogListAsync();
            });
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private void OnClick(PageBlogDto dto)
    {
        NavigationManager.NavigateTo("/blog/" + dto.Id);
    }
}
