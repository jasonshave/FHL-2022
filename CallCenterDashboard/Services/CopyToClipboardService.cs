using CallingDashboard.Interfaces;
using Microsoft.JSInterop;

namespace CallingDashboard.Services;

public class ClipboardService : IClipboardService
{
    private readonly IJSRuntime _jsInterop;

    public ClipboardService(IJSRuntime jsInterop)
    {
        _jsInterop = jsInterop;
    }

    public async Task CopyToClipboard(string text)
    {
        await _jsInterop.InvokeVoidAsync("navigator.clipboard.writeText", text);
    }
}