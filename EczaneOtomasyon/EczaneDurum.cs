using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EczaneOtomasyon
{
    public partial class EczaneDurum : Form
    {
        public EczaneDurum()
        {
            InitializeComponent();
        }

        veritabani veribaglantisi = new veritabani();
        public string eczaneID;
        public bool cikisYap = false;
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void EczaneDurum_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (checkBox1.Checked)
            {
                try
                {
                    using (SqlCommand komut = new SqlCommand("delete from Ilaclar where eczane = @p1", veribaglantisi.ac()))
                    {
                        komut.Parameters.AddWithValue("@p1", eczaneID);
                        komut.ExecuteNonQuery();
                    }
                    veribaglantisi.kapat();
                    using (SqlCommand komut = new SqlCommand("delete from Eczaneler where id = @p1", veribaglantisi.ac()))
                    {
                        komut.Parameters.AddWithValue("@p1", eczaneID);
                        komut.ExecuteNonQuery();
                    }
                    veribaglantisi.kapat();
                    MessageBox.Show("Eczane ve ilaçlar silindi!\nÇıkış Yapılıyor!");
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Veritabanı işlemleri sırasında bir sorun oluştu!");
                }
            } else
            {
                try
                {
                    using (SqlCommand komut = new SqlCommand("update Eczaneler set turu = @p1 where id = @p2", veribaglantisi.ac()))
                    {
                        komut.Parameters.AddWithValue("@p1", comboBox1.SelectedIndex.ToString());
                        komut.Parameters.AddWithValue("@p2", eczaneID);
                        komut.ExecuteNonQuery();
                    }
                    MessageBox.Show("Durum değiştirildi!");
                    veribaglantisi.kapat();
                }
                catch
                {
                    MessageBox.Show("Veritabanı işlemleri sırasında bir sorun oluştu!");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) { comboBox1.Enabled = false; }
            else { comboBox1.Enabled = true; }
        }

        private void EczaneDurum_FormClosing(object sender, FormClosingEventArgs e)
        {
            veribaglantisi.kapat();
            if (checkBox1.Checked) cikisYap = true;
        }
    }
}
