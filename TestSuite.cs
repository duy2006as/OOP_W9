/************************************************
 * Sinh viên: Hoàng Đình Duy
 * Mã sinh viên: 202418887
 * Bài thực hành 10: Smart Home Mini
 * File: TestSuite.cs - Bộ kiểm thử tự động từ T1 đến T6
 ************************************************/

namespace SmartHomeMini;

public static class TestSuite
{
    public static void RunAllTests()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n================================================================================");
        Console.WriteLine("          BẮT ĐẦU CHẠY BỘ KIỂM THỬ TỰ ĐỘNG (T1 -> T6) CHO DỰ ÁN");
        Console.WriteLine("================================================================================");
        Console.ResetColor();

        int passedCount = 0;
        int failedCount = 0;

        // ---------------------------------------------------------------------
        // TEST T1: Tạo light, fan, door - Factory trả đúng kiểu đối tượng
        // ---------------------------------------------------------------------
        Console.WriteLine("\n--- [T1] Kiểm tra tạo các thiết bị hợp lệ qua Factory ---");
        try
        {
            var light = DeviceFactory.Create("light", "Living room light");
            var fan = DeviceFactory.Create("fan", "Living room fan");
            var door = DeviceFactory.Create("door", "Main door");

            bool isLightValid = light is SmartLight;
            bool isFanValid = fan is SmartFan;
            bool isDoorValid = door is SmartDoor;

            if (isLightValid && isFanValid && isDoorValid)
            {
                PrintSuccess("T1", "Factory trả về đúng kiểu đối tượng SmartLight, SmartFan, SmartDoor.");
                passedCount++;
            }
            else
            {
                PrintFailure("T1", "Kiểu đối tượng trả về không khớp mong đợi.");
                failedCount++;
            }
        }
        catch (Exception ex)
        {
            PrintFailure("T1", $"Gặp ngoại lệ ngoài mong đợi: {ex.Message}");
            failedCount++;
        }

        // ---------------------------------------------------------------------
        // TEST T2: Tạo loại camera khi chưa hỗ trợ - Phát sinh ArgumentException
        // ---------------------------------------------------------------------
        Console.WriteLine("\n--- [T2] Kiểm tra thiết bị không hỗ trợ ('camera') ---");
        try
        {
            var camera = DeviceFactory.Create("camera", "Security camera");
            if (camera == null)
            {
                PrintFailure("T2", "Factory trả về null thay vì ném ArgumentException.");
                failedCount++;
            }
            else
            {
                PrintFailure("T2", "Factory tạo thành công thiết bị không hỗ trợ (không mong đợi).");
                failedCount++;
            }
        }
        catch (ArgumentException)
        {
            PrintSuccess("T2", "Đã phát sinh đúng ArgumentException khi tạo thiết bị không hỗ trợ.");
            passedCount++;
        }
        catch (Exception ex)
        {
            PrintFailure("T2", $"Phát sinh ngoại lệ sai loại: {ex.GetType().Name} thay vì ArgumentException.");
            failedCount++;
        }

        // ---------------------------------------------------------------------
        // TEST T3: Lấy HomeConfiguration.Instance hai lần - Hai biến cùng tham chiếu
        // ---------------------------------------------------------------------
        Console.WriteLine("\n--- [T3] Kiểm tra tính duy nhất của Singleton (HomeConfiguration) ---");
        try
        {
            var c1 = HomeConfiguration.Instance;
            var c2 = HomeConfiguration.Instance;

            bool isSameReference = ReferenceEquals(c1, c2);

            if (isSameReference)
            {
                PrintSuccess("T3", "Hai biến c1 và c2 cùng tham chiếu đến một đối tượng duy nhất.");
                passedCount++;
            }
            else
            {
                PrintFailure("T3", "ReferenceEquals(c1, c2) = False. Hai biến trỏ đến 2 thể hiện khác nhau.");
                failedCount++;
            }
        }
        catch (Exception ex)
        {
            PrintFailure("T3", $"Lỗi khi kiểm tra Singleton: {ex.Message}");
            failedCount++;
        }

        // ---------------------------------------------------------------------
        // TEST T4: Gọi GoToWork() - Đèn tắt, quạt tắt, cửa khóa
        // ---------------------------------------------------------------------
        Console.WriteLine("\n--- [T4] Kiểm tra kịch bản Facade: GoToWork() ---");
        try
        {
            var light = new SmartLight("Living room light");
            var fan = new SmartFan("Living room fan");
            var door = new SmartDoor("Main door");

            light.TurnOn();
            fan.TurnOn();
            door.Unlock();

            var facade = new SmartHomeFacade(light, fan, door);
            Console.WriteLine(">> Thực hiện gọi facade.GoToWork():");
            facade.GoToWork();

            bool isLightOff = !light.IsOn;
            bool isFanOff = !fan.IsOn;
            bool isDoorLocked = door.IsLocked;

            if (isLightOff && isFanOff && isDoorLocked)
            {
                PrintSuccess("T4", "GoToWork() hoạt động chuẩn xác: Đèn tắt, Quạt tắt, Cửa khóa.");
                passedCount++;
            }
            else
            {
                PrintFailure("T4", "Trạng thái thiết bị sau GoToWork() chưa đạt kết quả mong đợi.");
                if (!isLightOff) Console.WriteLine("   -> Đèn hiện đang BẬT.");
                if (!isFanOff) Console.WriteLine("   -> Quạt hiện chưa tắt.");
                if (!isDoorLocked) Console.WriteLine("   -> Cửa hiện chưa được khóa.");
                failedCount++;
            }
        }
        catch (Exception ex)
        {
            PrintFailure("T4", $"Lỗi thực thi GoToWork: {ex.Message}");
            failedCount++;
        }

