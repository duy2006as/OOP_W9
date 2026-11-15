/************************************************
 * Sinh viên: Hoàng Đình Duy
 * Mã sinh viên: 202418887
 * Bài thực hành 10: Smart Home Mini
 * File: HomeConfiguration.cs - Triển khai mẫu Singleton Pattern
 ************************************************/

namespace SmartHomeMini;

/// <summary>
/// Lưu trữ cấu hình toàn cục của ngôi nhà thông minh.
/// </summary>
public sealed class HomeConfiguration
{
    public string HomeName { get; set; } = "My Home";
    public int NightLightLevel { get; set; } = 30;

    public HomeConfiguration()
    {
    }

    public static HomeConfiguration Instance => new HomeConfiguration();
}
