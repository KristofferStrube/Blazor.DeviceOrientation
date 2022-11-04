using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.DeviceOrientation;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddDeviceOrientationService(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<IDeviceOrientationService>(sp => new DeviceOrientationService(sp.GetRequiredService<IJSRuntime>()));
    }
}
