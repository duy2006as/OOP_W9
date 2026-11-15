/************************************************
 * Sinh viên: Hoàng Đình Duy
 * Mã sinh viên: 202418887
 * Bài thực hành 10: Smart Home Mini
 * File: SmartDoor.cs - Cài đặt thiết bị Cửa thông minh
 ************************************************/

namespace SmartHomeMini;

/// <summary>
/// Đại diện cho thiết bị Cửa thông minh trong hệ thống.
/// Kế thừa cả IDevice (bật/tắt nguồn) và ILockable (khóa/mở khóa cơ học).
/// </summary>
public class SmartDoor : IDevice, ILockable
{
    public string Name { get; }
    public bool IsOn { get; private set; }
    public bool IsLocked { get; private set; }

    public SmartDoor(string name)
    {
        Name = name;
        IsOn = true;        // Mặc định cửa có điện
        IsLocked = false;   // Mặc định ban đầu chưa khóa
    }

    public void TurnOn()
    {
        IsOn = true;
        Console.WriteLine($"{Name}: ON");
    }

    public void TurnOff()
    {
        IsOn = false;
        Console.WriteLine($"{Name}: OFF");
    }

    public void Lock()
    {
        IsLocked = true;
        Console.WriteLine($"{Name}: LOCKED");
    }

    public void Unlock()
    {
        IsLocked = false;
        Console.WriteLine($"{Name}: UNLOCKED");
    }
}
