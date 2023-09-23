using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EczaneOtomasyon
{
    public partial class ParolaDegistir : Form
    {
        private string id;
        public ParolaDegistir(string id)
        {
            this.id = id;
            InitializeComponent();
        }

        private veritabani baglanti = new veritabani();
        private void button1_Click(object sender, EventArgs e)
        {
            if (tbYeniParola.Text.Trim().Length > 3 && tbEskiParola.Text.Trim().Length > 3)
            {
                DataSet ds = new DataSet();
                using (SqlDataAdapter da = new SqlDataAdapter($"select id from Eczaneler where id = '{id}' and parola = '{tbEskiParola.Text.Trim()}'", baglanti.ac()))
                {
                    da.Fill(ds, "sonuc");
                }
                baglanti.kapat();
                if (ds.Tables["sonuc"].Rows.Count > 0)
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("update Eczaneler set parola = @parola where id = @id", baglanti.ac()))
                        {
                            cmd.Parameters.AddWithValue("@parola", tbYeniParola.Text.Trim());
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                        baglanti.kapat();
                        this.Hide();
                        MessageBox.Show("Parolanız başarıyla değiştirildi!");
                    } catch { MessageBox.Show("Parolanız değiştirilemedi!"); }
                }
                else { MessageBox.Show("Parolanız hatalı!"); }
            } else { MessageBox.Show("Parolalar en az 4 karakter içermelidir!"); }
        }
    }
}
