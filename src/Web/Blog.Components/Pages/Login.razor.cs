using Blog.Components.Apis;
using Blog.Components.Module;
using Microsoft.AspNetCore.Components;

namespace Blog.Components.Pages;
public partial class Login
{
    private bool _show;

    [Inject]
    public UserApi UserApi { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    private LoginDto LoginDto = new();

    public async Task OnClick()
    {
        await UserApi.LoginAsync(LoginDto);

        NavigationManager.NavigateTo("/");
    }
}
