using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DeviceOrientation;

internal static class IJSRuntimeExtensions
{
    internal static async Task<IJSObjectReference> GetHelperAsync(this IJSRuntime jSRuntime)
    {
        return await jSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/KristofferStrube.Blazor.DeviceOrientation/KristofferStrube.Blazor.DeviceOrientation.js");
    }
}
