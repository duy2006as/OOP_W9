/************************************************
 * Sinh viên: Hoàng Đình Duy
 * Mã sinh viên: 202418887
 * Bài thực hành 10: Smart Home Mini
 * File: Program.cs - Giao diện Console điều khiển và kiểm thử
 ************************************************/

using System;
using SmartHomeMini;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.InputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n================================================================================");
            Console.WriteLine("             HỆ THỐNG ĐIỀU KHIỂN NHÀ THÔNG MINH - SMART HOME MINI");
            Console.WriteLine("       Sinh viên: Hoàng Đình Duy   |   Mã sinh viên: 202418887");
            Console.WriteLine("================================================================================");
            Console.ResetColor();
            Console.WriteLine(" 1. [TỰ ĐỘNG] Chạy toàn bộ kịch bản kiểm thử T1 -> T6");
            Console.WriteLine(" 2. [TỰ NHẬP] Kiểm thử tương tác Factory Pattern (Tạo thiết bị)");
            Console.WriteLine(" 3. [TỰ NHẬP] Kiểm thử tương tác Singleton Pattern (Cấu hình nhà)");
            Console.WriteLine(" 4. [TỰ NHẬP] Kiểm thử tương tác Facade Pattern (Kịch bản GoToWork / GoToSleep)");
            Console.WriteLine(" 0. Thoát chương trình");
            Console.WriteLine("================================================================================");
            Console.Write("Vui lòng chọn chức năng (0-4): ");

            string? choice = Console.ReadLine()?.Trim();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    TestSuite.RunAllTests();
                    WaitForKey();
                    break;
                case "2":
                    InteractiveFactoryTest();
                    WaitForKey();
                    break;
                case "3":
                    InteractiveSingletonTest();
                    WaitForKey();
                    break;
                case "4":
                    InteractiveFacadeTest();
                    WaitForKey();
                    break;
                case "0":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Cảm ơn bạn đã sử dụng chương trình. Tạm biệt!");
                    Console.ResetColor();
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Lựa chọn không hợp lệ, vui lòng chọn lại từ 0 đến 4.");
                    Console.ResetColor();
                    WaitForKey();
                    break;
            }
        }
    }

    /// <summary>
    /// Kiểm thử tương tác Factory Pattern: Người dùng tự do nhập loại thiết bị và tên thiết bị.
    /// </summary>
    private static void InteractiveFactoryTest()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIỂM THỬ TƯƠNG TÁC: FACTORY PATTERN (DeviceFactory) ---");
        Console.ResetColor();
        Console.WriteLine("Gợi ý các trường hợp bạn có thể nhập thử:");
        Console.WriteLine(" + Loại hỗ trợ chuẩn: 'light', 'fan', 'door'");
        Console.WriteLine(" + Kiểm tra chữ hoa/thường: 'LIGHT', 'Fan', '  door  '");
        Console.WriteLine(" + Kiểm tra thiết bị chưa hỗ trợ: 'camera', 'tv', 'robot'");
        Console.WriteLine();

        Console.Write(">> Nhập loại thiết bị (type): ");
        string type = Console.ReadLine() ?? string.Empty;

        Console.Write(">> Nhập tên cho thiết bị (name): ");
        string name = Console.ReadLine() ?? "Thiết bị mẫu";

        Console.WriteLine("\nĐang gọi: DeviceFactory.Create(\"{0}\", \"{1}\")...", type, name);

        try
        {
            IDevice device = DeviceFactory.Create(type, name);

            if (device == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[KẾT QUẢ]: Factory trả về null đối với loại '{0}'.", type);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[THÀNH CÔNG] Đã tạo đối tượng: {0} (Kiểu cụ thể: {1})", device.Name, device.GetType().Name);
                Console.ResetColor();

                Console.WriteLine("\nThực hiện thao tác thử trên thiết bị vừa tạo:");
                device.TurnOn();
                device.TurnOff();

                if (device is ILockable lockable)
                {
                    Console.WriteLine("Thiết bị này có hỗ trợ tính năng khóa (ILockable):");
                    lockable.Lock();
                    lockable.Unlock();
                }
            }
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[BẮT ĐƯỢC NGOẠI LỆ] ArgumentException: {0}", ex.Message);
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[NGOẠI LỆ]: {0} - {1}", ex.GetType().Name, ex.Message);
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Kiểm thử tương tác Singleton Pattern: Người dùng tự cấu hình ngôi nhà và kiểm tra tính duy nhất.
    /// </summary>
    private static void InteractiveSingletonTest()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIỂM THỬ TƯƠNG TÁC: SINGLETON PATTERN (HomeConfiguration) ---");
        Console.ResetColor();

        Console.WriteLine("Lấy tham chiếu đối tượng 1: var config1 = HomeConfiguration.Instance;");
        var config1 = HomeConfiguration.Instance;
        Console.WriteLine("Giá trị hiện tại của config1: HomeName = '{0}', NightLightLevel = {1}%",
            config1.HomeName, config1.NightLightLevel);

        Console.WriteLine("\nNhập dữ liệu mới để cập nhật cấu hình qua config1:");
        Console.Write(">> Nhập tên nhà mới: ");
        string? newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName))
        {
            config1.HomeName = newName;
        }

        Console.Write(">> Nhập độ sáng đèn ngủ ban đêm (1-100%): ");
        if (int.TryParse(Console.ReadLine(), out int newLevel))
        {
            config1.NightLightLevel = newLevel;
        }

        Console.WriteLine("\nBây giờ lấy tham chiếu đối tượng 2: var config2 = HomeConfiguration.Instance;");
        var config2 = HomeConfiguration.Instance;

        Console.WriteLine("Kiểm tra ReferenceEquals(config1, config2)...");
        bool isSame = ReferenceEquals(config1, config2);

        if (isSame)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[KẾT QUẢ]: ReferenceEquals = TRUE. Cả hai biến đều trỏ đến cùng 1 thể hiện duy nhất!");
            Console.WriteLine("Giá trị đọc từ config2: HomeName = '{0}', NightLightLevel = {1}%",
                config2.HomeName, config2.NightLightLevel);
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[KẾT QUẢ]: ReferenceEquals = FALSE. Hai tham chiếu trỏ đến hai đối tượng khác nhau.");
            Console.WriteLine("-> Giá trị trên config1: HomeName = '{0}', NightLightLevel = {1}%", config1.HomeName, config1.NightLightLevel);
            Console.WriteLine("-> Giá trị trên config2: HomeName = '{0}', NightLightLevel = {1}%", config2.HomeName, config2.NightLightLevel);
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Kiểm thử tương tác Facade Pattern: Kích hoạt các kịch bản hoặc điều khiển thiết bị riêng biệt.
    /// </summary>
    private static void InteractiveFacadeTest()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIỂM THỬ TƯƠNG TÁC: FACADE PATTERN (SmartHomeFacade) ---");
        Console.ResetColor();

        var light = new SmartLight("Đèn phòng khách");
        var fan = new SmartFan("Quạt thông gió");
        var door = new SmartDoor("Cửa chính");

        var facade = new SmartHomeFacade(light, fan, door);

        bool exitFacade = false;
        while (!exitFacade)
        {
            Console.WriteLine("\n--- Trạng thái hiện tại của các thiết bị ---");
            Console.WriteLine(" + Đèn : {0}", light.IsOn ? "ON (Đang Bật)" : "OFF (Đang Tắt)");
            Console.WriteLine(" + Quạt: {0}", fan.IsOn ? "ON (Đang Bật)" : "OFF (Đang Tắt)");
            Console.WriteLine(" + Cửa : {0}", door.IsLocked ? "LOCKED (Đã Khóa)" : "UNLOCKED (Chưa Khóa)");

            Console.WriteLine("\nChọn thao tác muốn thực hiện:");
            Console.WriteLine(" [1] Kích hoạt kịch bản Facade: Đi làm (GoToWork)");
            Console.WriteLine(" [2] Kích hoạt kịch bản Facade: Đi ngủ (GoToSleep)");
            Console.WriteLine(" [3] Điều khiển thiết bị riêng lẻ (Client gọi trực tiếp không qua Facade)");
            Console.WriteLine(" [0] Quay lại menu chính");
            Console.Write("Lựa chọn của bạn: ");

            string? subChoice = Console.ReadLine()?.Trim();
            Console.WriteLine();

            switch (subChoice)
            {
                case "1":
                    Console.WriteLine(">> Thực hiện: facade.GoToWork()...");
                    facade.GoToWork();
                    break;

                case "2":
                    Console.WriteLine(">> Thực hiện: facade.GoToSleep()...");
                    facade.GoToSleep();
                    break;

                case "3":
                    Console.WriteLine("Thao tác riêng lẻ trực tiếp với thiết bị:");
                    Console.WriteLine("1. Bật Đèn | 2. Tắt Đèn | 3. Khóa Cửa | 4. Mở Cửa");
                    Console.Write("Chọn: ");
                    string? devOp = Console.ReadLine()?.Trim();
                    if (devOp == "1") light.TurnOn();
                    else if (devOp == "2") light.TurnOff();
                    else if (devOp == "3") door.Lock();
                    else if (devOp == "4") door.Unlock();
                    break;

                case "0":
                    exitFacade = true;
                    break;

                default:
                    Console.WriteLine("Lựa chọn không hợp lệ.");
                    break;
            }
        }
    }

    private static void WaitForKey()
    {
        Console.WriteLine("\nNhấn phím bất kỳ (hoặc Enter) để tiếp tục...");
        try
        {
            if (Console.IsInputRedirected)
            {
                Console.ReadLine();
            }
            else
            {
                Console.ReadKey(true);
            }
        }
        catch
        {
        }
    }
}
