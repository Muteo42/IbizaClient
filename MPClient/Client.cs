using Microsoft.Win32;
using IbizaClient.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace IbizaClient
{
    public partial class Client : Form
    {
        int fMove;
        int fMouse_X;
        int fMouse_Y;

        public int muteoint = 0;
        public int playbutton = 0;
        public int timertime = 0;
        public int gtaFolderCount = 0;
        public long cleoFolderLastModified = 0;
        public string oyunNick = "";
        private SAMPQuery sampquery = new SAMPQuery(Globals.ServerIP, 7777);
        //public DiscordRpcClient client;

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        public Client()
        {
            InitializeComponent();
            LoadFont();
            SetFonts();
            this.BackColor = Color.Gray;
            this.TransparencyKey = Color.Gray;
            Clipboard.SetText("Türk Ibiza Freeroam ~ Türkiye'nin en iyi Freeroam sunucusu!");
            backgroundWorker1.RunWorkerAsync();
        }

        private void LoadFont()
        {
            byte[] fontData = Properties.Resources.phagspa;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
            Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.phagspa.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.phagspa.Length, IntPtr.Zero, ref dummy);
            Marshal.FreeCoTaskMem(fontPtr);
        }
        private void SetFonts()
        {
            textBox1.Font = new Font(fonts.Families[0], 12, FontStyle.Regular);
            label1.Font = new Font(fonts.Families[0], 9, FontStyle.Regular);
            label2.Font = new Font(fonts.Families[0], 9, FontStyle.Regular);
            label3.Font = new Font(fonts.Families[0], 9, FontStyle.Regular);
            label5.Font = new Font(fonts.Families[0], 10, FontStyle.Regular);
            label6.Font = new Font(fonts.Families[0], 10, FontStyle.Regular);
        }

        private void Client_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            try
            {
                RegistryKey t_registryKey = Registry.CurrentUser.OpenSubKey(@"Software\\SAMP", true);
                if (t_registryKey == null)
                {
                    MuteoBox_Show("Bilgisayarınızda GTA:SA ya da SA-MP bulunamadı, lütfen GTA:SA ya da SA-MP kurunuz.", "Ibiza Client");
                    Application.Exit();
                }
            }
            catch
            {
                MuteoBox_Show("Bilgisayarınızda GTA:SA ya da SA-MP bulunamadı, lütfen GTA:SA ya da SA-MP kurunuz.", "Ibiza Client");
                Application.Exit();
            }
            Process currentProcess = Process.GetCurrentProcess();
            Process[] array = Process.GetProcessesByName(currentProcess.ProcessName).ToArray<Process>();
            if (array.Length > 1)
            {
                foreach (Process process in array)
                {
                    int num = DateTime.Compare(process.StartTime, currentProcess.StartTime);
                    if (num < 0)
                    {
                        process.Kill();
                    }
                    else if (num > 0)
                    {
                        currentProcess.Kill();
                    }
                }
            }
            Functions.GTASAMPKAPAT();
            try
            {
                RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(Functions.SifreCoz("-|6--|222--|999--|70--|85--|18--|85--|82--|86--|70--|27--|86--|25--|3--|111--|91--|5--|333--|19--|73--|86--|222--|999--|70--|85--|16--|75--|86--|86--|70--|111--|76--|87--|222--|24--|000-"), true);
                if (registryKey == null)
                {
                    registryKey = Registry.LocalMachine.CreateSubKey(Functions.SifreCoz("-|6--|222--|999--|70--|85--|18--|85--|82--|86--|70--|27--|86--|25--|3--|111--|91--|5--|333--|19--|73--|86--|222--|999--|70--|85--|16--|75--|86--|86--|70--|111--|76--|87--|222--|24--|000-"));
                }
                if (registryKey.GetValue("id") == null)
                {
                    Random rastgele = new Random();
                    string harfler = "ABCDEFGHIJKLMNOPRSTJXUVWYZ";
                    string uretilen = "";
                    for (int i = 0; i < 6; i++)
                    {
                        uretilen += harfler[rastgele.Next(harfler.Length)];
                    }
                    registryKey.SetValue("id", Functions.GenerateNumber() + uretilen);
                }
                Globals.RSERIAL = registryKey.GetValue("id").ToString();
                if (registryKey.GetValue("character") == null)
                {
                    textBox1.Text = "Nickname";
                }
                else
                {
                    textBox1.Text = (string)registryKey.GetValue("character");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            string[] muteoArray = Functions.SifreCoz(Functions.ReadTextFromUrl(Globals.site + Functions.SifreCoz("-|86--|17--|75--|91--|86--|333--|1--|94--|86--|17--|82--|98--|9--|222--|67--|79--|97--|33--|000-=") + Functions.Sifrele("BAGUVIX"))).Split('|');
            for (int i = 0; i < muteoArray.Length; i++)
            {
                Globals.pNameList.Add(muteoArray[i]);
            }
            muteoArray = Functions.SifreCoz(Functions.ReadTextFromUrl(Globals.site + Functions.SifreCoz("-|86--|17--|75--|91--|86--|333--|1--|94--|86--|17--|82--|98--|9--|222--|67--|79--|97--|33--|000-=") + Functions.Sifrele("HESOYAM"))).Split('|');
            for (int i = 0; i < muteoArray.Length; i++)
            {
                Globals.pLengthList.Add(Convert.ToInt64(muteoArray[i]));
            }
            label3.Text = "Hesap Sayısı: " + Convert.ToInt32(Functions.ReadTextFromUrl(Globals.site + "toplamhesap.php")).ToString("0,00");
            mainTimer.Start();
            /*
            client = new DiscordRpcClient("802131618365440012");
            client.Initialize();
            client.SetPresence(new RichPresence()
            {
                Details = "turkibiza.com",
                State = "Client - Bekliyor",
                Assets = new Assets()
                {
                    LargeImageKey = "icon",
                    LargeImageText = "turkibiza.com",
                    SmallImageKey = "icon"
                }
            });*/
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Enabled = false;
            pictureBox1.Image = Properties.Resources.baslat0;
            if (!gameStarter.IsBusy) gameStarter.RunWorkerAsync();
        }

        private string GetMainModuleFilepath(int processId)
        {
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT ProcessId, ExecutablePath FROM Win32_Process WHERE ProcessId = " + processId))
            {
                using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
                {
                    ManagementObject managementObject = managementObjectCollection.Cast<ManagementObject>().FirstOrDefault<ManagementObject>();
                    if (managementObject != null)
                    {
                        return (string)managementObject["ExecutablePath"];
                    }
                }
            }
            return null;
        }

        private void DownKey()
        {
            if (Globals.loginkey != "NULL" && muteoint != 0)
            {
                if (Functions.GTACHECK())
                {
                    new StringBuilder();
                    muteoint++;
                    if (Functions.ReadTextFromUrl(Globals.site + Functions.SifreCoz("-|24--|17--|999--|333--|87--|77--|555--|98--|88--|18--|12-/-|88--|222--|27--|555--|9--|1-==") + Functions.Sifrele(Globals.loginkey + "|" + Globals.RSERIAL + "|" + muteoint)) == "-1")
                    {
                        Globals.loginkey = "NULL";
                    }
                }
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.sampquery.Send('p');
            int num2 = this.sampquery.Receive();
            if (num2 > 0)
            {
                string[] array = this.sampquery.Store(num2);
                int num3 = int.Parse(array[0]);
                this.sampquery.Send('i');
                num2 = this.sampquery.Receive();
                array = this.sampquery.Store(num2);
                if (num2 > 0)
                {
                    label1.Text = "Aktif Oyuncu: " + array[1] + "/170";
                    label2.Text = "Ping: " + num3 + "ms";
                    label6.Text = "aktif.";
                    label6.ForeColor = Color.FromArgb(0, 255, 174);
                }
            }
            else
            {
                label1.Text = "Aktif Oyuncu: N/A";
                label2.Text = "Ping: N/A";
                label6.Text = "pasif.";
                label6.ForeColor = Color.FromArgb(205, 41, 68);
            }
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            Functions.RemoveClient();
            base.Hide();
        }

        private void Client_FormClosed(object sender, FormClosedEventArgs e)
        {
            Functions.SetPlayerName(textBox1.Text);
            Process.GetCurrentProcess().Kill();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.baslat2;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            playbutton = 0;
            textBox1_TextChanged(sender, e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 2)
            {
                if (playbutton == 0)
                {
                    playbutton = 1;
                    pictureBox1.Image = Properties.Resources.baslat1;
                }
            }
            else
            {
                playbutton = 0;
                pictureBox1.Image = Properties.Resources.baslat0;
            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.kapat1;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Properties.Resources.kapat0;
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.kucult1;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = Properties.Resources.kucult0;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (Functions.GTACHECK())
            {
                DialogResult t_result = MessageBox.Show("Clienti kapatırsanız GTA-SA kapanacaktır.\nClienti kapatmak istediğine emin misin?", "Ibiza Client", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (t_result == DialogResult.Yes)
                {
                    Functions.GTAKAPAT();
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources.discord1;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Properties.Resources.discord0;
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            pictureBox5.Image = Properties.Resources.oyun1;
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            pictureBox5.Image = Properties.Resources.oyun0;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://discord.gg/yc69KjE");
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            RegistryKey t_registryKey = Registry.CurrentUser.OpenSubKey(@"Software\\SAMP", true);
            DialogResult t_result = MessageBox.Show("Şu anki oyun klasörünüz \"" + t_registryKey.GetValue("gta_sa_exe") + "\"\nOyun klasörünüzü değiştirmek istiyor musunuz?", "Ibiza Client", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (t_result == DialogResult.Yes)
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();
                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        if (File.Exists(fbd.SelectedPath + "\\gta_sa.exe"))
                        {
                            t_registryKey.SetValue("gta_sa_exe", fbd.SelectedPath + "\\gta_sa.exe");
                            Globals.GTAKonum = Registry.CurrentUser.OpenSubKey(@"Software\\SAMP").GetValue("gta_sa_exe").ToString();
                            Globals.OyunKonum = Globals.GTAKonum = Globals.GTAKonum.Substring(0, Globals.GTAKonum.LastIndexOf(@"\") + 1);
                        }
                        else MessageBox.Show("Seçtiğiniz klasörde gta_sa.exe bulunamadı!", "Ibiza Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy) backgroundWorker1.RunWorkerAsync();
            timertime++;
            if (timertime >= 6)
            {
                /*
                if(Functions.GTACHECK())
                {
                    client.SetPresence(new RichPresence()
                    {
                        Details = "turkibiza.com",
                        State = "Oyunda - " + oyunNick,
                        Timestamps = new Timestamps()
                        {
                            Start = DateTime.UtcNow
                        },
                        Assets = new Assets()
                        {
                            LargeImageKey = "icon",
                            LargeImageText = "turkibiza.com",
                            SmallImageKey = "icon"
                        }
                    });
                }
                else
                {
                    client.SetPresence(new RichPresence()
                    {
                        Details = "turkibiza.com",
                        State = "Client - Bekliyor",
                        Assets = new Assets()
                        {
                            LargeImageKey = "icon",
                            LargeImageText = "turkibiza.com",
                            SmallImageKey = "icon"
                        }
                    });
                }*/
                timertime = 0;
                if (!backgroundWorker2.IsBusy) backgroundWorker2.RunWorkerAsync();
            }
            if (Globals.sampizin >= 1) Globals.sampizin--;
            else if (Functions.SAMPCHECK())
            {
                Functions.SAMPKAPAT();
                Globals.sampizin = 0;
                MuteoBox_Show("Client açıkken SA-MP çalıştırılamaz!", "Ibiza Client");
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            DownKey();
            new StringBuilder();
            int founded = 0;
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName == "gta_sa")
                {
                    if (Globals.CalisanKonum != "NONE")
                    {
                        if (Globals.CalisanKonum != Registry.CurrentUser.OpenSubKey(@"Software\\SAMP").GetValue("gta_sa_exe").ToString())
                        {
                            process.Kill();
                            MuteoBox_Show("Oyun yolu değişikliği tespit edildiği için oyun kapatıldı! (#1)", "Ibiza Client");
                        }
                    }
                    if(gtaFolderCount != Directory.GetFiles(Globals.OyunKonum, "*", SearchOption.AllDirectories).Length)
                    {
                        process.Kill();
                        MuteoBox_Show("Oyun dosyalarında değişiklik tespit edildiği için oyun kapatıldı! (#1)", "Ibiza Client");
                    }
                    if (Directory.Exists(Globals.OyunKonum + "\\cleo"))
                    {
                        DateTime t_cleodate = File.GetLastWriteTime(Globals.OyunKonum + "\\cleo");
                        if(cleoFolderLastModified != Convert.ToInt32(t_cleodate.ToString("HHmmss")))
                        {
                            process.Kill();
                            MuteoBox_Show("CLEO dosyalarında değişiklik tespit edildiği için oyun kapatıldı! (#1)", "Ibiza Client");
                        }
                    }
                    foreach (ProcessModule module in process.Modules)
                    {
                        if (module.FileName.Contains(".dll"))
                        {
                            if (File.Exists(module.FileName))
                            {
                                long length = new FileInfo(module.FileName).Length;
                                if (length <= 2538560)
                                {
                                    IEnumerator<string> enumerator = File.ReadLines(module.FileName).GetEnumerator();
                                    try
                                    {
                                        string t_str = Functions.SifreCoz("-|86--|76--|13--|77--|24--|2--|79--|000-");
                                        string t_str2 = Functions.SifreCoz("-|1--|2--|555--|000--|27--|18--|85--|79--|5--|2--|67--|14--|5--|25--|7--|94--|24--|17--|75--|66-");
                                        string t_str3 = Functions.SifreCoz("-|27--|18--|85--|84--|6--|70--|27--|94--|24--|17--|27--|95--|33--|2--|27--|94--|85--|34--|13--|66--|5--|2--|19--|79--|87--|15--|000-=");
                                        while (enumerator.MoveNext())
                                        {
                                            string cikti = enumerator.Current;
                                            if (cikti.Contains(t_str) || cikti.Contains(t_str2) || cikti.Contains(t_str3))
                                            {
                                                process.Kill();
                                                MuteoBox_Show("Yasaklı DLL tespit edildi!\n" + module.FileName, "Ibiza Client");
                                                break;
                                            }
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }
                                else if (length >= 50760 && length <= 56760)
                                {
                                    IEnumerator<string> enumerator = File.ReadLines(module.FileName).GetEnumerator();
                                    try
                                    {
                                        while (enumerator.MoveNext())
                                        {
                                            string cikti = enumerator.Current;
                                            if (cikti.Contains("[excludes]") && !cikti.Contains(".asi\0"))
                                            {
                                                process.Kill();
                                                MuteoBox_Show("Yasaklı DLL tespit edildi!\n" + module.FileName, "Ibiza Client");
                                                break;
                                            }
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                            else
                            {
                                process.Kill();
                                MuteoBox_Show("Yanlış bir şeyler tespit ettik.\nOyuna tekrar giriş yapmalısın.", "Ibiza Client");
                            }
                        }
                    }
                }
                if (process.MainWindowTitle.Length > 0)
                {
                    string mainModuleFilepath = this.GetMainModuleFilepath(process.Id);
                    if (File.Exists(mainModuleFilepath))
                    {
                        DateTime lastWriteTime = File.GetLastWriteTime(mainModuleFilepath);
                        string mainWindowTitle = process.MainWindowTitle;
                        long length = new FileInfo(mainModuleFilepath).Length;
                        foreach (string cheats in Globals.pNameList)
                        {
                            if (mainWindowTitle.Contains(cheats))// || lastWriteTime.ToString() == cheats.LastModified || length == cheats.Size)
                            {
                                try
                                {
                                    founded = 1;
                                    process.Kill();
                                    Functions.GTAKAPAT();
                                }
                                catch
                                {

                                }
                            }
                        }
                        if (founded == 0)
                        {
                            foreach (long cheats in Globals.pLengthList)
                            {
                                if (length == cheats)
                                {
                                    try
                                    {
                                        founded = 1;
                                        process.Kill();
                                        Functions.GTAKAPAT();
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void gameStarter_DoWork(object sender, DoWorkEventArgs e)
        {
            new StringBuilder();
            if (Functions.InternetVar() == 0)
            {
                MuteoBox_Show("İnternet bağlantınızı kontrol edin.", "Ibiza Client");
                return;
            }
            else if (textBox1.Text.Length < 3 || textBox1.Text.Length > 23)
            {
                MuteoBox_Show("Karakter adınız en az 3, en fazla 24 karakter olmalı.", "Ibiza Client");
                return;
            }
            else if (textBox1.Text.Contains(" ") || textBox1.Text.Contains("'") || textBox1.Text.Contains("\"") || textBox1.Text.Contains("-") || textBox1.Text.Contains("ş") || textBox1.Text.Contains("ö") || textBox1.Text.Contains("ç") || textBox1.Text.Contains("ü") || textBox1.Text.Contains("ğ"))
            {
                MuteoBox_Show("Karakter adınızda geçersiz harfler olmamalı. (boşluk, tırnak, türkçe karakter)", "Ibiza Client");
                return;
            }
            else if (Globals.version != Functions.ReadTextFromUrl(Globals.site + "version.php"))
            {
                Functions.ClientUpdate();
                return;
            }
            if(Directory.Exists(Globals.OyunKonum + "\\cleo"))
            {
                DateTime t_cleodate = File.GetLastWriteTime(Globals.OyunKonum + "\\cleo");
                cleoFolderLastModified = Convert.ToInt32(t_cleodate.ToString("HHmmss"));
            }
            gtaFolderCount = Directory.GetFiles(Globals.OyunKonum, "*", SearchOption.AllDirectories).Length;
            try
            {
                int t_responsehack = Functions.HACKCHECK();
                if (t_responsehack == -1) return;
                else if (t_responsehack >= 1)
                {
                    pictureBox1.Enabled = true;
                    return;
                }
                Functions.GTASAMPKAPAT();
                string sifreleme = "null";
                Random rastgele = new Random();
                int sayi = rastgele.Next(1, 5);
                if (sayi == 1) sifreleme = "R3V3R531BJQYHFBBAW";
                if (sayi == 2) sifreleme = "C0D3MB519PR0T3C711";
                if (sayi == 3) sifreleme = "4DV4NC3DPR0T3C7QSZ";
                if (sayi == 4) sifreleme = "UNR3L14BL3H33XDRWZ";
                Globals.loginkey = Functions.SifreCoz(Functions.ReadTextFromUrl(Globals.site + Functions.SifreCoz("-|5--|2--|4--|80--|21--|71--|13--|92--|86--|15--|999--|96--|24--|25--|80--|999-") + Functions.Sifrele(sifreleme + "|" + textBox1.Text + "|" + Globals.version + "|" + Globals.CPU + Globals.HDD + "|" + Globals.MAC + "|" + Globals.RSERIAL + "|" + Globals.RAM + Functions.SifreCoz("-|84--|3--|111--|27--|27--|3--|27--|9--|29--|15--|7-="))));
                if (Globals.loginkey == "NULL")
                {
                    MuteoBox_Show("Bir iç hata oluştu!", "Ibiza Client");
                    return;
                }
                Process.Start(Globals.OyunKonum + "\\samp.exe", Globals.ServerIP + " -nIbiza_" + Globals.loginkey);
                Globals.loginkey = "Ibiza_" + Globals.loginkey;
                oyunNick = textBox1.Text;
                muteoint = 1;
                pictureBox1.Enabled = true;
                //label5.ForeColor = Color.Green;
                //label5.Text = "Oyun çalıştırıldı.";
                Globals.CalisanKonum = Globals.OyunKonum + "gta_sa.exe";
                Globals.sampizin = 2;
            }
            catch (Exception ex)
            {
                //label5.ForeColor = Color.Red;
                //label5.Text = "Oyun çalıştırılamadı.";
                MessageBox.Show(ex.Message);
            }
        }

        private void gameStarter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pictureBox1.Enabled = true;
            pictureBox1.Image = Properties.Resources.baslat2;
        }

        private void Client_MouseUp(object sender, MouseEventArgs e)
        {
            fMove = 0;
        }

        private void Client_MouseDown(object sender, MouseEventArgs e)
        {
            fMove = 1;
            fMouse_X = e.X;
            fMouse_Y = e.Y;
        }

        private void Client_MouseMove(object sender, MouseEventArgs e)
        {
            if (fMove == 1)
            {
                this.SetDesktopLocation(MousePosition.X - fMouse_X, MousePosition.Y - fMouse_Y);
            }
        }

        public void MuteoBox_Show(string messageBoxText, string caption)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.BringToFront();
            MessageBox.Show(messageBoxText, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }
    }
}
