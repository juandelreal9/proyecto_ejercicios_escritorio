using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace proyectoejerciciosescritorio.Services;

public class ThemeService
{
    private readonly IJSRuntime _jsRuntime;
    public bool IsDarkMode { get; private set; }

    public event Action? OnThemeChanged;

    public ThemeService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task ToggleThemeAsync()
    {
        IsDarkMode = !IsDarkMode;
        await _jsRuntime.InvokeVoidAsync("window.setTheme", IsDarkMode);
        OnThemeChanged?.Invoke();
    }
}

