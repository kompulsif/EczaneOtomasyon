using System;
using System.Collections;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EczaneOtomasyon
{
    public partial class IlacEkle : Form
    {
        public IlacEkle()
        {
            InitializeComponent();
        }
        veritabani veribaglantisi = new veritabani();
        public ArrayList turler = new ArrayList();
        public string eczaneID = null;
        ArrayList kodlar = new ArrayList();
        private void IlacEkle_Load(object sender, EventArgs e)
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
            }
            catch
            {
                MessageBox.Show("Veritabanı işlemleri sırasında bir sorun oluştu!");
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtKod.Text.Trim().Length > 2 && txtAd.Text.Trim().Length > 1 && txtKullanim.Text.Trim().Length > 4 && comboBox1.SelectedItem.ToString() != "Seçilmedi")
            {
                if (!kodlar.Contains(txtKod.Text.Trim()))
                {
                    try
                    {
                        using (SqlCommand komut = new SqlCommand("insert into Ilaclar values (@p1, @p2, @p3, @p4, @p5)", veribaglantisi.ac()))
                        {
                            komut.Parameters.AddWithValue("@p1", txtKod.Text.Trim());
                            komut.Parameters.AddWithValue("@p2", txtAd.Text.Trim());
                            komut.Parameters.AddWithValue("@p3", comboBox1.SelectedItem.ToString());
                            komut.Parameters.AddWithValue("@p4", txtKullanim.Text.Trim());
                            komut.Parameters.AddWithValue("@p5", eczaneID);
                            komut.ExecuteNonQuery();
                            MessageBox.Show("İlaç Eklendi!");
                            this.Close();
                        }
                        veribaglantisi.kapat();
                    }
                    catch
                    {
                        MessageBox.Show("Veritabanı işlemleri sırasında bir sorun oluştu!");
                    }
                } else MessageBox.Show("Bu kod kullanılmış!");
            } else { MessageBox.Show("Kod en az 3 haneli\nAdı en az 2 haneli\nKullanımı en az 5 haneli olmalı ve türü seçilmelidir"); }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
