using Blazored.LocalStorage;
using Blog.Components.Extensions;
using Blog.Components.Module;
using Masa.Blazor;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Blog.Components.Apis;

public class BlogTypeApi
{
    private readonly HttpClient http;
    private readonly IPopupService _popupService;
    private readonly ISyncLocalStorageService localStorageService;
    private const string Name = "/api/BlogType";
    public BlogTypeApi(IHttpClientFactory httpClientFactory, IPopupService popupService, ISyncLocalStorageService localStorageService)
    {
        http = httpClientFactory.CreateClient(string.Empty);

        this._popupService = popupService;
        this.localStorageService = localStorageService;
    }


    /// <summary>
    /// 创建博客类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task CreateAsync(CreateBlogTypeDto input)
    {


        var token = localStorageService.GetItemAsString("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var message = await http.PostAsJsonAsync(Name, input);

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
    /// 删除博客类型
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(Guid id)
    {

        var token = localStorageService.GetItemAsString("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var message = await http.DeleteAsync(Name + "/" + id.ToString());

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
    /// 更新博客类型
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task UpdateAsync(UpdateBlogTypeDto input)
    {


        var token = localStorageService.GetItemAsString("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var message = await http.PutAsJsonAsync(Name, input);

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
    /// 获取博客所有类型
    /// </summary>
    /// <returns></returns>
    public async Task<List<BlogTypeDto>> GetListAsync()
    {

        var token = localStorageService.GetItemAsString("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var message = await http.GetAsync(Name + "/list");

        if(message.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            throw new Exception("未登录账号，无权操作");
        }

        var data = await message.ToJsonAsync<List<BlogTypeDto>>();

        if(data?.Code != 200)
        {
            await _popupService.ToastErrorAsync(data?.Message);

            return new List<BlogTypeDto>();
        }

        return data?.Data;
    }
}
