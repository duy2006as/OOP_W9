/************************************************
 * Sinh viên: Hoàng Đình Duy
 * Mã sinh viên: 202418887
 * Bài thực hành 10: Smart Home Mini
 * File: SmartLight.cs - Cài đặt thiết bị Đèn thông minh
 ************************************************/

namespace SmartHomeMini;

/// <summary>
/// Đại diện cho thiết bị Đèn thông minh trong hệ thống.
/// </summary>
public class SmartLight : IDevice
{
    public string Name { get; }
    public bool IsOn { get; private set; }

    public SmartLight(string name)
    {
        Name = name;
        IsOn = false;
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
}
