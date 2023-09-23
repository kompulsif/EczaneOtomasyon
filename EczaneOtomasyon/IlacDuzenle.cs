using System;
using System.Collections;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EczaneOtomasyon
{
    public partial class IlacDuzenle : Form
    {
        public IlacDuzenle()
        {
            InitializeComponent();
        }
        veritabani veribaglantisi = new veritabani();
        public ArrayList turler = new ArrayList();
        public string eczaneID = null;
        public string kod = null;
        ArrayList kodlar = new ArrayList();
        private void IlacDuzenle_Load(object sender, EventArgs e)
        {
            foreach (string t in turler)
            {
                comboBox1.Items.Add(t);
            }
            comboBox1.SelectedIndex = 0;
            try
            {
                using (SqlCommand komut = new SqlCommand("select kodu from Ilaclar", veribaglantisi.ac()))
                {
                    SqlDataReader dr = komut.ExecuteReader();
                    while (dr.Read()) kodlar.Add(dr["kodu"]);
                }
                veribaglantisi.kapat();
                txtKod.Text = kod;
            }
            catch
            {
                MessageBox.Show("Veritabanı işlemleri sırasında bir sorun oluştu!");
                this.Close();
            }
        }

        private void txtKod_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Kod Değiştirilemez");
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtAd.Text.Trim().Length > 1 && txtKullanim.Text.Trim().Length > 4 && comboBox1.SelectedItem.ToString() != "Seçilmedi")
            {
                try
                {
                    using (SqlCommand komut = new SqlCommand("update Ilaclar set adi = @p1, turu = @p2, kullanim = @p3 where eczane = @p4 and kodu = @p5", veribaglantisi.ac()))
                    {
                        komut.Parameters.AddWithValue("@p1", txtAd.Text.Trim());
                        komut.Parameters.AddWithValue("@p2", comboBox1.SelectedItem.ToString());
                        komut.Parameters.AddWithValue("@p3", txtKullanim.Text.Trim());
                        komut.Parameters.AddWithValue("@p4", eczaneID);
                        komut.Parameters.AddWithValue("@p5", txtKod.Text);
                        komut.ExecuteNonQuery();
                        MessageBox.Show("İlaç bilgileri düzenlendi!");
                        this.Close();
                    }
                    veribaglantisi.kapat();
                }
                catch
                {
                    MessageBox.Show("Veritabanı işlemleri sırasında bir sorun oluştu!");
                }
            }
            else { MessageBox.Show("Adı en az 2 haneli\nKullanımı en az 5 haneli olmalı ve ilaç türü seçilmelidir"); }
        }
    }
}
