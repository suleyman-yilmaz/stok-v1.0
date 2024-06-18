using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace stok_v1._0
{
    public static class satisEkrani
    {
        public static string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=stok;Integrated Security=True";

        public static DataTable satisEkraniListele()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM vw_sepet ORDER BY [BARKOD NO]", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu" + ex);
            }
            return dt;
        }

        public static void seUrunEkle(string barkodNo, string satisAdet, string satisFiyat, string toplamTutar, string cikisTarihi, string aciklama)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                decimal satisFiyatDecimal = decimal.Parse(satisFiyat);
                decimal toplamTutarDecimal = decimal.Parse(toplamTutar);

                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_seUrunEkle", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@barkodNo", barkodNo);
                cmd.Parameters.AddWithValue("@satisAdet", satisAdet);
                cmd.Parameters.AddWithValue("@satisFiyat", satisFiyatDecimal);
                cmd.Parameters.AddWithValue("@toplamTutar", toplamTutarDecimal);
                cmd.Parameters.AddWithValue("@cikisTarihi", cikisTarihi);
                cmd.Parameters.AddWithValue("@aciklama", aciklama);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Ürün başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public static void seUrunSil(string ID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_seUrunSil", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", ID);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Ürün başarıyla silindi.");
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }

        public static void seSatisIptal()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_seSatisIptal", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Ürünler başarıyla silindi.");
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }
    }
}