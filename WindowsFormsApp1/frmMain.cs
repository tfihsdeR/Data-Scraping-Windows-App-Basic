using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace WindowsFormsApp1
{
    public partial class frmMain : Form
    {
        HtmlWeb htmlWeb = new HtmlWeb();
        
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnGetContent_Click(object sender, EventArgs e)
        {
            var doc = htmlWeb.Load(txtUri.Text);

            var haber = new List<Haberler>();

            var hblInbox = doc.DocumentNode.SelectNodes("//div[@class='hblnBox']");

            foreach (var inbox in hblInbox)
            {
                try
                {
                    if (inbox.SelectSingleNode("./h2") == null)
                    {
                        string haberZamani = inbox.SelectSingleNode("./div[@class='hblnTime']").InnerText;
                        string haberResim = inbox.SelectSingleNode("./a/div[@class='hblnImage']/img").Attributes["src"].Value;
                        string haberTitle = inbox.SelectSingleNode("./a/span[@class='hblnContent']").InnerText;
                        string haberUri = inbox.SelectSingleNode("./a").Attributes["href"].Value;

                        haber.Add(new Haberler()
                        {
                            Baslik = haberTitle,
                            Time = haberZamani,
                            PictureUri = haberResim,
                            Uri = haberUri
                        });
                    }
                    else
                    {
                        continue;
                    }
                    
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.StackTrace);
                }


                foreach (var item in haber)
                {
                    lstNews.Items.Add(item.Baslik);
                    lstNews.Items.Add(item.Uri);
                    lstNews.Items.Add(item.PictureUri);
                    lstNews.Items.Add(item.Time);
                }

            }

            MessageBox.Show($"Haberler çekildi. Toplam haber sayısı: {haber.Count}");
        }
    }
}