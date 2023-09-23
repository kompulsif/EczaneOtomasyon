using System;
using System.Drawing;
using System.Windows.Forms;
using QRCoder;

namespace EczaneOtomasyon
{
    public partial class KodResim : Form
    {
        public KodResim()
        {
            InitializeComponent();
        }
        public string kod = "";
        private void KodResim_Load(object sender, EventArgs e)
        {
            QRCodeGenerator qrOlusturucu = new QRCodeGenerator();
            QRCodeData qrVeri = qrOlusturucu.CreateQrCode(kod, QRCodeGenerator.ECCLevel.Q);
            QRCode qrKod = new QRCode(qrVeri);
            Bitmap qrResim = qrKod.GetGraphic(10);
            pictureBox1.Image = qrResim;
        }
    }
}
