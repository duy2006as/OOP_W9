/************************************************
 * Sinh viên: Hoàng Đình Duy
 * Mã sinh viên: 202418887
 * Bài thực hành 10: Smart Home Mini
 * File: IDevice.cs - Giao diện chung cho thiết bị
 ************************************************/

namespace SmartHomeMini;

/// <summary>
/// Abstraction chung cho mọi thiết bị trong ngôi nhà thông minh.
/// Các thiết bị cơ bản đều có khả năng Bật (TurnOn) và Tắt (TurnOff).
/// </summary>
public interface IDevice
{
    string Name { get; }
    bool IsOn { get; }
    void TurnOn();
    void TurnOff();
}
