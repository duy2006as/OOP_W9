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

            Console.WriteLine(" KIEM THU 3 DESIGN PATTERN");
            Console.WriteLine(" 1. Kiem thu Factory Pattern");
            Console.WriteLine(" 2. Kiem thu Singleton Pattern");
            Console.WriteLine(" 3. Kiem thu Facade Pattern");
            Console.WriteLine();
            Console.WriteLine(" KIEM THU CHI TIET CAC TINH HUONG TU T1 DEN T6");
            Console.WriteLine(" 4. Kiem thu T1: Tao light fan door bang Factory");
            Console.WriteLine(" 5. Kiem thu T2: Tao thiet bi chua ho tro phat sinh ArgumentException");
            Console.WriteLine(" 6. Kiem thu T3: Lay hai the hien HomeConfiguration duy nhat");
            Console.WriteLine(" 7. Kiem thu T4: Kich ban di lam GoToWork");
            Console.WriteLine(" 8. Kiem thu T5: Kich ban di ngu GoToSleep");
            Console.WriteLine(" 9. Kiem thu T6: Cap nhat cau hinh va chuoi ky tu hoa thuong");
            Console.WriteLine();
            Console.WriteLine(" THAO TAC KHAC");
            Console.WriteLine(" 10. Dieu khien rieng le tung thiet bi");
            Console.WriteLine(" 0. Thoat chuong trinh");
            Console.WriteLine("================================================================================");
            Console.Write("Vui long chon chuc nang tu 0 den 10: ");

            string? choice = Console.ReadLine()?.Trim();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    TestFactoryPattern();
                    WaitForKey();
                    break;
                case "2":
                    TestSingletonPattern();
                    WaitForKey();
                    break;
                case "3":
                    TestFacadePattern();
                    WaitForKey();
                    break;
                case "4":
                    TestScenarioT1();
                    WaitForKey();
                    break;
                case "5":
                    TestScenarioT2();
                    WaitForKey();
                    break;
                case "6":
                    TestScenarioT3();
                    WaitForKey();
                    break;
                case "7":
                    TestScenarioT4();
                    WaitForKey();
                    break;
                case "8":
                    TestScenarioT5();
                    WaitForKey();
                    break;
                case "9":
                    TestScenarioT6();
                    WaitForKey();
                    break;
                case "10":
                    TestIndividualControl();
                    WaitForKey();
                    break;
                case "0":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Cam on ban da su dung chuong trinh. Tam biet.");
                    Console.ResetColor();
                    return;
                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Lua chon khong hop le, vui long nhap lai so tu 0 den 10.");
                    Console.ResetColor();
                    WaitForKey();
                    break;
            }
        }
    }

    private static void TestFactoryPattern()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIEM THU FACTORY PATTERN ---");
        Console.ResetColor();
        Console.WriteLine("Moi ban tu nhap loai thiet bi vi du light hoac fan hoac door:");
        Console.Write("Loai thiet bi: ");
        string type = Console.ReadLine() ?? "light";
        if (string.IsNullOrWhiteSpace(type)) type = "light";

        Console.Write("Ten thiet bi: ");
        string name = Console.ReadLine() ?? "Thiet bi phong khach";
        if (string.IsNullOrWhiteSpace(name)) name = "Thiet bi phong khach";

        Console.WriteLine("\nDang goi DeviceFactory.Create...");
        try
        {
            IDevice device = DeviceFactory.Create(type, name);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ket qua: Factory tao thanh cong thiet bi " + device.Name + " voi kieu la " + device.GetType().Name);
            Console.ResetColor();

            Console.WriteLine("\nThao tac thu voi thiet bi vua tao:");
            device.TurnOn();
            device.TurnOff();

            if (device is ILockable lockable)
            {
                Console.WriteLine("Thiet bi co them giao dien ILockable:");
                lockable.Lock();
                lockable.Unlock();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Thong bao tu Factory: " + ex.Message);
            Console.ResetColor();
        }
    }

    private static void TestSingletonPattern()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIEM THU SINGLETON PATTERN ---");
        Console.ResetColor();

        var config1 = HomeConfiguration.Instance;
        Console.WriteLine("Gia tri hien tai qua the hien thu nhat:");
        Console.WriteLine("HomeName: " + config1.HomeName);
        Console.WriteLine("NightLightLevel: " + config1.NightLightLevel);

        Console.WriteLine("\nMoi ban tu nhap thong tin moi de cap nhat qua the hien thu nhat:");
        Console.Write("Nhap ten nha moi: ");
        string? newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName))
        {
            config1.HomeName = newName;
        }

        Console.Write("Nhap muc sang ban dem moi tu 1 den 100: ");
        if (int.TryParse(Console.ReadLine(), out int newLevel))
        {
            config1.NightLightLevel = newLevel;
        }

        Console.WriteLine("\nLay the hien thu hai qua bien config2:");
        var config2 = HomeConfiguration.Instance;
        Console.WriteLine("HomeName doc tu config2: " + config2.HomeName);
        Console.WriteLine("NightLightLevel doc tu config2: " + config2.NightLightLevel);

        Console.WriteLine("\nKiem tra ReferenceEquals giua config1 va config2...");
        if (ReferenceEquals(config1, config2))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ket qua: ReferenceEquals bang true, ca hai bien cung tro den mot the hien duy nhat trong bo nho.");
            Console.WriteLine("Du lieu duoc chia se dong bo va nhat quan tren toan bo ung dung.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ket qua: ReferenceEquals bang false, hai the hien khac nhau.");
            Console.ResetColor();
        }
    }

    private static void TestFacadePattern()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIEM THU FACADE PATTERN ---");
        Console.ResetColor();

        Console.Write("Nhap ten den cho he thong: ");
        string? lightName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(lightName)) lightName = "Den tong hop";

        Console.Write("Nhap ten quat cho he thong: ");
        string? fanName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fanName)) fanName = "Quat tong hop";

        Console.Write("Nhap ten cua cho he thong: ");
        string? doorName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(doorName)) doorName = "Cua tong hop";

        var light = new SmartLight(lightName);
        var fan = new SmartFan(fanName);
        var door = new SmartDoor(doorName);
        var facade = new SmartHomeFacade(light, fan, door);

        Console.WriteLine("\nMoi ban tu chon thao tac Facade muon kiem tra:");
        Console.WriteLine("1. Kich hoat kịch ban di lam GoToWork");
        Console.WriteLine("2. Kich hoat kịch ban di ngu GoToSleep");
        Console.Write("Nhap lua chon: ");
        string? sub = Console.ReadLine()?.Trim();
        Console.WriteLine();

        if (sub == "1")
        {
            Console.WriteLine("Thuc hien facade.GoToWork...");
            facade.GoToWork();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Facade da dieu phoi tat den, tat quat va khoa cua trong mot thao tac don gian.");
            Console.ResetColor();
        }
        else if (sub == "2")
        {
            Console.WriteLine("Thuc hien facade.GoToSleep...");
            facade.GoToSleep();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Facade da dieu phoi tat quat, khoa cua, bat den va thong bao muc sang ban dem.");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine("Lua chon thao tac khong hop le.");
        }
    }

    private static void TestScenarioT1()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIEM THU T1: TAO LIGHT FAN DOOR BANG FACTORY ---");
        Console.ResetColor();

        Console.Write("Nhap ten cho den: ");
        string? lightName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(lightName)) lightName = "Living room light";

        Console.Write("Nhap ten cho quat: ");
        string? fanName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fanName)) fanName = "Living room fan";

        Console.Write("Nhap ten cho cua: ");
        string? doorName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(doorName)) doorName = "Main door";

        var light = DeviceFactory.Create("light", lightName);
        var fan = DeviceFactory.Create("fan", fanName);
        var door = DeviceFactory.Create("door", doorName);

        bool ok = (light is SmartLight) && (fan is SmartFan) && (door is SmartDoor);

        if (ok)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ket qua T1: Thanh cong vi Factory tra ve dung cac kieu doi tuong SmartLight, SmartFan, SmartDoor.");
            Console.ResetColor();
            light.TurnOn();
            fan.TurnOff();
            if (door is ILockable lockable)
            {
                lockable.Lock();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ket qua T1: Chua dung kieu doi tuong.");
            Console.ResetColor();
        }
    }

    private static void TestScenarioT2()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIEM THU T2: TAO THIET BI CHUA HO TRO PHAT SINH ARGUMENTEXCEPTION ---");
        Console.ResetColor();

        Console.Write("Nhap loai thiet bi chua ho tro muon thu nghiem vi du camera: ");
        string? unType = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(unType)) unType = "camera";

        Console.WriteLine("Goi DeviceFactory.Create voi loai " + unType + "...");

        try
        {
            var dev = DeviceFactory.Create(unType, "Security device");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ket qua T2: Khong mong doi vi da tao thanh cong " + dev.GetType().Name);
            Console.ResetColor();
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ket qua T2: Thanh cong vi he thong da phat sinh dung ArgumentException nhu yeu cau.");
            Console.WriteLine("Noi dung ngoai le: " + ex.Message);
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ngoai le sai loai: " + ex.Message);
            Console.ResetColor();
        }
    }

    private static void TestScenarioT3()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIEM THU T3: LAY HAI THE HIEN HOMECONFIGURATION DUY NHAT ---");
        Console.ResetColor();

        Console.WriteLine("Thuc hien lay c1 = HomeConfiguration.Instance");
        var c1 = HomeConfiguration.Instance;

        Console.WriteLine("Thuc hien lay c2 = HomeConfiguration.Instance");
        var c2 = HomeConfiguration.Instance;

        bool same = ReferenceEquals(c1, c2);

        if (same)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ket qua T3: Thanh cong vi ReferenceEquals giua c1 va c2 la dung.");
            Console.WriteLine("Ca hai bien cung tham chieu den mot the hien duy nhat.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ket qua T3: That bai vi ReferenceEquals tra ve false.");
            Console.ResetColor();
        }
    }

    private static void TestScenarioT4()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIEM THU T4: KICH BAN DI LAM GOTOWORK ---");
        Console.ResetColor();

        var light = new SmartLight("Den phong khach");
        var fan = new SmartFan("Quat thong gio");
        var door = new SmartDoor("Cua chinh");

        light.TurnOn();
        fan.TurnOn();
        door.Unlock();

        Console.WriteLine("Trang thai truoc khi di lam:");
        Console.WriteLine("Den: Dang bat");
        Console.WriteLine("Quat: Dang bat");
        Console.WriteLine("Cua: Chua khoa");

        var facade = new SmartHomeFacade(light, fan, door);
        Console.WriteLine("\nThuc hien goi: facade.GoToWork();");
        facade.GoToWork();

        Console.WriteLine("\nTrang thai sau khi di lam:");
        Console.WriteLine("Den: " + (light.IsOn ? "Dang bat" : "Dang tat"));
        Console.WriteLine("Quat: " + (fan.IsOn ? "Dang bat" : "Dang tat"));
        Console.WriteLine("Cua: " + (door.IsLocked ? "Da khoa" : "Chua khoa"));

        if (!light.IsOn && !fan.IsOn && door.IsLocked)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ket qua T4: Thanh cong vi den tat, quat tat, cua da khoa.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ket qua T4: Chua dat yeu cau.");
            Console.ResetColor();
        }
    }

    private static void TestScenarioT5()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIEM THU T5: KICH BAN DI NGU GOTOSLEEP ---");
        Console.ResetColor();

        var light = new SmartLight("Den phong ngu");
        var fan = new SmartFan("Quat phong ngu");
        var door = new SmartDoor("Cua phong");

        light.TurnOff();
        fan.TurnOn();
        door.Unlock();

        Console.WriteLine("Trang thai truoc khi di ngu:");
        Console.WriteLine("Den: Dang tat");
        Console.WriteLine("Quat: Dang bat");
        Console.WriteLine("Cua: Chua khoa");

        var facade = new SmartHomeFacade(light, fan, door);
        Console.WriteLine("\nThuc hien goi: facade.GoToSleep();");
        facade.GoToSleep();

        Console.WriteLine("\nTrang thai sau khi di ngu:");
        Console.WriteLine("Quat: " + (fan.IsOn ? "Dang bat" : "Dang tat"));
        Console.WriteLine("Cua: " + (door.IsLocked ? "Da khoa" : "Chua khoa"));
        Console.WriteLine("Den: " + (light.IsOn ? "Dang bat" : "Dang tat"));

        if (!fan.IsOn && door.IsLocked && light.IsOn)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ket qua T5: Thanh cong vi quat tat, cua da khoa, den bat va muc sang ban dem da hien thi.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ket qua T5: Chua dat yeu cau.");
            Console.ResetColor();
        }
    }

    private static void TestScenarioT6()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("--- KIEM THU T6: CAP NHAT CAU HINH VA CHUOI KY TU HOA THUONG ---");
        Console.ResetColor();

        Console.Write("Nhap chuoi loai thiet bi chua chu hoa va khoang trang vi du  LiGhT : ");
        string? rawType = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(rawType)) rawType = "  LiGhT  ";

        try
        {
            var dev = DeviceFactory.Create(rawType, "Thiet bi kiem tra");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Thanh cong: Factory xu ly tot chuoi va tao thanh cong " + dev.GetType().Name);
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("That bai khi xu ly chuoi: " + ex.Message);
            Console.ResetColor();
        }

        Console.WriteLine("\nNhap muc sang ban dem moi de kiem tra tinh dong bo:");
        Console.Write("Nhap so tu 1 den 100: ");
        if (int.TryParse(Console.ReadLine(), out int lvl))
        {
            HomeConfiguration.Instance.NightLightLevel = lvl;
            Console.WriteLine("Da cap nhat NightLightLevel trong Singleton thanh " + lvl);
        }

        var light = new SmartLight("Den kiem tra");
        var fan = new SmartFan("Quat kiem tra");
        var door = new SmartDoor("Cua kiem tra");
        var facade = new SmartHomeFacade(light, fan, door);

        Console.WriteLine("\nThuc hien goi GoToSleep de kiem tra muc sang moi:");
        facade.GoToSleep();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Ket qua T6: Hoan thanh kiem tra tinh dong bo va xu ly chuoi linh hoat.");
        Console.ResetColor();
    }

    private static void TestIndividualControl()
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
