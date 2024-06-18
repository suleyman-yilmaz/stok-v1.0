using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stok_v1._0
{
    public static class anlikStok
    {
        public static string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=stok;Integrated Security=True";

        public static DataTable anlikStokListele()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM vw_anlik ORDER BY [BARKOD NO]", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            return dt;
        }
    }
}