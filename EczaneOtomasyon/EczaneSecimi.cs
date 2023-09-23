using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace EczaneOtomasyon
{
    public partial class EczaneSecimi : Form
    {
        public EczaneSecimi()
        {
            InitializeComponent();
        }

        veritabani veriBaglantisi = new veritabani();
        DataSet eczaneler = new DataSet();
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process process = Process.GetCurrentProcess();
            try
            {
                process.Kill();
            }
            catch { Application.Exit(); }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("kolon1", "id");
            dataGridView1.Columns.Add("kolon2", "ADI");
            dataGridView1.Columns.Add("kolon3", "TURU");
            dataGridView1.Columns.Add("kolon4", "ADRES");
            dataGridView1.Columns.Add("kolon5", "kAdi");
            dataGridView1.Columns.Add("kolon5", "kParola");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;

            foreach (DataRow satir in eczaneler.Tables["eczaneler"].Rows)
            {
                if (satir["turu"].ToString() == "0")
                {
                    object[] veriler = satir.ItemArray;

                    dataGridView1.Rows.Add(veriler);
                }
            }
        }

        private void EczaneSecimi_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlDataAdapter d = new SqlDataAdapter("select id, adi as 'ADI', turu, adres as 'ADRES', kullaniciAdi, parola from Eczaneler", veriBaglantisi.ac()))
                {
                    d.Fill(eczaneler, "eczaneler");                    
                }
                veriBaglantisi.kapat();
            } catch {
                MessageBox.Show("Veritabani baglantisi kurulamadı!");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = eczaneler.Tables["eczaneler"];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string eczaneID = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            string eczaneAdi = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            string eczaneAdres = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            string kullaniciAdi = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            veriBaglantisi.kapat();
            EczaneGiris giris = new EczaneGiris(this, eczaneID, eczaneAdi, eczaneAdres, kullaniciAdi);
            giris.ShowDialog();
        }
    }
}
