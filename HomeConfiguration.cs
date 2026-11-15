/************************************************
 * Sinh viên: Hoàng Đình Duy
 * Mã sinh viên: 202418887
 * Bài thực hành 10: Smart Home Mini
 * File: HomeConfiguration.cs - Triển khai mẫu Singleton Pattern
 ************************************************/

namespace SmartHomeMini;

public sealed class HomeConfiguration
{
    private static readonly Lazy<HomeConfiguration> lazy =
        new(() => new HomeConfiguration());

    public static HomeConfiguration Instance => lazy.Value;

    public string HomeName { get; set; } = "My Home";
    public int NightLightLevel { get; set; } = 30;

    private HomeConfiguration()
    {
    }
}
