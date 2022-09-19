using Microsoft.Win32;
using System.Collections.Generic;

namespace IbizaClient.Classes
{
    class Globals
    {
        public static string GTAKonum = Registry.CurrentUser.OpenSubKey(@"Software\\SAMP").GetValue("gta_sa_exe").ToString();
        public static string OyunKonum = GTAKonum = GTAKonum.Substring(0, GTAKonum.LastIndexOf(@"\") + 1);
        public static string CalisanKonum = "NONE";
        public static string version = "0.0.2.1";
        public static string ServerIP = "80.93.220.67";
        public static string site = "https://turkibiza.com/iclient/";
        public static string dlllist = "none";
        public static string asilist = "none";
        public static string cleolist = "none";
        public static string pathlist = "none";
        public static string CPU = Functions.CPUSeriNoCek();
        public static string MAC = Functions.AlMacAdresi();
        public static string RAM = Functions.GetPCInformation();
        public static string HDD = Functions.GetHDD();
        public static string loginkey = "NULL";
        public static string RSERIAL = "none";
        public static string IP = Functions.ReadTextFromUrl(Globals.site + "getip.php");
        public static int sampizin = 0;
        public static List<string> pNameList = new List<string>();
        public static List<long> pLengthList = new List<long>();
    }
}
