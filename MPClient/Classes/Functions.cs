using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using Microsoft.Win32;
using System.ComponentModel;

namespace IbizaClient.Classes
{
    class Functions
    {
        public static string ReadTextFromUrl(string url)
        {
            CookieContainer cookieContainer = new CookieContainer();
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.UserAgent = "TurkIbiza";
            httpWebRequest.CookieContainer = cookieContainer;
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            return new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding(httpWebResponse.CharacterSet)).ReadToEnd();
        }

        public static string GetPCInformation()
        {
            return string.Concat(new object[]
            {
                Environment.ProcessorCount,
                "/",
                Environment.MachineName,
                "/",
                Environment.UserDomainName,
                "\\",
                Environment.UserName,
                "/",
                Environment.GetLogicalDrives().Length
            });
        }

        public static string WMIC()
        {
            string result = "";
            foreach (ManagementBaseObject managementBaseObject in new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard").Get())
            {
                ManagementObject managementObject = (ManagementObject)managementBaseObject;
                try
                {
                    result = managementObject.GetPropertyValue("SerialNumber").ToString();
                }
                catch
                {
                }
            }
            return result;
        }

        public static string GetHDD()
        {
            ManagementObjectCollection instances = new ManagementClass("Win32_LogicalDisk").GetInstances();
            string text = "";
            foreach (ManagementBaseObject managementBaseObject in instances)
            {
                ManagementObject managementObject = (ManagementObject)managementBaseObject;
                text += Convert.ToString(managementObject["VolumeSerialNumber"]);
            }
            return text;
        }

        public static string ToBase64(string metin)
        {
            byte[] byt = System.Text.Encoding.UTF8.GetBytes(metin);
            return Convert.ToBase64String(byt);
        }

        public static string FromBase64(string metin)
        {
            byte[] byt = Convert.FromBase64String(metin);
            return System.Text.Encoding.UTF8.GetString(byt);
        }
 
        public static string Sifrele(string metin)
        {
            new StringBuilder();
            metin = ToBase64(metin);
            metin = metin.Replace("0", "-|000-");
            metin = metin.Replace("1", "-|111-");
            metin = metin.Replace("2", "-|222-");
            metin = metin.Replace("3", "-|333-");
            metin = metin.Replace("4", "-|444-");
            metin = metin.Replace("5", "-|555-");
            metin = metin.Replace("6", "-|666-");
            metin = metin.Replace("7", "-|777-");
            metin = metin.Replace("8", "-|888-");
            metin = metin.Replace("9", "-|999-");

            metin = metin.Replace("q", "-|99-");
            metin = metin.Replace("Q", "-|1-");
            metin = metin.Replace("w", "-|98-");
            metin = metin.Replace("W", "-|2-");
            metin = metin.Replace("e", "-|97-");
            metin = metin.Replace("E", "-|3-");
            metin = metin.Replace("r", "-|96-");
            metin = metin.Replace("R", "-|4-");
            metin = metin.Replace("y", "-|95-");
            metin = metin.Replace("Y", "-|5-");
            metin = metin.Replace("u", "-|94-");
            metin = metin.Replace("U", "-|6-");
            metin = metin.Replace("ı", "-|93-");
            metin = metin.Replace("I", "-|7-");
            metin = metin.Replace("o", "-|92-");
            metin = metin.Replace("O", "-|8-");
            metin = metin.Replace("p", "-|91-");
            metin = metin.Replace("P", "-|9-");
            metin = metin.Replace("ğ", "-|90-");
            metin = metin.Replace("Ğ", "-|10-");
            metin = metin.Replace("ü", "-|89-");
            metin = metin.Replace("Ü", "-|11-");
            metin = metin.Replace("a", "-|88-");
            metin = metin.Replace("A", "-|12-");
            metin = metin.Replace("b", "-|87-");
            metin = metin.Replace("B", "-|13-");
            metin = metin.Replace("c", "-|86-");
            metin = metin.Replace("C", "-|14-");
            metin = metin.Replace("d", "-|85-");
            metin = metin.Replace("D", "-|15-");
            metin = metin.Replace("f", "-|84-");
            metin = metin.Replace("F", "-|16-");
            metin = metin.Replace("g", "-|83-");
            metin = metin.Replace("G", "-|17-");
            metin = metin.Replace("h", "-|82-");
            metin = metin.Replace("H", "-|18-");
            metin = metin.Replace("j", "-|81-");
            metin = metin.Replace("J", "-|19-");
            metin = metin.Replace("k", "-|80-");
            metin = metin.Replace("K", "-|20-");
            metin = metin.Replace("l", "-|79-");
            metin = metin.Replace("L", "-|21-");
            metin = metin.Replace("ş", "-|78-");
            metin = metin.Replace("Ş", "-|22-");
            metin = metin.Replace("i", "-|77-");
            metin = metin.Replace("İ", "-|23-");
            metin = metin.Replace("z", "-|76-");
            metin = metin.Replace("Z", "-|24-");
            metin = metin.Replace("x", "-|75-");
            metin = metin.Replace("X", "-|25-");
            metin = metin.Replace("c", "-|74-");
            metin = metin.Replace("C", "-|26-");
            metin = metin.Replace("v", "-|73-");
            metin = metin.Replace("V", "-|27-");
            metin = metin.Replace("b", "-|72-");
            metin = metin.Replace("B", "-|28-");
            metin = metin.Replace("n", "-|71-");
            metin = metin.Replace("N", "-|29-");
            metin = metin.Replace("m", "-|70-");
            metin = metin.Replace("M", "-|30-");
            metin = metin.Replace("ö", "-|69-");
            metin = metin.Replace("Ö", "-|31-");
            metin = metin.Replace("ç", "-|68-");
            metin = metin.Replace("Ç", "-|32-");
            metin = metin.Replace("t", "-|67-");
            metin = metin.Replace("T", "-|33-");
            metin = metin.Replace("s", "-|66-");
            metin = metin.Replace("S", "-|34-");
            return metin.ToString();
        }

        public static string SifreCoz(string metin)
        {
            new StringBuilder();
            metin = replaceyap(metin, "0", "-|000-");
            metin = replaceyap(metin, "1", "-|111-");
            metin = replaceyap(metin, "2", "-|222-");
            metin = replaceyap(metin, "3", "-|333-");
            metin = replaceyap(metin, "4", "-|444-");
            metin = replaceyap(metin, "5", "-|555-");
            metin = replaceyap(metin, "6", "-|666-");
            metin = replaceyap(metin, "7", "-|777-");
            metin = replaceyap(metin, "8", "-|888-");
            metin = replaceyap(metin, "9", "-|999-");

            metin = replaceyap(metin, "q", "-|99-");
            metin = replaceyap(metin, "Q", "-|1-");
            metin = replaceyap(metin, "w", "-|98-");
            metin = replaceyap(metin, "W", "-|2-");
            metin = replaceyap(metin, "e", "-|97-");
            metin = replaceyap(metin, "E", "-|3-");
            metin = replaceyap(metin, "r", "-|96-");
            metin = replaceyap(metin, "R", "-|4-");
            metin = replaceyap(metin, "y", "-|95-");
            metin = replaceyap(metin, "Y", "-|5-");
            metin = replaceyap(metin, "u", "-|94-");
            metin = replaceyap(metin, "U", "-|6-");
            metin = replaceyap(metin, "ı", "-|93-");
            metin = replaceyap(metin, "I", "-|7-");
            metin = replaceyap(metin, "o", "-|92-");
            metin = replaceyap(metin, "O", "-|8-");
            metin = replaceyap(metin, "p", "-|91-");
            metin = replaceyap(metin, "P", "-|9-");
            metin = replaceyap(metin, "ğ", "-|90-");
            metin = replaceyap(metin, "Ğ", "-|10-");
            metin = replaceyap(metin, "ü", "-|89-");
            metin = replaceyap(metin, "Ü", "-|11-");
            metin = replaceyap(metin, "a", "-|88-");
            metin = replaceyap(metin, "A", "-|12-");
            metin = replaceyap(metin, "b", "-|87-");
            metin = replaceyap(metin, "B", "-|13-");
            metin = replaceyap(metin, "c", "-|86-");
            metin = replaceyap(metin, "C", "-|14-");
            metin = replaceyap(metin, "d", "-|85-");
            metin = replaceyap(metin, "D", "-|15-");
            metin = replaceyap(metin, "f", "-|84-");
            metin = replaceyap(metin, "F", "-|16-");
            metin = replaceyap(metin, "g", "-|83-");
            metin = replaceyap(metin, "G", "-|17-");
            metin = replaceyap(metin, "h", "-|82-");
            metin = replaceyap(metin, "H", "-|18-");
            metin = replaceyap(metin, "j", "-|81-");
            metin = replaceyap(metin, "J", "-|19-");
            metin = replaceyap(metin, "k", "-|80-");
            metin = replaceyap(metin, "K", "-|20-");
            metin = replaceyap(metin, "l", "-|79-");
            metin = replaceyap(metin, "L", "-|21-");
            metin = replaceyap(metin, "ş", "-|78-");
            metin = replaceyap(metin, "Ş", "-|22-");
            metin = replaceyap(metin, "i", "-|77-");
            metin = replaceyap(metin, "İ", "-|23-");
            metin = replaceyap(metin, "z", "-|76-");
            metin = replaceyap(metin, "Z", "-|24-");
            metin = replaceyap(metin, "x", "-|75-");
            metin = replaceyap(metin, "X", "-|25-");
            metin = replaceyap(metin, "c", "-|74-");
            metin = replaceyap(metin, "C", "-|26-");
            metin = replaceyap(metin, "v", "-|73-");
            metin = replaceyap(metin, "V", "-|27-");
            metin = replaceyap(metin, "b", "-|72-");
            metin = replaceyap(metin, "B", "-|28-");
            metin = replaceyap(metin, "n", "-|71-");
            metin = replaceyap(metin, "N", "-|29-");
            metin = replaceyap(metin, "m", "-|70-");
            metin = replaceyap(metin, "M", "-|30-");
            metin = replaceyap(metin, "ö", "-|69-");
            metin = replaceyap(metin, "Ö", "-|31-");
            metin = replaceyap(metin, "ç", "-|68-");
            metin = replaceyap(metin, "Ç", "-|32-");
            metin = replaceyap(metin, "t", "-|67-");
            metin = replaceyap(metin, "T", "-|33-");
            metin = replaceyap(metin, "s", "-|66-");
            metin = replaceyap(metin, "S", "-|34-");
            metin = metin.ToString();
            return FromBase64(metin);
        }

        public static string replaceyap(string metin, string metin2, string metin3)
        {
            metin = metin.Replace(metin3, metin2);
            return metin;
        }

        public static void ClientUpdate()
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(Functions.SifreCoz("-|6--|222--|999--|70--|85--|18--|85--|82--|86--|70--|27--|86--|25--|3--|111--|91--|5--|333--|19--|73--|86--|222--|999--|70--|85--|16--|75--|86--|86--|70--|111--|76--|87--|222--|24--|000-"), true);
            if (registryKey == null)
            {
                registryKey = Registry.LocalMachine.CreateSubKey(Functions.SifreCoz("-|6--|222--|999--|70--|85--|18--|85--|82--|86--|70--|27--|86--|25--|3--|111--|91--|5--|333--|19--|73--|86--|222--|999--|70--|85--|16--|75--|86--|86--|70--|111--|76--|87--|222--|24--|000-"));
            }
            registryKey.SetValue("updatepath", System.Reflection.Assembly.GetExecutingAssembly().Location);
            WebClient indir = new WebClient();
            Uri yol = new Uri(Globals.site + "IbizaUpdater.exe");
            indir.DownloadFileCompleted += new AsyncCompletedEventHandler(DosyaIndirildi);
            indir.DownloadFileAsync(yol, Path.GetTempPath() + "IbizaUpdater.exe");
        }

