using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IbizaClient.Classes;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing.Text;

namespace IbizaClient
{
    public partial class Downloader : Form
    {
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection fonts = new PrivateFontCollection();

        int t_muteobar = 0;
        int t_muteowidth = 0;
        string[] muteoArray;
        string[] muteoTempArray;
        public Downloader()
        {
            InitializeComponent();
            try
            {
                if (!Directory.Exists(Globals.OyunKonum))
                {
                    MessageBox.Show("Oyun klasörünüzün geçersiz olduğu tespit edildi, lütfen düzeltin.", "Ibiza Client");
                    Environment.Exit(0);
                }
                if (Globals.version != Functions.ReadTextFromUrl(Globals.site + "version.php"))
                {
                    Functions.ClientUpdate();
                    return;
                }
                LoadFont();
                SetFonts();
                string sifreleme = "null";
                Random rastgele = new Random();
                int sayi = rastgele.Next(1, 5);
                if (sayi == 1) sifreleme = "R3V3R531BJQYHFBBAW";
                if (sayi == 2) sifreleme = "C0D3MB519PR0T3C711";
                if (sayi == 3) sifreleme = "4DV4NC3DPR0T3C7QSZ";
                if (sayi == 4) sifreleme = "UNR3L14BL3H33XDRWZ";
                Globals.pathlist = Functions.SifreCoz(Functions.ReadTextFromUrl(Globals.site + Functions.SifreCoz("-|86--|17--|16--|000--|88--|17--|75--|91--|86--|333--|1--|94--|86--|17--|82--|98--|9--|222--|67--|79--|97--|33--|000-=") + Functions.Sifrele(sifreleme)));
                muteoArray = Globals.pathlist.Split('|');
                IndirmeKontrol();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void IndirmeKontrol()
        {
            int founded = 0;
            for (int i = 0; i < muteoArray.Length; i++)
            {
                muteoTempArray = muteoArray[i].Split('?');
                if (!File.Exists(Globals.OyunKonum + "\\" + muteoTempArray[0]))
                {
                    founded = 1;
                    DosyaIndirt(Globals.site + "path/" + muteoTempArray[0], Globals.OyunKonum + "\\" + muteoTempArray[0]);
                    break;
                }
                else
                {
                    long filesize = new FileInfo(Globals.OyunKonum + "\\" + muteoTempArray[0]).Length;
                    if (filesize != Convert.ToInt32(muteoTempArray[1]))
                    {
                        founded = 1;
                        DosyaIndirt(Globals.site + "path/" + muteoTempArray[0], Globals.OyunKonum + "\\" + muteoTempArray[0]);
                        break;
                    }
                }
            }
            if (founded == 0)
            {
                timer2.Start();
            }
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
            label1.Font = new Font(fonts.Families[0], 9, FontStyle.Regular);
            label2.Font = new Font(fonts.Families[0], 9, FontStyle.Regular);
        }

        private static Stopwatch stopWatch = new Stopwatch();
        private static long lastBytes;
        private static long currentBytes;

        public void DosyaIndirt(string url, string kyol)
        {
            WebClient indir = new WebClient();
            Uri link = new Uri(url);
            indir.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DosyaIndir);
            indir.DownloadFileCompleted += new AsyncCompletedEventHandler(Indirildi);
            stopWatch.Start();
            indir.DownloadFileAsync(link, kyol);
        }
        
        public void DosyaIndir(object sender, DownloadProgressChangedEventArgs e)
        {
            SetMuteoBar(e.ProgressPercentage);
            currentBytes = lastBytes + e.BytesReceived;
            label1.Text = ComputeDownloadSize((double)e.BytesReceived).ToString("0.00") + "MB / " + ComputeDownloadSize((double)e.TotalBytesToReceive).ToString("0.00") + "MB";
            label2.Text = ComputeDownloadSpeed((double)e.BytesReceived, stopWatch).ToString("0,00") + "kb/sn";
        }

        public static double ComputeDownloadSize(double Size)
        {
            return Size / 1024.0 / 1024.0;
        }

        public static double ComputeDownloadSpeed(double Size, Stopwatch stopWatch)
        {
            return Size / 1024.0 / stopWatch.Elapsed.TotalSeconds;
        }

        private void Indirildi(object sender, AsyncCompletedEventArgs e)
        {
            stopWatch.Reset();
            IndirmeKontrol();
        }
        private void SetMuteoBar(int count)
        {
            t_muteowidth = count * 8 + 40;
            t_muteobar = count;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (panel1.Width < t_muteowidth) panel1.Width += 2;
            else
            {
                panel1.Width = t_muteowidth;
                timer1.Stop();
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
            Environment.Exit(0);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            this.Hide();
            Client client = new Client();
            client.Show();
            client.WindowState = FormWindowState.Minimized;
            client.WindowState = FormWindowState.Normal;
        }
    }
}
