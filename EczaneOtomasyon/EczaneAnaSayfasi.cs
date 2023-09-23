using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EczaneOtomasyon
{
    public partial class EczaneAnaSayfasi : Form
    {
        public EczaneAnaSayfasi()
        {
            InitializeComponent();

        }
        veritabani veribaglantisi = new veritabani();
        public string eczaneID;
        public string eczaneAdi;
        public string eczaneAdres;
        public string kullaniciAdi;
        private void EczaneAnaSayfasi_Load(object sender, EventArgs e)
        {
            textBox1.Text = eczaneAdi;
            textBox2.Text = eczaneAdres;
            try
            {
                using (SqlCommand komut = new SqlCommand("select turu from IlacTurleri", veribaglantisi.ac()))
                {
                    SqlDataReader dr = komut.ExecuteReader();
                    while (dr.Read()) comboBox1.Items.Add(dr["turu"]);
                }
                veribaglantisi.kapat();

                using (SqlDataAdapter komut = new SqlDataAdapter(
                    $"select kodu as 'KODU', adi as 'ADI', turu as 'TÜRÜ', kullanim as 'KULLANIMI' from Ilaclar where eczane = {eczaneID}", veribaglantisi.ac()))
                {
                    DataSet ilaclar = new DataSet();
                    komut.Fill(ilaclar, "ilaclar");
                    dataGridView1.DataSource = ilaclar.Tables["ilaclar"];
                }
                veribaglantisi.kapat();
                comboBox1.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("Veritabanı işlemleri sırasında bir sorun oluştu!");
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtiAd.Text = "";
            txtKod.Text = "";
            txtKullanim.Text = "";
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ArrayList kosullar = new ArrayList();
            if (txtiAd.Text.Trim().Length > 0) kosullar.Add($"adi like N'%{txtiAd.Text.Trim()}%'");
            if (txtKod.Text.Trim().Length > 0) kosullar.Add($"kodu = N'{txtKod.Text.Trim()}'");
            if (txtKullanim.Text.Trim().Length > 0) kosullar.Add($"kullanim like N'%{txtKullanim.Text.Trim()}%'");
            if (comboBox1.SelectedItem.ToString() != "Seçilmedi") kosullar.Add($"turu = '{comboBox1.SelectedItem.ToString()}'");
            string kosul = string.Join(" and ", kosullar.ToArray()).Trim();
            kosul = (kosul == "") ? "1=1" : kosul;
            try
            {
                using (SqlDataAdapter komut = new SqlDataAdapter(
                    $"select kodu as 'KODU', adi as 'ADI', turu as 'TÜRÜ', kullanim as 'KULLANIMI' from Ilaclar where eczane = {eczaneID} and {kosul}", veribaglantisi.ac()))
                {
                    DataSet ilaclar = new DataSet();
                    komut.Fill(ilaclar, "ilaclar");
                    dataGridView1.DataSource = ilaclar.Tables["ilaclar"];
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
            IlacEkle eklemePaneli = new IlacEkle();
            foreach (string turcb in comboBox1.Items)
            {
                eklemePaneli.turler.Add(turcb);
            }
            eklemePaneli.eczaneID = this.eczaneID;
            eklemePaneli.ShowDialog();
        }

        private void EczaneAnaSayfasi_FormClosing(object sender, FormClosingEventArgs e)
        {
            veribaglantisi.kapat();
            EczaneSecimi eczaneSecim = new EczaneSecimi();
            eczaneSecim.Show();
        }

        private void ilaciSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    string kodu = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    using (SqlCommand komut = new SqlCommand($"delete from Ilaclar where eczane = @p1 and kodu = @p2", veribaglantisi.ac()))
                    {
                        komut.Parameters.AddWithValue("@p1", eczaneID);
                        komut.Parameters.AddWithValue("@p2", kodu);
                        komut.ExecuteNonQuery();
                        MessageBox.Show("İlaç eczanenizden kaldırıldı!");
                        dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                    }
                    veribaglantisi.kapat();
                }
                catch
                {
                    MessageBox.Show("Bir sorun oluştu\nİlaç silinemedi!");
                }
            }
            else { MessageBox.Show("Henüz bir ilaç seçmediniz"); }
        }

        private void ilaciDuzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                IlacDuzenle duzenlemeFormu = new IlacDuzenle();
                foreach (string turcb in comboBox1.Items)
                {
                    duzenlemeFormu.turler.Add(turcb);
                }
                duzenlemeFormu.kod = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                duzenlemeFormu.eczaneID = this.eczaneID;
                duzenlemeFormu.ShowDialog();
            }
            else { MessageBox.Show("Henüz bir ilaç seçmediniz"); }
        }

        private void qRKoduGosterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                KodResim kr = new KodResim();
                kr.kod = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                kr.ShowDialog();
            }
            else { MessageBox.Show("Henüz bir ilaç seçmediniz"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EczaneDurum durumDuzenle = new EczaneDurum();
            durumDuzenle.eczaneID = eczaneID;
            durumDuzenle.ShowDialog();
            if (durumDuzenle.cikisYap) { this.Close(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ParolaDegistir pdegisim = new ParolaDegistir(eczaneID);
            pdegisim.ShowDialog();
        }
    }
}