        public static void DosyaIndirildi(object sender, AsyncCompletedEventArgs e)
        {
            System.Diagnostics.Process.Start(Path.GetTempPath() + "IbizaUpdater.exe");
            Environment.Exit(0);
        }

        public static int RemoveClient()
        {
            ReadTextFromUrl(Globals.site + SifreCoz("-|86--|70--|27--|67--|87--|333--|24--|79--|21--|71--|13--|92--|86--|15--|999--|96--|24--|25--|80--|999-") + Sifrele(Globals.loginkey));
            return 0;
        }

        public static string GenerateNumber()
        {
            Random random = new Random();
            string text = "";
            for (int i = 1; i < 20; i++)
            {
                text += random.Next(0, 9).ToString();
            }
            return text;
        }

        public static void SetPlayerName(string name)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(Functions.SifreCoz("-|6--|222--|999--|70--|85--|18--|85--|82--|86--|70--|27--|86--|25--|3--|111--|91--|5--|333--|19--|73--|86--|222--|999--|70--|85--|16--|75--|86--|86--|70--|111--|76--|87--|222--|24--|000-"), true);
            if (registryKey == null)
            {
                registryKey = Registry.LocalMachine.CreateSubKey(Functions.SifreCoz("-|6--|222--|999--|70--|85--|18--|85--|82--|86--|70--|27--|86--|25--|3--|111--|91--|5--|333--|19--|73--|86--|222--|999--|70--|85--|16--|75--|86--|86--|70--|111--|76--|87--|222--|24--|000-"));
            }
            registryKey.SetValue("character", name);
        }

        public static void GTAKAPAT()
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.MainWindowTitle.Contains("GTA:SA:MP") || process.MainWindowTitle.Contains("GTA: San Andreas"))
                {
                    process.Kill();
                }
                else if (process.ProcessName == "gta_sa")
                {
                    process.Kill();
                }
            }
        }

        public static void SAMPKAPAT()
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.MainWindowTitle.Contains("SA-MP 0.3"))
                {
                    process.Kill();
                }
                else if (process.ProcessName == "samp")
                {
                    process.Kill();
                }
            }
        }

        public static void GTASAMPKAPAT()
        {
            string processName = "gta_sa";
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                if (process.MainWindowTitle.Contains("GTA:SA:MP") || process.MainWindowTitle.Contains("GTA: San Andreas"))
                {
                    process.Kill();
                }
                else if (process.ProcessName == "gta_sa")
                {
                    process.Kill();
                }
                else if (process.MainWindowTitle.Contains("SA-MP 0.3"))
                {
                    process.Kill();
                }
                else if (process.ProcessName == processName)
                {
                    process.Kill();
                }
            }
        }

        public static bool GTACHECK()
        {
            return Process.GetProcessesByName("gta_sa").FirstOrDefault((Process p) => p.MainModule.FileName.StartsWith("")) != null;
        }

        public static bool SAMPCHECK()
        {
            return Process.GetProcessesByName("samp").FirstOrDefault((Process p) => p.MainModule.FileName.StartsWith("")) != null;
        }

        public static string GetRandomSymbols()
        {
            return GetRandomSymbols(8);
        }

        public static string GetRandomSymbols(int count)
        {
            int index = randomSymbolsIndex;
            string result = new string(Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", count).Select(delegate (string s)
            {
                index += random.Next(s.Length);
                if (index >= s.Length)
                {
                    index -= s.Length;
                }
                return s[index];
            }).ToArray<char>());
            randomSymbolsIndex = index;
            return result;
        }

        public static string CPUSeriNoCek()
        {
            string processorID = "";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * FROM WIN32_Processor");
            ManagementObjectCollection mObject = searcher.Get();

            foreach (ManagementObject obj in mObject)
            {
                processorID = obj["ProcessorId"].ToString();
            }

            return processorID;
        }

        public static int InternetVar()
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static string AlMacAdresi()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty) sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
            return sMacAddress.Replace(":", "");
        }

        public static int HACKCHECK()
        {
            if (Globals.dlllist.Length < 5 || Globals.asilist.Length < 5 || Globals.cleolist.Length < 5)
            {
                string sifreleme = "null";
                Random rastgele = new Random();
                int sayi = rastgele.Next(1, 5);
                if (sayi == 1) sifreleme = "R3V3R531BJQYHFBBAW";
                if (sayi == 2) sifreleme = "C0D3MB519PR0T3C711";
                if (sayi == 3) sifreleme = "4DV4NC3DPR0T3C7QSZ";
                if (sayi == 4) sifreleme = "UNR3L14BL3H33XDRWZ";
                try
                {
                    Globals.dlllist = SifreCoz(ReadTextFromUrl(Globals.site + Functions.SifreCoz("-|24--|17--|75--|66--|87--|17--|79--|76--|85--|14--|555--|98--|88--|18--|12-/-|88--|222--|27--|555--|9--|1-==") + Sifrele(sifreleme)));
                    Globals.asilist = SifreCoz(ReadTextFromUrl(Globals.site + Functions.SifreCoz("-|5--|25--|29--|91--|87--|17--|79--|76--|85--|14--|555--|98--|88--|18--|12-/-|88--|222--|27--|555--|9--|1-==") + Sifrele(sifreleme)));
                    Globals.cleolist = SifreCoz(ReadTextFromUrl(Globals.site + Functions.SifreCoz("-|5--|222--|75--|79--|87--|222--|75--|91--|86--|333--|1--|94--|86--|17--|82--|98--|9--|222--|67--|79--|97--|33--|000-=") + Sifrele(sifreleme)));
                    if (Globals.dlllist.Length < 0)
                    {
                        MessageBox.Show("Liste doğrulanamadı. #1", "Ibiza Client");
                        return -1;
                    }
                    if (Globals.asilist.Length < 0)
                    {
                        MessageBox.Show("Liste doğrulanamadı. #2", "Ibiza Client");
                        return -1;
                    }
                    if (Globals.cleolist.Length < 0)
                    {
                        MessageBox.Show("Liste doğrulanamadı. #3", "Ibiza Client");
                        return -1;
                    }
                }
                catch
                {
                    
                }
            }
            string Hileler = "";
            int HileSayi = 0;
            int num = 0;
            List<string> dllicerik = new List<string>();
            string[] dosyalar = Directory.GetFiles(Globals.OyunKonum, "*.dll");
            foreach (string icerik in dosyalar)
            {
                string dosyaIsim = Path.GetFileName(icerik);
                dllicerik.Add(dosyaIsim);
            }

            foreach (string dllkontrol in dllicerik)
            {
                if (File.Exists(Globals.OyunKonum + dllkontrol))
                {
                    try
                    {
                        long filesize = new FileInfo(Globals.OyunKonum + "\\" + dllkontrol).Length;
                        string sonuc = dllkontrol + "|" + filesize.ToString() + "|";
                        if (Globals.dlllist.Contains(sonuc))
                        {
                            Hileler = Hileler + dllkontrol + "\n";
                            HileSayi++;
                        }
                    }
                    catch (Exception arg)
                    {
                        MessageBox.Show("Ibiza sunucularına şu anda ulaşılamıyor. (#1) " + arg, "Ibiza Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(Environment.ExitCode);
                    }
                }
            }
            List<string> asiIcerik = new List<string>();
            string[] asiler = Directory.GetFiles(Globals.OyunKonum, "*.asi");
            foreach (string asiEkle in asiler)
            {
                string dosyaIsim = Path.GetFileName(asiEkle);
                asiIcerik.Add(dosyaIsim);
            }
            foreach (string asiKontrol in asiIcerik)
            {
                if (File.Exists(Globals.OyunKonum + asiKontrol))
                {
                    try
                    {
                        long filesize = new FileInfo(Globals.OyunKonum + "\\" + asiKontrol).Length;
                        Crc32 crc32 = new Crc32();
                        String hash = String.Empty;
                        using (FileStream fs = File.Open(Globals.OyunKonum + "\\" + asiKontrol, FileMode.Open))
                            foreach (byte b in crc32.ComputeHash(fs)) hash += b.ToString("x2").ToLower();

                        string sonuc = asiKontrol + "|" + filesize.ToString() + "|" + hash + "|";
                        if (!Globals.asilist.Contains(sonuc))
                        {
                            Hileler = Hileler + asiKontrol + "\n";
                            HileSayi++;
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ibiza Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(Environment.ExitCode);
                    }
                }
            }
            if (Directory.Exists(Globals.OyunKonum + "\\cleo"))
            {
                string[] fileArray = Directory.GetFiles(Globals.OyunKonum + "\\cleo\\", "*.cs");
                foreach (string file in fileArray)
                {
                    try
                    {
                        string fileName = Path.GetFileName(file);
                        long filesize = new FileInfo(Globals.OyunKonum + "\\cleo\\" + fileName).Length;
                        Crc32 crc32 = new Crc32();
                        String hash = String.Empty;
                        using (FileStream fs = File.Open(Globals.OyunKonum + "\\cleo\\" + fileName, FileMode.Open))
                            foreach (byte b in crc32.ComputeHash(fs)) hash += b.ToString("x2").ToLower();

                        string sonuc = fileName + "|" + filesize.ToString() + "|" + hash + "|";

                        if (!Globals.cleolist.Contains(sonuc))
                        {
                            Hileler = Hileler + "cleo/" + fileName + "\n";
                            HileSayi++;
                        }
                    }
                    catch (Exception arg)
                    {
                        MessageBox.Show("Ibiza sunucularına şu anda ulaşılamıyor. (#3) " + arg, "Ibiza Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(Environment.ExitCode);
                    }
                }
            }
            if (Directory.Exists(Globals.OyunKonum + "\\mod_sa"))
            {
                Hileler = Hileler + "mod_sa\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\RapidFire_Project_Attack"))
            {
                Hileler = Hileler + "RapidFire_Project_Attack\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\nod_sa"))
            {
                Hileler = Hileler + "nod_sa\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\moonloader"))
            {
                Hileler = Hileler + "moonloader\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\EliteModSa"))
            {
                Hileler = Hileler + "EliteModSa\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\Sobreit 0.3DL"))
            {
                Hileler = Hileler + "Sobreit 0.3DL\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\OverLight Custom"))
            {
                Hileler = Hileler + "OverLight Custom\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\OverLight"))
            {
                Hileler = Hileler + "OverLight\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\Sobreit"))
            {
                Hileler = Hileler + "Sobreit\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\Menyoo"))
            {
                Hileler = Hileler + "Menyoo\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\OverLight_Mod"))
            {
                Hileler = Hileler + "OverLight_Mod\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\glance_mod"))
            {
                Hileler = Hileler + "glance_mod\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\RedEclipseMod"))
            {
                Hileler = Hileler + "RedEclipseMod\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\StealingData"))
            {
                Hileler = Hileler + "StealingData\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\BlueEclipseMod"))
            {
                Hileler = Hileler + "BlueEclipseMod\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\CustomSAA2"))
            {
                Hileler = Hileler + "CustomSAA2\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\LiquidMod"))
            {
                Hileler = Hileler + "LiquidMod\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\H3X"))
            {
                Hileler = Hileler + "H3X\n";
                HileSayi++;
            }
            if (Directory.Exists(Globals.OyunKonum + "\\SAMPFUNCS"))
            {
                Hileler = Hileler + "SAMPFUNCS\n";
                HileSayi++;
            }

            string[] iniler = Directory.GetFiles(Globals.OyunKonum, "*.ini*", SearchOption.AllDirectories);
            string[] pngler = Directory.GetFiles(Globals.OyunKonum, "*.png*", SearchOption.AllDirectories);
            foreach (var file in iniler)
            {
                StreamReader sr = new StreamReader(file);
                string ini = sr.ReadToEnd();
                if (ini.Contains("https://github.com/BlastHackNet/mod_s0beit_sa") || ini.Contains("esp_players_defaulton") || ini.Contains("key_anttweakbar_cheatsmenu") || ini.Contains("Hackers_Ping_Location") || ini.Contains("s0beit"))
                {
                    num = 1;
                    MessageBox.Show("s0beit dosyaları tespit edildi!", "Ibiza Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
                else
                {
                    foreach (var file2 in pngler)
                    {
                        if (file2.Contains("speedo") || file2.Contains("needle"))
                        {
                            num = 1;
                            MessageBox.Show("s0beit dosyaları tespit edildi!", "Ibiza Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return -1;
                        }
                    }
                }
            }

            if (Directory.Exists(Globals.OyunKonum + "\\scripts"))
            {
                string[] scriptsasi = Directory.GetFiles(Globals.OyunKonum + "\\scripts", "*.asi*", SearchOption.AllDirectories);
                foreach (var file in scriptsasi)
                {
                    HileSayi++;
                    FileInfo f = new FileInfo(file);
                    string ad = f.FullName;
                    ad = ad.Replace(Globals.OyunKonum, "") + "\n";
                    Hileler = Hileler + ad;
                }
            }

            if (Directory.Exists(Globals.OyunKonum + "\\modloader"))
            {
                string[] modloaderasi = Directory.GetFiles(Globals.OyunKonum + "\\modloader", "*.asi*", SearchOption.AllDirectories);
                string[] modloadercleo = Directory.GetFiles(Globals.OyunKonum + "\\modloader", "*.cs*", SearchOption.AllDirectories);
                string[] modloadercol = Directory.GetFiles(Globals.OyunKonum + "\\modloader", "*.col*", SearchOption.AllDirectories);
                foreach (var file in modloaderasi)
                {
                    FileInfo f = new FileInfo(file);
                    string ad = f.FullName;
                    if (!ad.Contains("std.asi.dll") && !ad.Contains("std.asi.md"))
                    {
                        HileSayi++;
                        ad = ad.Replace(Globals.OyunKonum, "") + "\n";
                        Hileler = Hileler + ad;
                    }
                }
                foreach (var file in modloadercleo)
                {
                    HileSayi++;
                    FileInfo f = new FileInfo(file);
                    string ad = f.FullName;
                    ad = ad.Replace(Globals.OyunKonum, "") + "\n";
                    Hileler = Hileler + ad;
                }
                foreach (var file in modloadercol)
                {
                    HileSayi++;
                    FileInfo f = new FileInfo(file);
                    string ad = f.FullName;
                    ad = ad.Replace(Globals.OyunKonum, "") + "\n";
                    Hileler = Hileler + ad;
                }
            }

            if (File.Exists(Globals.OyunKonum + "\\gta_sa.exe"))
            {
                IEnumerator<string> enumerator = File.ReadLines(Globals.OyunKonum + "\\gta_sa.exe").GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        string cikti = enumerator.Current;
                        if (cikti.Contains("//RenderWare/RW36Active/rwsdk/src/babinfrm.c#1") && !cikti.Contains("\0d3d9.dll\0"))
                        {
                            num = 1;
                            MessageBox.Show("gta_sa.exe dosyasında değişiklik tespit edildi!", "Ibiza Client");
                        }
                    }
                }
                catch
                {
                    
                }
            }
            if (File.Exists(Globals.OyunKonum + "\\vorbisFile.dll"))
            {
                IEnumerator<string> enumerator = File.ReadLines(Globals.OyunKonum + "\\vorbisFile.dll").GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        string cikti = enumerator.Current;
                        if (cikti.Contains("[excludes]") && !cikti.Contains(".asi\0"))
                        {
                            num = 1;
                            MessageBox.Show("vorbisFile.dll içerisinde .asi açığı farkedildi!", "Ibiza Client");
                        }
                    }
                }
                catch
                {

                }
            }
            if(HileSayi >= 1) MessageBox.Show("Lütfen aşağıdaki izinsiz dosyaları silin.\n\n" + Hileler, "Ibiza Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            if(HileSayi >= 1 && num == 0) num = 1;
            return num;
        }
        private static int randomSymbolsIndex = 0;
        private static readonly Random random = new Random();
    }
}
