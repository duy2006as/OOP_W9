/************************************************
 * Sinh viên: Hoàng Đình Duy
 * Mã sinh viên: 202418887
 * Bài thực hành 10: Smart Home Mini
 * File: DeviceFactory.cs - Triển khai mẫu Factory Pattern
 ************************************************/

namespace SmartHomeMini;

/// <summary>
/// DeviceFactory đóng vai trò khởi tạo các thiết bị IDevice mà không làm lộ các lớp cụ thể ra ngoài Client.
/// </summary>
public static class DeviceFactory
{
    public static IDevice Create(string type, string name)
    {
        if (type == "light")
        {
            return new SmartLight(name);
        }
        else if (type == "fan")
        {
            return new SmartFan(name);
        }
        else if (type == "door")
        {
            return new SmartDoor(name);
        }

        return null!;
    }
}
