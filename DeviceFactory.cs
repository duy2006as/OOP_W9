/************************************************
 * Sinh viên: Hoàng Đình Duy
 * Mã sinh viên: 202418887
 * Bài thực hành 10: Smart Home Mini
 * File: DeviceFactory.cs - Triển khai mẫu Factory Pattern
 ************************************************/

namespace SmartHomeMini;

public static class DeviceFactory
{
    public static IDevice Create(string type, string name)
    {
        return type.Trim().ToLowerInvariant() switch
        {
            "light" => new SmartLight(name),
            "fan" => new SmartFan(name),
            "door" => new SmartDoor(name),
            _ => throw new ArgumentException($"Unsupported device type: {type}")
        };
    }
}
