using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace HtmlDownloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            WebClient client = new WebClient();

            try
            {
                byte[] content = client.DownloadData(this.txtUrl.Text);
                this.txtDisplay.Text = Encoding.ASCII.GetString(content);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Cursor = Cursors.Default;
        }

        private void tmrDownloadTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadFile("http://www.contoso.com", "downloadfile.htm");
            }
            catch (Exception ex)
            {
                StreamWriter writer = new StreamWriter("downloadfile.htm");
                writer.Write("The following error occurred. " + ex.ToString());
                writer.Close();
            }
            this.tmrDownloadTimer.Enabled = false;
        }
    }
}
