namespace KristofferStrube.Blazor.DeviceOrientation
{
    public interface IDeviceOrientationService
    {
        event EventHandler<DeviceOrientationEvent>? OnDeviceOrientation;

        ValueTask DisposeAsync();
    }
}