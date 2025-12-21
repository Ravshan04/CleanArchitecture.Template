using CleanArchitecture.WASM.Auth;
using Microsoft.AspNetCore.Components;

namespace CleanArchitecture.WASM.Pages;

public partial class Login : ComponentBase
{
    [Inject] public AuthService AuthService { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string? Error  { get; set; }

    private async Task LoginAsync()
    {
        Error = null;

        try
        {
            //await AuthService.LoginAsync(Username, Password);
            await AuthService.LoginAsync();
            Navigation.NavigateTo("/", true);
        }
        catch (Exception ex)
        {
            Error = "Incorrect login or password: " + ex.Message;
        }
    }
}