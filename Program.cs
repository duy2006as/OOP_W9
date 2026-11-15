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
            Console.WriteLine("             HE THONG DIEU KHIEN NHA THONG MINH - SMART HOME MINI");
            Console.WriteLine("       Sinh vien: Hoang Dinh Duy   -   Ma sinh vien: 202418887");
            Console.WriteLine("================================================================================");
            Console.ResetColor();
            Console.WriteLine(" 1. Kiem tra Factory tao thiet bi T1 va T2");
            Console.WriteLine(" 2. Kiem tra Singleton cau hinh nha T3");
            Console.WriteLine(" 3. Kiem tra Facade kich ban di lam GoToWork T4");
            Console.WriteLine(" 4. Kiem tra Facade kich ban di ngu GoToSleep T5");
            Console.WriteLine(" 5. Kiem tra cap nhat cau hinh va chuoi hoa thuong T6");
            Console.WriteLine(" 6. Dieu khien rieng le tung thiet bi");
            Console.WriteLine(" 0. Thoat chuong trinh");
            Console.WriteLine("================================================================================");
            Console.Write("Vui long chon chuc nang tu 0 den 6: ");

            string? choice = Console.ReadLine()?.Trim();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    InteractiveFactoryTest();
                    WaitForKey();
                    break;
                case "2":
                    InteractiveSingletonTest();
                    WaitForKey();
                    break;
                case "3":
                    InteractiveGoToWorkTest();
                    WaitForKey();
                    break;
                case "4":
                    InteractiveGoToSleepTest();
                    WaitForKey();
                    break;
                case "5":
                    InteractiveT6Test();
                    WaitForKey();
                    break;
                case "6":
                    InteractiveIndividualControl();
                    WaitForKey();
                    break;
                case "0":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Cam on ban da su dung chuong trinh. Tam biet.");
                    Console.ResetColor();
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Lua chon khong hop le, vui long nhap lai tu 0 den 6.");
                    Console.ResetColor();
                    WaitForKey();
                    break;
            }
        }
    }

    private static void InteractiveFactoryTest()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIEM TRA FACTORY TAO THIET BI T1 VA T2 ---");
        Console.ResetColor();
        Console.WriteLine("Ban co the nhap loai ho tro nhu light, fan, door de thu T1");
        Console.WriteLine("Hoac nhap loai chua ho tro nhu camera, robot, tivi de thu T2");
        Console.WriteLine();

        Console.Write("Nhap loai thiet bi muon tao: ");
        string type = Console.ReadLine() ?? string.Empty;

        Console.Write("Nhap ten thiet bi: ");
        string name = Console.ReadLine() ?? "Thiet bi mau";
        if (string.IsNullOrWhiteSpace(name))
        {
            name = "Thiet bi mau";
        }

        Console.WriteLine("\nDang goi phuong thuc Create cua DeviceFactory...");

        try
        {
            IDevice device = DeviceFactory.Create(type, name);

            if (device == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Factory tra ve null doi voi loai thiet bi nay.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ket qua T1: Thanh cong vi da tao duoc thiet bi " + device.Name + " voi kieu cu the la " + device.GetType().Name);
                Console.ResetColor();

                Console.WriteLine("\nThuc hien kiem tra bat tat thiet bi:");
                device.TurnOn();
                device.TurnOff();

                if (device is ILockable lockable)
                {
                    Console.WriteLine("Thiet bi nay co ho tro tinh nang khoa:");
                    lockable.Lock();
                    lockable.Unlock();
                }
            }
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ket qua T2: Bat dung ngoai le ArgumentException hop le theo yeu cau: " + ex.Message);
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ngoai le khong mong doi: " + ex.Message);
            Console.ResetColor();
        }
    }

    private static void InteractiveSingletonTest()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIEM TRA SINGLETON CAU HINH NHA T3 ---");
        Console.ResetColor();

        Console.WriteLine("Lay the hien thu nhat: var c1 = HomeConfiguration.Instance;");
        var c1 = HomeConfiguration.Instance;
        Console.WriteLine("Gia tri cau hinh hien tai qua bien c1: HomeName = " + c1.HomeName + ", NightLightLevel = " + c1.NightLightLevel);

        Console.WriteLine("\nTu nhap gia tri moi de kiem tra cap nhat qua bien c1:");
        Console.Write("Nhap ten nha moi: ");
        string? newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName))
        {
            c1.HomeName = newName;
        }

        Console.Write("Nhap muc sang ban dem moi tu 1 den 100: ");
        if (int.TryParse(Console.ReadLine(), out int newLevel))
        {
            c1.NightLightLevel = newLevel;
        }

        Console.WriteLine("\nLay the hien thu hai: var c2 = HomeConfiguration.Instance;");
        var c2 = HomeConfiguration.Instance;

        Console.WriteLine("Gia tri doc duoc qua bien c2: HomeName = " + c2.HomeName + ", NightLightLevel = " + c2.NightLightLevel);
        Console.WriteLine("Thuc hien kiem tra ReferenceEquals giua c1 va c2...");

        bool isSame = ReferenceEquals(c1, c2);

        if (isSame)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ket qua T3: ReferenceEquals bang true, ca hai bien cung tro den mot the hien duy nhat trong bo nho.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ket qua T3: ReferenceEquals bang false, hai bien tro den hai the hien khac nhau.");
            Console.ResetColor();
        }
    }

    private static void InteractiveGoToWorkTest()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIEM TRA FACADE KICH BAN DI LAM GOTOWORK T4 ---");
        Console.ResetColor();

        Console.Write("Nhap ten den phong khach hoac nhan Enter de dung mac dinh: ");
        string? lightName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(lightName)) lightName = "Den phong khach";

        Console.Write("Nhap ten quat hoac nhan Enter de dung mac dinh: ");
        string? fanName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fanName)) fanName = "Quat thong gio";

        Console.Write("Nhap ten cua chinh hoac nhan Enter de dung mac dinh: ");
        string? doorName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(doorName)) doorName = "Cua chinh";

        var light = new SmartLight(lightName);
        var fan = new SmartFan(fanName);
        var door = new SmartDoor(doorName);

        light.TurnOn();
        fan.TurnOn();
        door.Unlock();

        Console.WriteLine("\nTrang thai cac thiet bi truoc khi di lam:");
        Console.WriteLine("Den: Dang bat");
        Console.WriteLine("Quat: Dang bat");
        Console.WriteLine("Cua: Chua khoa");

        var facade = new SmartHomeFacade(light, fan, door);
        Console.WriteLine("\nThuc hien goi: facade.GoToWork();");
        facade.GoToWork();

        Console.WriteLine("\nTrang thai cac thiet bi sau khi goi GoToWork:");
        Console.WriteLine("Den: " + (light.IsOn ? "Dang bat" : "Dang tat"));
        Console.WriteLine("Quat: " + (fan.IsOn ? "Dang bat" : "Dang tat"));
        Console.WriteLine("Cua: " + (door.IsLocked ? "Da khoa" : "Chua khoa"));

        if (!light.IsOn && !fan.IsOn && door.IsLocked)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ket qua T4: Thanh cong vi den tat, quat tat va cua da duoc khoa an toan.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ket qua T4: Chua dat yeu cau.");
            Console.ResetColor();
        }
    }

    private static void InteractiveGoToSleepTest()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIEM TRA FACADE KICH BAN DI NGU GOTOSLEEP T5 ---");
        Console.ResetColor();

        Console.Write("Nhap ten den phong ngu hoac nhan Enter de dung mac dinh: ");
        string? lightName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(lightName)) lightName = "Den phong ngu";

        Console.Write("Nhap ten quat hoac nhan Enter de dung mac dinh: ");
        string? fanName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fanName)) fanName = "Quat phong ngu";

        Console.Write("Nhap ten cua phong hoac nhan Enter de dung mac dinh: ");
        string? doorName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(doorName)) doorName = "Cua phong";

        var light = new SmartLight(lightName);
        var fan = new SmartFan(fanName);
        var door = new SmartDoor(doorName);

        light.TurnOff();
        fan.TurnOn();
        door.Unlock();

        Console.WriteLine("\nTrang thai cac thiet bi truoc khi di ngu:");
        Console.WriteLine("Den: Dang tat");
        Console.WriteLine("Quat: Dang bat");
        Console.WriteLine("Cua: Chua khoa");

        var facade = new SmartHomeFacade(light, fan, door);
        Console.WriteLine("\nThuc hien goi: facade.GoToSleep();");
        facade.GoToSleep();

        Console.WriteLine("\nTrang thai cac thiet bi sau khi goi GoToSleep:");
        Console.WriteLine("Quat: " + (fan.IsOn ? "Dang bat" : "Dang tat"));
        Console.WriteLine("Cua: " + (door.IsLocked ? "Da khoa" : "Chua khoa"));
        Console.WriteLine("Den: " + (light.IsOn ? "Dang bat" : "Dang tat"));

        if (!fan.IsOn && door.IsLocked && light.IsOn)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ket qua T5: Thanh cong vi quat tat, cua da khoa va den da bat theo muc sang ban dem.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ket qua T5: Chua dat yeu cau.");
            Console.ResetColor();
        }
    }

    private static void InteractiveT6Test()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIEM TRA CAP NHAT CAU HINH VA CHUOI HOA THUONG T6 ---");
        Console.ResetColor();

        Console.Write("Nhap chuoi loai thiet bi co chua chu hoa hoac khoang trang vi du LiGhT hoac DoOr: ");
        string typeInput = Console.ReadLine() ?? "  LiGhT  ";
        if (string.IsNullOrWhiteSpace(typeInput)) typeInput = "  LiGhT  ";

        Console.Write("Nhap ten thiet bi muon dat: ");
        string nameInput = Console.ReadLine() ?? "Den hanh lang";
        if (string.IsNullOrWhiteSpace(nameInput)) nameInput = "Den hanh lang";

        try
        {
            var testDevice = DeviceFactory.Create(typeInput, nameInput);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Thanh cong: Factory xu ly chuoi tot va tao duoc thiet bi loai " + testDevice.GetType().Name);
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That bai khi xu ly chuoi: " + ex.Message);
            Console.ResetColor();
        }

        Console.WriteLine("\nNhap muc sang ban dem moi de kiem tra tinh dong bo cua Facade:");
        Console.Write("Nhap so tu 1 den 100: ");
        if (int.TryParse(Console.ReadLine(), out int newLevel))
        {
            HomeConfiguration.Instance.NightLightLevel = newLevel;
            Console.WriteLine("Da cap nhat NightLightLevel trong Singleton thanh " + newLevel);
        }

        var light = new SmartLight("Den kiem tra");
        var fan = new SmartFan("Quat kiem tra");
        var door = new SmartDoor("Cua kiem tra");
        var facade = new SmartHomeFacade(light, fan, door);

        Console.WriteLine("\nThuc hien goi GoToSleep de kiem tra muc sang moi:");
        facade.GoToSleep();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Ket qua T6: Hoan thanh kiem tra tinh dong bo va linh hoat cua he thong.");
        Console.ResetColor();
    }

    private static void InteractiveIndividualControl()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- DIEU KHIEN RIENG LE TUNG THIET BI ---");
        Console.ResetColor();

        var light = new SmartLight("Den phong khach");
        var fan = new SmartFan("Quat thong gio");
        var door = new SmartDoor("Cua chinh");

        bool exitSub = false;
        while (!exitSub)
        {
            Console.WriteLine("\nTrang thai hien tai cua cac thiet bi:");
            Console.WriteLine("Den: " + (light.IsOn ? "Dang bat" : "Dang tat"));
            Console.WriteLine("Quat: " + (fan.IsOn ? "Dang bat" : "Dang tat"));
            Console.WriteLine("Cua: " + (door.IsLocked ? "Da khoa" : "Chua khoa"));

            Console.WriteLine("\nChon thao tac rieng le:");
            Console.WriteLine("1. Bat den");
            Console.WriteLine("2. Tat den");
            Console.WriteLine("3. Bat quat");
            Console.WriteLine("4. Tat quat");
            Console.WriteLine("5. Khoa cua");
            Console.WriteLine("6. Mo khoa cua");
            Console.WriteLine("0. Quay lai menu chinh");
            Console.Write("Nhap lua chon thao tac: ");

            string? sub = Console.ReadLine()?.Trim();
            Console.WriteLine();

            switch (sub)
            {
                case "1": light.TurnOn(); break;
                case "2": light.TurnOff(); break;
                case "3": fan.TurnOn(); break;
                case "4": fan.TurnOff(); break;
                case "5": door.Lock(); break;
                case "6": door.Unlock(); break;
                case "0": exitSub = true; break;
                default:
                    Console.WriteLine("Lua chon thao tac khong hop le.");
                    break;
            }
        }
    }

    private static void WaitForKey()
    {
        Console.WriteLine("\nNhan phim Enter de tiep tuc...");
        try
        {
            Console.ReadLine();
        }
        catch
        {
        }
    }
}
