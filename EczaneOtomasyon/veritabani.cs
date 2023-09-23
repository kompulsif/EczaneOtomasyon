using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EczaneOtomasyon
{
    class veritabani
    {
        SqlConnection b;
        public SqlConnection ac()
        {
            b = new SqlConnection("Data Source=localhost;Initial Catalog=Eczane;Integrated Security=True");
            b.Open();
            return b;
        }
        public void kapat()
        {
            try
            {
                b.Close();
            }
            catch { }
        }
    }
}