        // ---------------------------------------------------------------------
        // TEST T5: Gọi GoToSleep() - Quạt tắt, cửa khóa, đèn bật, hiện mức sáng
        // ---------------------------------------------------------------------
        Console.WriteLine("\n--- [T5] Kiểm tra kịch bản Facade: GoToSleep() ---");
        try
        {
            var light = new SmartLight("Bedroom light");
            var fan = new SmartFan("Bedroom fan");
            var door = new SmartDoor("Front door");

            fan.TurnOn();
            door.Unlock();
            light.TurnOff();

            var facade = new SmartHomeFacade(light, fan, door);
            Console.WriteLine(">> Thực hiện gọi facade.GoToSleep():");
            facade.GoToSleep();

            bool isFanOff = !fan.IsOn;
            bool isDoorLocked = door.IsLocked;
            bool isLightOn = light.IsOn;

            if (isFanOff && isDoorLocked && isLightOn)
            {
                PrintSuccess("T5", "GoToSleep() hoạt động chuẩn xác: Quạt tắt, Cửa khóa, Đèn bật.");
                passedCount++;
            }
            else
            {
                PrintFailure("T5", "Trạng thái thiết bị sau GoToSleep() chưa đạt kết quả mong đợi.");
                if (!isDoorLocked) Console.WriteLine("   -> Cửa chưa được khóa khi đi ngủ.");
                if (!isFanOff) Console.WriteLine("   -> Quạt chưa tắt.");
                if (!isLightOn) Console.WriteLine("   -> Đèn chưa bật.");
                failedCount++;
            }
        }
        catch (Exception ex)
        {
            PrintFailure("T5", $"Lỗi thực thi GoToSleep: {ex.Message}");
            failedCount++;
        }

        // ---------------------------------------------------------------------
        // TEST T6: Cập nhật cấu hình Singleton và kiểm tra phản ánh vào Facade
        //          Đồng thời kiểm tra khả năng xử lý chuỗi hoa/thường của Factory
        // ---------------------------------------------------------------------
        Console.WriteLine("\n--- [T6] Kiểm tra tích hợp cấu hình động & Xử lý chuỗi Factory ---");
        try
        {
            var config = HomeConfiguration.Instance;
            config.NightLightLevel = 75;

            var configCheck = HomeConfiguration.Instance;
            bool isConfigSynchronized = (configCheck.NightLightLevel == 75);

            bool isFactoryRobust = false;
            try
            {
                var lightCaseTest = DeviceFactory.Create("  LiGhT  ", "Hallway light");
                isFactoryRobust = lightCaseTest is SmartLight;
            }
            catch
            {
                isFactoryRobust = false;
            }

            if (isConfigSynchronized && isFactoryRobust)
            {
                PrintSuccess("T6", "Tích hợp hoàn thành: Cấu hình đồng bộ và Factory xử lý chuẩn xác chuỗi nhập.");
                passedCount++;
            }
            else
            {
                PrintFailure("T6", "Kiểm thử T6 chưa đạt mong đợi:");
                if (!isConfigSynchronized)
                    Console.WriteLine("   -> Cấu hình không đồng bộ giữa các lần truy cập.");
                if (!isFactoryRobust)
                    Console.WriteLine("   -> Factory không nhận diện được chuỗi có khoảng trắng hoặc chữ hoa.");
                failedCount++;
            }
        }
        catch (Exception ex)
        {
            PrintFailure("T6", $"Lỗi thực thi T6: {ex.Message}");
            failedCount++;
        }

        // ---------------------------------------------------------------------
        // TỔNG KẾT BÁO CÁO KIỂM THỬ
        // ---------------------------------------------------------------------
        Console.WriteLine("\n================================================================================");
        Console.WriteLine($"               TỔNG KẾT KIỂM THỬ: {passedCount} PASSED / {failedCount} FAILED");
        Console.WriteLine("================================================================================\n");
    }

    private static void PrintSuccess(string testCode, string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write($"[{testCode}] [PASSED] ");
        Console.ResetColor();
        Console.WriteLine(message);
    }

    private static void PrintFailure(string testCode, string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write($"[{testCode}] [FAILED] ");
        Console.ResetColor();
        Console.WriteLine(message);
    }
}
