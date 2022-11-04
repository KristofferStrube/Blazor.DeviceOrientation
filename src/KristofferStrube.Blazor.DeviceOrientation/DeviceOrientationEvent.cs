using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.DeviceOrientation;

public class DeviceOrientationEvent
{
    [JsonPropertyName("aplha")]
    public double? Alpha { get; set; }

    [JsonPropertyName("beta")]
    public double? Beta { get; set; }

    [JsonPropertyName("gamma")]
    public double? Gamma { get; set; }

    [JsonPropertyName("absolute")]
    public bool Absolute { get; set; }
}
