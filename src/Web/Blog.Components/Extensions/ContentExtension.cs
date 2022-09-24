using Blog.Components.Module.Base;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Blog.Components.Extensions;
public static class ContentExtension
{
    public static async Task<HttpResult<T>?> ToJsonAsync<T>(this HttpResponseMessage message)
    {
        return JsonConvert.DeserializeObject<HttpResult<T>>(await message.Content.ReadAsStringAsync());
    }


    public static async Task<HttpResult?> ToJsonAsync(this HttpResponseMessage message)
    {
        return JsonConvert.DeserializeObject<HttpResult>(await message.Content.ReadAsStringAsync());
    }
}
