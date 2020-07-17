using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyStockSystem.SubItems
{
    public partial class DownloadImgForm : MetroForm
    {
        public string ParentUrl { get; set; }
        //public string WhereImage; // 1
        WebClient client;

        public DownloadImgForm()
        {
            InitializeComponent();
        }

        private void DownloadImgForm_Load(object sender, EventArgs e)
        {
            //LbImageUrl.Text = ParentUrl;
            LbImageUrl.Visible = false;

            client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            //PrgDownload.Value = e.ProgressPercentage; // 3

            Invoke(new MethodInvoker(delegate ()
            {
                PrgDownload.Value = e.ProgressPercentage;

                if(e.BytesReceived == e.TotalBytesToReceive) // 다 받으면
                {
                    Client_DownloadFileCompleted(sender, null);
                }
            }));
            
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //ImgDownload.Image = Image.FromFile(WhereImage); // 4
            
            string fileName = ParentUrl.Substring(ParentUrl.IndexOf('=') + 1);
            ImgDownload.Image = Image.FromFile(Environment.CurrentDirectory + $@"\{fileName}");
            ImgDownload.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DownloadImgForm_Shown(object sender, EventArgs e)
        {
            
            Thread thread = new Thread(new ThreadStart(StartDownload));
            thread.Start();
        }

        private void StartDownload()
        {
            Uri uri = new Uri(ParentUrl);
            string fileName = ParentUrl.Substring(ParentUrl.IndexOf('=') + 1);
            client.DownloadFileAsync(uri, Environment.CurrentDirectory + $@"\{fileName}");
            //WhereImage = Environment.CurrentDirectory + $@"\{fileName}"; // 2
        }
    }
}
