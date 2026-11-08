using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace proyectoejerciciosescritorio.Services;

public class KeyBindingInfo
{
    public string CommandId { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Display { get; set; } = string.Empty;
}

public class CommandKeyConfigService
{
    private readonly IJSRuntime _jsRuntime;

    public event Action? OnBindingsChanged;

    public KeyBindingInfo ExerciseKey { get; private set; } = new()
    {
        CommandId = "SHIFT_RIGHT",
        Key = "Shift",
        Code = "ShiftRight",
        Display = "Shift Der"
    };

    public KeyBindingInfo LabelKey { get; private set; } = new()
    {
        CommandId = "LABEL",
        Key = "-",
        Code = "Minus",
        Display = "-"
    };

    public KeyBindingInfo NextDayKey { get; private set; } = new()
    {
        CommandId = "NEXT_DAY",
        Key = "¿",
        Code = "Equal",
        Display = "¿"
    };

    public KeyBindingInfo SelectKey { get; private set; } = new()
    {
        CommandId = "ENTER",
        Key = "Enter",
        Code = "Enter",
        Display = "Enter ↵"
    };

    public CommandKeyConfigService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task InitializeAsync()
    {
        try
        {
            var savedJson = await _jsRuntime.InvokeAsync<string?>("window.loadSavedCommandBindings");
            if (!string.IsNullOrWhiteSpace(savedJson))
            {
                var list = JsonSerializer.Deserialize<List<KeyBindingInfo>>(savedJson);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        ApplyBindingInMemory(item);
                    }
                }
            }
        }
        catch
        {
        }

        await SyncToJsAsync();
    }

    public async Task UpdateBindingAsync(string commandId, string key, string code, string display)
    {
        var updated = new KeyBindingInfo
        {
            CommandId = commandId,
            Key = key,
            Code = code,
            Display = string.IsNullOrWhiteSpace(display) ? key : display
        };

        ApplyBindingInMemory(updated);
        await SyncToJsAsync();
        OnBindingsChanged?.Invoke();
    }

    public async Task ResetToDefaultsAsync()
    {
        ExerciseKey = new KeyBindingInfo
        {
            CommandId = "SHIFT_RIGHT",
            Key = "Shift",
            Code = "ShiftRight",
            Display = "Shift Der"
        };
        LabelKey = new KeyBindingInfo
        {
            CommandId = "LABEL",
            Key = "-",
            Code = "Minus",
            Display = "-"
        };
        NextDayKey = new KeyBindingInfo
        {
            CommandId = "NEXT_DAY",
            Key = "¿",
            Code = "Equal",
            Display = "¿"
        };
        SelectKey = new KeyBindingInfo
        {
            CommandId = "ENTER",
            Key = "Enter",
            Code = "Enter",
            Display = "Enter ↵"
        };

        await SyncToJsAsync();
        OnBindingsChanged?.Invoke();
    }

    private void ApplyBindingInMemory(KeyBindingInfo item)
    {
        switch (item.CommandId)
        {
            case "SHIFT_RIGHT":
                ExerciseKey = item;
                break;
            case "LABEL":
                LabelKey = item;
                break;
            case "NEXT_DAY":
                NextDayKey = item;
                break;
            case "ENTER":
                SelectKey = item;
                break;
        }
    }

    private async Task SyncToJsAsync()
    {
        var all = new List<KeyBindingInfo> { ExerciseKey, LabelKey, NextDayKey, SelectKey };
        string json = JsonSerializer.Serialize(all);
        await _jsRuntime.InvokeVoidAsync("window.updateCommandBindings", json);
    }
}

