using Blazored.LocalStorage;
using Blog.Components.Extensions;
using Blog.Components.Module;
using Masa.Blazor;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Blog.Components.Apis;

public class UserApi
{
    private readonly HttpClient http;
    private readonly IPopupService _popupService;
    private readonly ILocalStorageService localStorageService;
    private const string Name = "/api/User";
    public UserApi(IHttpClientFactory httpClientFactory, IPopupService popupService, ILocalStorageService localStorageService)
    {
        http = httpClientFactory.CreateClient(string.Empty);


        this._popupService = popupService;
        this.localStorageService = localStorageService;
    }


    /// <summary>
    /// 注册账号
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task CreateUserAsync(CreateUserDto input)
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
    /// 用户登录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<string?> LoginAsync(LoginDto input)
    {

        var token = await localStorageService.GetItemAsStringAsync("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var message = await http.PostAsJsonAsync(Name + "/login", input);

        var data = await message.ToJsonAsync<string>();

        if(data?.Code != 200)
        {
            await _popupService.ToastErrorAsync(data?.Message);
            return "";
        }

        await localStorageService.SetItemAsStringAsync("token", data.Data);

        return data.Data;
    }

    /// <summary>
    /// 更新用户信息
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task UpdateAsync(UpdateUserDto input)
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
    /// 删除用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(Guid id)
    {
        var token = await localStorageService.GetItemAsStringAsync("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var message = await http.DeleteAsync(Name + "/" + id);

        var data = await message.ToJsonAsync();

        if(data?.Code != 200)
        {
            await _popupService.ToastErrorAsync(data?.Message);
        }

    }

    /// <summary>
    /// 获取当前账号详情
    /// </summary>
    /// <returns></returns>
    public async Task<UserDto> GetAsync()
    {
        var token = await localStorageService.GetItemAsStringAsync("token");
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var message = await http.GetAsync(Name);

        var data = await message.ToJsonAsync<UserDto>();

        if(data?.Code != 200)
        {
            throw new Exception(data.Message);
        }

        return data.Data;
    }
}
