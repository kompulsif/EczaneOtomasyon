using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace EczaneOtomasyon
{
    public partial class EczaneGiris : Form
    {
        ArrayList degerler = new ArrayList();
        private EczaneAnaSayfasi anaSayfa = new EczaneAnaSayfasi();
        private EczaneSecimi x;
        private veritabani baglanti = new veritabani();
        public EczaneGiris(EczaneSecimi x, string eczaneID, string eczaneAdi, string eczaneAdres, string kullaniciAdi)
        {
            this.x = x;
            anaSayfa.eczaneID = eczaneID;
            anaSayfa.eczaneAdi = eczaneAdi;
            anaSayfa.eczaneAdres = eczaneAdres;
            anaSayfa.kullaniciAdi = kullaniciAdi;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbKullaniciAdi.Text.Trim().Length > 2 && tbParola.Text.Trim().Length > 3)
            {
                DataSet ds = new DataSet();
                using (
                    SqlDataAdapter da = new SqlDataAdapter(
                        $"select parola from Eczaneler where id = '{anaSayfa.eczaneID}' and parola = '{tbParola.Text.Trim()}' and kullaniciAdi = '{tbKullaniciAdi.Text.Trim()}'", baglanti.ac()))
                    da.Fill(ds, "sonuc");
                if (ds.Tables["sonuc"].Rows.Count > 0)
                {
                    baglanti.kapat();
                    anaSayfa.Show();
                    x.Hide();
                    this.Close();
                }
                else { MessageBox.Show("Bilgiler yanlış!"); }
            } else { MessageBox.Show("Kullanıcı Adı en az 3, Parola ise en az 4 karakter olmalıdır!"); }
        }
    }
}
