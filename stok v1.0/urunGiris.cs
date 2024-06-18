using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace stok_v1._0
{
    public static class urunGiris
    {
        public static string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=stok;Integrated Security=True";

        public static DataTable urunGirisiListele()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM vw_urunGirisi ORDER BY [BARKOD NO]", conn);
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

        public static void ugUrunEkle(string barkodNo, string girenMiktar, string alisFiyati, string toplamTutar, string girisTarihi, string firma)
        {
            try
            {
                decimal alisFiyatiDecimal = decimal.Parse(alisFiyati);
                decimal toplamTutarDecimal = decimal.Parse(toplamTutar);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ugUrunEkle", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@barkodno", barkodNo);
                    cmd.Parameters.AddWithValue("@girenMiktar", girenMiktar);
                    cmd.Parameters.AddWithValue("@alisFiyati", alisFiyatiDecimal);
                    cmd.Parameters.AddWithValue("@toplamTutar", toplamTutarDecimal);
                    cmd.Parameters.AddWithValue("@girisTarihi", girisTarihi);
                    cmd.Parameters.AddWithValue("@firma", firma);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Ürün başarıyla eklendi.");
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }

        public static void ugUrunSil(string ID)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ugUrunSil", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@girenid", ID);
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

        public static void ugUrunDuzenle(string id, string girenMiktar, string alisFiyati, string toplamTutar, string girisTarihi, string Firma)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("sp_ugUrunDuzenle", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
                cmd.Parameters.AddWithValue("@girenMiktar", Convert.ToInt32(girenMiktar));
                cmd.Parameters.AddWithValue("@alisFiyati", Convert.ToDecimal(alisFiyati));
                cmd.Parameters.AddWithValue("@toplamTutar", Convert.ToDecimal(toplamTutar));
                cmd.Parameters.AddWithValue("@girisTarihi", girisTarihi);
                cmd.Parameters.AddWithValue("@firma", Firma);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Ürün başarıyla düzenlendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Ürün düzenlenirken bir hata oluştu veya güncellenecek bir ürün bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

    }
}