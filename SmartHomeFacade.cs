/************************************************
 * Sinh viên: Hoàng Đình Duy
 * Mã sinh viên: 202418887
 * Bài thực hành 10: Smart Home Mini
 * File: SmartHomeFacade.cs - Triển khai mẫu Facade Pattern
 ************************************************/

namespace SmartHomeMini;

/// <summary>
/// SmartHomeFacade cung cấp giao diện cấp cao để thực hiện
/// các kịch bản phối hợp nhiều thiết bị (GoToWork, GoToSleep).
/// </summary>
public sealed class SmartHomeFacade
{
    private readonly IDevice light;
    private readonly IDevice fan;
    private readonly ILockable door;

    public SmartHomeFacade(IDevice light, IDevice fan, ILockable door)
    {
        this.light = light;
        this.fan = fan;
        this.door = door;
    }

    public void GoToWork()
    {
        light.TurnOn(); 
        fan.TurnOff();
        door.Lock();
    }

    public void GoToSleep()
    {
        fan.TurnOff();
        light.TurnOn();
        int level = 50; 
        Console.WriteLine($"Night light level: {level}%");
    }
}
