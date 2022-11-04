using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DeviceOrientation;

public class DeviceOrientationService : IAsyncDisposable, IDeviceOrientationService
{
    private event EventHandler<DeviceOrientationEvent>? onDeviceOrientation;
    private readonly Lazy<Task<IJSObjectReference>> helperTask;
    private DotNetObjectReference<DeviceOrientationService> objRef;

    public DeviceOrientationService(IJSRuntime jSRuntime)
    {
        helperTask = new(jSRuntime.GetHelperAsync);
        objRef = DotNetObjectReference.Create(this);
    }

    public event EventHandler<DeviceOrientationEvent>? OnDeviceOrientation
    {
        add => Subscribe(value);
        remove => Unsubscribe(value);
    }

    private void Subscribe(EventHandler<DeviceOrientationEvent>? value)
    {
        if (onDeviceOrientation is null)
        {
            Task.Run(async () => await AddListenerForDeviceOrientation());
        }
        onDeviceOrientation += value;
    }

    private void Unsubscribe(EventHandler<DeviceOrientationEvent>? value)
    {
        onDeviceOrientation -= value;
        if (onDeviceOrientation is null)
        {
            RemoveListenerForDeviceOrientation().ConfigureAwait(false);
        }
    }

    public async Task AddListenerForDeviceOrientation()
    {
        var helper = await helperTask.Value;
        await helper.InvokeVoidAsync("addListenerForDeviceOrientation", objRef);
    }

    private async Task RemoveListenerForDeviceOrientation()
    {
        var helper = await helperTask.Value;
        await helper.InvokeVoidAsync("removeListenerForDeviceOrientation", objRef);
    }

    [JSInvokable]
    public void InvokeOnDeviceOrientation(DeviceOrientationEvent deviceOrientationEvent)
    {
        onDeviceOrientation?.Invoke(this, deviceOrientationEvent);
    }

    public async ValueTask DisposeAsync()
    {
        if (helperTask.IsValueCreated)
        {
            IJSObjectReference module = await helperTask.Value;
            await module.DisposeAsync();
        }
        GC.SuppressFinalize(this);
    }
}
