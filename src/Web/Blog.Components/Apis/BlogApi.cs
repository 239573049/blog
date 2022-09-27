using Blazored.LocalStorage;
using Blog.Components.Extensions;
using Blog.Components.Module;
using Blog.Components.Module.Base;
using Masa.Blazor;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Blog.Components.Apis;

public class BlogApi
{
    private readonly HttpClient http;
    private readonly IPopupService _popupService;
    private readonly ILocalStorageService localStorageService;
    private const string Name = "/api/Blog";
    public BlogApi(IHttpClientFactory httpClientFactory, IPopupService popupService, ILocalStorageService localStorageService)
    {
        http = httpClientFactory.CreateClient(string.Empty);


        this._popupService = popupService;
        this.localStorageService = localStorageService;
    }

    /// <summary>
    /// 新增博客
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task CreateAsync(CreateBlogsDto input)
    {
        var token = await localStorageService.GetItemAsStringAsync("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var message = await http.PostAsJsonAsync(Name, input);

        var data = await message.ToJsonAsync();

        if(data?.Code != 200)
        {
            await _popupService.ToastErrorAsync(data?.Message);
        }
    }

    /// <summary>
    /// 更新浏览量
    /// </summary>
    /// <param name="blogId"></param>
    /// <returns></returns>
    public async Task AddPageViewAsync(Guid blogId)
    {

        var token = await localStorageService.GetItemAsStringAsync("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var message = await http.GetAsync(Name + "/page-view/" + blogId.ToString());

        var data = await message.ToJsonAsync();

        if(data?.Code != 200)
        {
            await _popupService.ToastErrorAsync(data?.Message);
        }
    }

    /// <summary>
    /// 更新博客
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task UpdateAsync(UpdateBlogDto input)
    {

        var token = await localStorageService.GetItemAsStringAsync("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var message = await http.PutAsJsonAsync(Name, input);

        var data = await message.ToJsonAsync();

        if(data?.Code != 200)
        {
            await _popupService.ToastErrorAsync(data?.Message);
        }
    }

    /// <summary>
    /// 删除博客
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(Guid id)
    {
        var token = await localStorageService.GetItemAsStringAsync("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var message = await http.DeleteAsync(Name);

        if(message.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new Exception("未登录账号，无权操作");
        }

        var data = await message.ToJsonAsync();
        
        if(data?.Code != 200)
        {
            await _popupService.ToastErrorAsync(data?.Message);
        }
    }

    /// <summary>
    /// 获取博客详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<BlogDto?> GetAsync(Guid id)
    {
        var token = await localStorageService.GetItemAsStringAsync("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var message = await http.GetAsync(Name + "/" + id.ToString());

        if(message.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new Exception("未登录账号，无权操作");
        }

        var data = await message.ToJsonAsync<BlogDto>();

        if(data?.Code != 200)
        {
            throw new Exception(data?.Message);
        }

        return data?.Data;

    }

    /// <summary>
    /// 点赞博客
    /// </summary>
    /// <param name="id">博客Id</param>
    /// <returns></returns>
    public async Task LikeAsync(Guid id)
    {
        var token = await localStorageService.GetItemAsStringAsync("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var message = await http.GetAsync(Name + "/like/" + id);

        if(message.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new Exception("未登录账号，无权操作");
        }

        var data = await message.ToJsonAsync();

        if(data?.Code != 200)
        {
            await _popupService.ToastErrorAsync(data?.Message);
        }

    }

    /// <summary>
    /// 添加评论
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task CreateCommentAsync(CreateCommentDto input)
    {
        var token = await localStorageService.GetItemAsStringAsync("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var message = await http.PostAsJsonAsync(Name + "/comment", input);


        if(message.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new Exception("未登录账号，无权操作");
        }


        var data = await message.ToJsonAsync();

        if(data?.Code != 200)
        {
            await _popupService.ToastErrorAsync(data?.Message);
        }
    }

    /// <summary>
    /// 删除评论
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteCommentAsync(Guid id)
    {
        var token = await localStorageService.GetItemAsStringAsync("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var message = await http.DeleteAsync(Name + "/comment/" + id);

        if(message.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new Exception("未登录账号，无权操作");
        }

        var data = await message.ToJsonAsync();

        if(data?.Code != 200)
        {
            await _popupService.ToastErrorAsync(data?.Message);
        }
    }

    /// <summary>
    /// 获取博客推荐类
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PageResponseDto<PageBlogDto>> GetListAsync(BlogInput input)
    {
        var token = await localStorageService.GetItemAsStringAsync("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var message = await http.GetAsync(Name + $"/list?Keyword={input.Keyword}&TypeId={input.TypeId}&Page={input.Page}&PageSize={input.PageSize}");

        if(message.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new Exception("未登录账号，无权操作");
        }

        var data = await message.ToJsonAsync<PageResponseDto<PageBlogDto>>();

        if(data?.Code != 200)
        {
            throw new Exception(data?.Message);
        }

        return data.Data;

    }
}
