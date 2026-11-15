/************************************************
 * Sinh viên: Hoàng Đình Duy
 * Mã sinh viên: 202418887
 * Bài thực hành 10: Smart Home Mini
 * File: ILockable.cs - Giao diện cho thiết bị có chức năng khóa
 ************************************************/

namespace SmartHomeMini;

/// <summary>
/// Giao diện dành riêng cho các thiết bị có tính năng khóa/mở khóa an ninh.
/// Tách rời khỏi IDevice để tuân thủ Interface Segregation Principle (ISP),
/// tránh việc đèn hay quạt bị ép phải cài đặt phương thức Lock/Unlock vô nghĩa.
/// </summary>
public interface ILockable
{
    bool IsLocked { get; }
    void Lock();
    void Unlock();
}
