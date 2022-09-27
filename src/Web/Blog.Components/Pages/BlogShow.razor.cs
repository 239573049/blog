using Blazored.LocalStorage;
using Blog.Components.Apis;
using Blog.Components.Module;
using Masa.Blazor.Components.Editor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blog.Components.Pages;
public partial class BlogShow
{
    [Parameter]
    public Guid Id { get; set; }

    /// <summary>
    /// 博客内容
    /// </summary>
    public BlogDto? BlogDto { get; set; }

    [Inject]
    public BlogApi BlogApi { get; set; } = null!;

    [Inject]
    public IJSRuntime Js { get; set; } 


    /// <summary>
    /// 评论内容
    /// </summary>
    private string? Content;


    private async Task GetBlogAsync()
    {
        BlogDto = await BlogApi.GetAsync(Id);

        await Js.InvokeVoidAsync("loadContent", "content", BlogDto?.Content);

        StateHasChanged();

    }

    /// <summary>
    ///  添加浏览量
    /// </summary>
    /// <returns></returns>
    private async Task AddPageViewAsync()
    {
        await BlogApi.AddPageViewAsync(Id);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(firstRender)
        {
            await GetBlogAsync();
            await AddPageViewAsync();
            await base.OnAfterRenderAsync(firstRender);
        }
    }

    /// <summary>
    /// 添加评论
    /// </summary>
    /// <returns></returns>
    public async Task CreateCommentAsync()
    {
        await BlogApi.CreateCommentAsync(new CreateCommentDto()
        {
            Content = Content,
            BlogId = Id
        });
        Content = "";
        await GetBlogAsync();

    }
}
