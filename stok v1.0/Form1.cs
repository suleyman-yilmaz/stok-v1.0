using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Excel = Microsoft.Office.Interop.Excel;

namespace stok_v1._0
{
    public partial class Form1 : Form
    {
        string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=stok;Integrated Security=True";
        DataSet table = new DataSet();

        public Form1()
        {
            InitializeComponent();
            DataTable dt = anlikStok.anlikStokListele();
            grdAnlik.DataSource = dt;
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                DataTable dt = anlikStok.anlikStokListele();
                grdAnlik.DataSource = dt;
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                DataTable ugDt = urunGiris.urunGirisiListele();
                grdUrunGirisi.DataSource = ugDt;
                grdUrunGirisi.Columns[0].Visible = false;
            }
            else if (tabControl1.SelectedTab == tabPage3)
            {
                DataTable ucDt = urunCikis.urunCikisListele();
                grdUrunCikisi.DataSource = ucDt;
                grdUrunCikisi.Columns[0].Visible = false;
            }
            else if (tabControl1.SelectedTab == tabPage4)
            {
                DataTable seDt = satisEkrani.satisEkraniListele();
                grdSatisEkrani.DataSource = seDt;
                grdSatisEkrani.Columns[0].Visible = false;
            }
            else
            {
                DataTable ubDt = urunBilgi.urunBilgiListele();
                grdUrunBilgi.DataSource = ubDt;
            }
        }

        private void btnUgEkle_Click(object sender, EventArgs e)
        {
            string barkodNo = txtUgBarkodNo.Text;
            string girenMiktar = txtUgGirenMiktar.Text;
            string alisFiyati = txtUgAlisFiyati.Text;
            string toplamTutar = txtUgToplamTutar.Text;
            string girisTarihi = dtUgGirisTarihi.Text;
            string firma = txtUgFirma.Text;
            urunGiris.ugUrunEkle(barkodNo, girenMiktar, alisFiyati, toplamTutar, girisTarihi, firma);

            DataTable ugDt = urunGiris.urunGirisiListele();
            grdUrunGirisi.DataSource = ugDt;

            txtUgBarkodNo.Text = "";
            txtUgGirenMiktar.Text = "";
            txtUgAlisFiyati.Text = "";
            txtUgToplamTutar.Text = "";
            txtUgFirma.Text = "";
            dtUgGirisTarihi.Text = DateTime.Now.ToString();
        }

        private void btnUgSil_Click(object sender, EventArgs e)
        {
            string ID = lblUgID.Text;
            urunGiris.ugUrunSil(ID);

            DataTable ugDt = urunGiris.urunGirisiListele();
            grdUrunGirisi.DataSource = ugDt;

            txtUgBarkodNo.Text = "";
            txtUgGirenMiktar.Text = "";
            txtUgAlisFiyati.Text = "";
            txtUgToplamTutar.Text = "";
            txtUgFirma.Text = "";
            dtUgGirisTarihi.Text = DateTime.Now.ToString();
            lblUgID.Text = "Düzenlenecek Veya Silinecek Olan ID...";
        }

        private void btnUgDüzenle_Click(object sender, EventArgs e)
        {
            string id = lblUgID.Text;
            string girenMiktar = txtUgGirenMiktar.Text;
            string alisFiyati = txtUgAlisFiyati.Text;
            string toplamTutar = txtUgToplamTutar.Text;
            string girisTarihi = dtUgGirisTarihi.Text;
            string Firma = txtUgFirma.Text;
            urunGiris.ugUrunDuzenle(id, girenMiktar, alisFiyati, toplamTutar, girisTarihi, Firma);

            DataTable ugDt = urunGiris.urunGirisiListele();
            grdUrunGirisi.DataSource = ugDt;

            txtUgBarkodNo.Text = "";
            txtUgGirenMiktar.Text = "";
            txtUgAlisFiyati.Text = "";
            txtUgToplamTutar.Text = "";
            txtUgFirma.Text = "";
            dtUgGirisTarihi.Text = DateTime.Now.ToString();
            lblUgID.Text = "Düzenlenecek Veya Silinecek Olan ID...";
        }

        private void btnUcSil_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ucUrunSil", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(lblUcID.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ürün başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                DataTable ucDt = urunCikis.urunCikisListele();
                grdUrunCikisi.DataSource = ucDt;

                lblUcID.Text = "Silinecek Olan ID...";
            }
        }

        private void btnSeEkle_Click(object sender, EventArgs e)
        {
            string barkodNo = txtSeBarkodNo.Text;
            string satisAdet = txtSeSatisMiktari.Text;
            string satisFiyat = txtSeSatisFiyati.Text;
            string toplamTutar = txtSeToplamTutar.Text;
            string cikisTarihi = dtSeSatisTarihi.Text;
            string aciklama = txtSeAciklama.Text;
            satisEkrani.seUrunEkle(barkodNo, satisAdet, satisFiyat, toplamTutar, cikisTarihi, aciklama);

            DataTable seDt = satisEkrani.satisEkraniListele();
            grdSatisEkrani.DataSource = seDt;

            txtSeBarkodNo.Text = "";
            txtSeSatisMiktari.Text = "";
            txtSeSatisFiyati.Text = "";
            txtSeToplamTutar.Text = "";
            dtSeSatisTarihi.Text = DateTime.Now.ToString();
            txtSeAciklama.Text = "";

            seLblToplamTutar();
            kdvhesapla();
            geneltoplam();
        }

        private void btnSeSil_Click(object sender, EventArgs e)
        {
            string ID = lblSeID.Text;
            satisEkrani.seUrunSil(ID);

            DataTable seDt = satisEkrani.satisEkraniListele();
            grdSatisEkrani.DataSource = seDt;

            txtSeBarkodNo.Text = "";
            txtSeSatisMiktari.Text = "";
            txtSeSatisFiyati.Text = "";
            txtSeToplamTutar.Text = "";
            dtSeSatisTarihi.Text = DateTime.Now.ToString();
            txtSeAciklama.Text = "";
            lblSeID.Text = "Düzenlenecek Veya Silinecek Olan ID...";

            seLblToplamTutar();
            kdvhesapla();
            geneltoplam();
        }

        private void btnSeSatisIptal_Click(object sender, EventArgs e)
        {
            satisEkrani.seSatisIptal();
            DataTable seDt = satisEkrani.satisEkraniListele();
            grdSatisEkrani.DataSource = seDt;

            seLblToplamTutar();
            kdvhesapla();
            geneltoplam();
        }

        private void btnSeSatisYap_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                for (int i = 0; i < grdSatisEkrani.Rows.Count - 1; i++)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("sp_seSatisYap", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@barkodno", grdSatisEkrani.Rows[i].Cells["BARKOD NO"].Value.ToString());
                    cmd.Parameters.AddWithValue("@cikanMiktar", int.Parse(grdSatisEkrani.Rows[i].Cells["SATIŞ ADETİ"].Value.ToString()));
                    cmd.Parameters.AddWithValue("@satisFiyati", decimal.Parse(grdSatisEkrani.Rows[i].Cells["SATIŞ FİYATI"].Value.ToString()));
                    cmd.Parameters.AddWithValue("@toplamTutar", decimal.Parse(grdSatisEkrani.Rows[i].Cells["TOPLAM TUTARI"].Value.ToString()));
                    cmd.Parameters.AddWithValue("@cikisTarihi", grdSatisEkrani.Rows[i].Cells["ÇIKIŞ TARİHİ"].Value.ToString());
                    cmd.Parameters.AddWithValue("@aciklama", grdSatisEkrani.Rows[i].Cells["AÇIKLAMA"].Value.ToString());

                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                MessageBox.Show("Satış başarıyla gerçekleşti. Satış Listele tablosuna ürünler eklendi.");
                satisEkrani.seSatisIptal();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Satış Yap Fonksiyonunda Hata: " + ex.Message);
            }
            finally
            {
                DataTable seDt = satisEkrani.satisEkraniListele();
                grdSatisEkrani.DataSource = seDt;

                seLblToplamTutar();
                kdvhesapla();
                geneltoplam();
            }
        }

        private void btnSeFiyatVer_Click(object sender, EventArgs e)
        {
            export_dgw_excel_2(grdSatisEkrani);
        }

        private void btnUbgEkle_Click(object sender, EventArgs e)
        {
            string barkodNo = txtUbgBarkodNo.Text;
            string urunAdi = txtUbgUrunAdi.Text;
            string birimi = txtUbgBirimi.Text;
            urunBilgi.ubgUrunEkle(barkodNo, urunAdi, birimi);

            DataTable ubDt = urunBilgi.urunBilgiListele();
            grdUrunBilgi.DataSource = ubDt;

            txtUbgBarkodNo.Text = "";
            txtUbgUrunAdi.Text = "";
            txtUbgBirimi.Text = "";
        }

        private void btnUbgSil_Click(object sender, EventArgs e)
        {
            string ID = txtUbgBarkodNo.Text;
            urunBilgi.ubgUrunSİl(ID);

            DataTable ubDt = urunBilgi.urunBilgiListele();
            grdUrunBilgi.DataSource = ubDt;

            txtUbgBarkodNo.Text = "";
            txtUbgUrunAdi.Text = "";
            txtUbgBirimi.Text = "";
        }

        private void btnUbgDüzenle_Click(object sender, EventArgs e)
        {
            string barkodNo = txtUbgBarkodNo.Text;
            string urunAdi = txtUbgUrunAdi.Text;
            string birimi = txtUbgBirimi.Text;
            urunBilgi.ugUrunDuzenle(barkodNo, urunAdi, birimi);

            DataTable ubDt = urunBilgi.urunBilgiListele();
            grdUrunBilgi.DataSource = ubDt;

            txtUbgBarkodNo.Text = "";
            txtUbgUrunAdi.Text = "";
            txtUbgBirimi.Text = "";
        }

        private void txtAsBarkodNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable filteredTable = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM fn_AsBarkodSorgula(@barkod)", conn);
                cmd.Parameters.AddWithValue("@barkod", txtAsBarkodNo.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(filteredTable);
                grdAnlik.DataSource = filteredTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu" + ex);
            }

            if (string.IsNullOrEmpty(txtAsBarkodNo.Text))
            {
                DataTable dt = anlikStok.anlikStokListele();
                grdAnlik.DataSource = dt;
            }
        }

        private void txtAsUrunAdi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable filteredTable = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM fn_AsUrunAdiSorgula(@urunAdi)", conn);
                cmd.Parameters.AddWithValue("@urunAdi", txtAsUrunAdi.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(filteredTable);
                grdAnlik.DataSource = filteredTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu" + ex);
            }

            if (string.IsNullOrEmpty(txtAsUrunAdi.Text))
            {
                DataTable dt = anlikStok.anlikStokListele();
                grdAnlik.DataSource = dt;
            }
        }

        private void txtAsBirim_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable filteredTable = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM fn_AsBirimSorgula(@birim)", conn);
                cmd.Parameters.AddWithValue("@birim", txtAsBirim.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(filteredTable);
                grdAnlik.DataSource = filteredTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu" + ex);
            }

            if (string.IsNullOrEmpty(txtAsBirim.Text))
            {
                DataTable dt = anlikStok.anlikStokListele();
                grdAnlik.DataSource = dt;
            }
        }

        private void txtUcBarkodNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable filteredTable = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM fn_UcBarkodSorgula(@barkod)", conn);
                cmd.Parameters.AddWithValue("@barkod", txtUcBarkodNo.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(filteredTable);
                grdUrunCikisi.DataSource = filteredTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu" + ex);
            }

            if (string.IsNullOrEmpty(txtUcBarkodNo.Text))
            {
                DataTable dt = urunCikis.urunCikisListele();
                grdUrunCikisi.DataSource = dt;
            }
        }

        private void txtUcUrunAdi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable filteredTable = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM fn_UcUrunAdiSorgula(@urunAdi)", conn);
                cmd.Parameters.AddWithValue("@urunAdi", txtUcUrunAdi.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(filteredTable);
                grdUrunCikisi.DataSource = filteredTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu" + ex);
            }

            if (string.IsNullOrEmpty(txtUcUrunAdi.Text))
            {
                DataTable dt = urunCikis.urunCikisListele();
                grdUrunCikisi.DataSource = dt;
            }
        }

        private void txtUcBirimi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable filteredTable = new DataTable();
                SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM fn_UcBirimSorgula(@birim)", conn);
                cmd.Parameters.AddWithValue("@birim", txtUcBirimi.Text);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(filteredTable);
                grdUrunCikisi.DataSource = filteredTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu" + ex);
            }

            if (string.IsNullOrEmpty(txtUcBirimi.Text))
            {
                DataTable dt = urunCikis.urunCikisListele();
                grdUrunCikisi.DataSource = dt;
            }
        }

        private void txtUgGirenMiktar_TextChanged(object sender, EventArgs e)
        {
            ugToplamTutarHesapla();
        }

        private void txtUgAlisFiyati_TextChanged(object sender, EventArgs e)
        {
            ugToplamTutarHesapla();
        }

        private void ugToplamTutarHesapla()
        {
            if (txtUgGirenMiktar.Text == "")
            {
                txtUgToplamTutar.Text = "0";
            }
            else if (txtUgAlisFiyati.Text == "")
            {
                txtUgToplamTutar.Text = "0";
            }
            else
            {
                txtUgToplamTutar.Text = (double.Parse(txtUgGirenMiktar.Text) * double.Parse(txtUgAlisFiyati.Text)).ToString();
            }
        }

        private void txtSeSatisMiktari_TextChanged(object sender, EventArgs e)
        {
            seToplamTutar();
        }

        private void txtSeSatisFiyati_TextChanged(object sender, EventArgs e)
        {
            seToplamTutar();
        }

        private void seToplamTutar()
        {
            if (txtSeSatisMiktari.Text == "")
            {
                txtSeToplamTutar.Text = "0";
            }
            else if (txtSeSatisFiyati.Text == "")
            {
                txtSeToplamTutar.Text = "0";
            }
            else
            {
                txtSeToplamTutar.Text = (double.Parse(txtSeSatisMiktari.Text) * double.Parse(txtSeSatisFiyati.Text)).ToString();
            }
        }

        private void grdUrunGirisi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblUgID.Text = grdUrunGirisi.CurrentRow.Cells["girenID"].Value.ToString();
            txtUgBarkodNo.Text = grdUrunGirisi.CurrentRow.Cells["BARKOD NO"].Value.ToString();
            txtUgGirenMiktar.Text = grdUrunGirisi.CurrentRow.Cells["GİREN MİKTAR"].Value.ToString();
            txtUgAlisFiyati.Text = grdUrunGirisi.CurrentRow.Cells["ALIŞ FİYATI"].Value.ToString();
            dtUgGirisTarihi.Text = grdUrunGirisi.CurrentRow.Cells["GİRİŞ TARİHİ"].Value.ToString();
            txtUgFirma.Text = grdUrunGirisi.CurrentRow.Cells["FİRMA"].Value.ToString();
        }

        private void grdSatisEkrani_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblSeID.Text = grdSatisEkrani.CurrentRow.Cells["sepetID"].Value.ToString();
            txtSeBarkodNo.Text = grdSatisEkrani.CurrentRow.Cells["BARKOD NO"].Value.ToString();
            txtSeSatisMiktari.Text = grdSatisEkrani.CurrentRow.Cells["SATIŞ ADETİ"].Value.ToString();
            txtSeSatisFiyati.Text = grdSatisEkrani.CurrentRow.Cells["SATIŞ FİYATI"].Value.ToString();
            txtSeToplamTutar.Text = grdSatisEkrani.CurrentRow.Cells["TOPLAM TUTARI"].Value.ToString();
            dtSeSatisTarihi.Text = grdSatisEkrani.CurrentRow.Cells["ÇIKIŞ TARİHİ"].Value.ToString();
            txtSeAciklama.Text = grdSatisEkrani.CurrentRow.Cells["AÇIKLAMA"].Value.ToString();
        }

        private void grdUrunBilgi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUbgBarkodNo.Text = grdUrunBilgi.CurrentRow.Cells["BARKOD NO"].Value.ToString();
            txtUbgUrunAdi.Text = grdUrunBilgi.CurrentRow.Cells["ÜRÜN ADI"].Value.ToString();
            txtUbgBirimi.Text = grdUrunBilgi.CurrentRow.Cells["BİRİMİ"].Value.ToString();
        }

        private void grdUrunCikisi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblUcID.Text = grdUrunCikisi.CurrentRow.Cells["cikanID"].Value.ToString();
        }

        bool export_dgw_excel_2(DataGridView dgw)
        {
            bool durum = false;
            try
            {
                Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = true;

                string programDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
                string excelFileName = "satis.xlsx";
                string excelFilePath = System.IO.Path.Combine(programDirectory, excelFileName);

                Excel.Workbook workbook = excel.Workbooks.Open(excelFilePath);
                Excel.Worksheet sheet1 = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Sheets[1];

                int StartCol = 1; // Excel'de sütun başlatma indeksi (A sütunu)
                int StartRow = 8; // Başlangıç satırı
                int[] columnsToExport = { 2, 3, 4, 5, 6 }; // Yazdırılacak sütunlar
                int j, i;

                string label17Text = label17.Text;
                int targetRow = 40;
                int targetColumn = 6;

                string label9Text = label9.Text;
                int targetRow1 = 41;
                int targetColumn1 = 6;

                string label1Text = label1.Text;
                int targetRow2 = 42;
                int targetColumn2 = 6;

                StartRow++;
                for (i = 0; i < dgw.Rows.Count; i++)
                {
                    for (int k = 0; k < columnsToExport.Length; k++)
                    {
                        j = columnsToExport[k];
                        try
                        {
                            Microsoft.Office.Interop.Excel.Range myRange = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[StartRow + i, StartCol + k + 1];
                            myRange.Value2 = dgw[j, i].Value == null ? "" : dgw[j, i].Value;
                        }
                        catch
                        {
                            
                        }
                    }
                }

                Microsoft.Office.Interop.Excel.Range targetCell = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[targetRow, targetColumn];
                targetCell.Value2 = label17Text;

                Microsoft.Office.Interop.Excel.Range targetCell1 = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[targetRow1, targetColumn1];
                targetCell1.Value2 = label9Text;

                Microsoft.Office.Interop.Excel.Range targetCell2 = (Microsoft.Office.Interop.Excel.Range)sheet1.Cells[targetRow2, targetColumn2];
                targetCell2.Value2 = label1Text;

                durum = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("DataGrid Verileri Aktarılamadı : " + ex.Message);
            }
            return durum;
        }

        private void seLblToplamTutar()
        {
            try
            {
                double toplamTutar = 0.0;

                // DataGridView'deki toplamTutar hücrelerini topla
                foreach (DataGridViewRow row in grdSatisEkrani.Rows)
                {
                    if (row.Cells["TOPLAM TUTARI"].Value != null)
                    {
                        double hucreselTutar;
                        if (double.TryParse(row.Cells["TOPLAM TUTARI"].Value.ToString(), out hucreselTutar))
                        {
                            toplamTutar += hucreselTutar;
                        }
                    }
                }

                // Sonucu label17'ye yazdır
                label17.Text = toplamTutar.ToString("F2") + "TL";
            }
            catch (Exception ex)
            {
                // Hata işleme: Hata mesajını görüntüleyebilir veya bir hata günlüğüne kaydedebilirsiniz.
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }

        private void kdvhesapla()

        {
            try
            {
                // Label 17'deki metni double bir sayıya çevirin
                if (double.TryParse(label17.Text.Replace("TL", ""), out double sayi7))
                {
                    // Sayıyı 0.20 ile çarpın
                    double sonuc = sayi7 * 0.20;

                    // Sonucu Label 9'a yazdırın
                    label9.Text = sonuc.ToString("F2") + "TL"; // Sonucu iki ondalık basamaklı olarak göstermek için "F2" formatını kullanıyoruz
                }
                else
                {
                    MessageBox.Show("Label 17'deki metin bir sayıya çevrilemiyor.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("KDV Hesaplama fonksiyonunda hata oluştu !" + ex.Message);
            }
        }

        private void geneltoplam()
        {
            try
            {
                // Label 7'deki metni double bir sayıya çevirin
                if (double.TryParse(label17.Text.Replace("TL", ""), out double sayi7))
                {
                    // Label 9'daki metni double bir sayıya çevirin
                    if (double.TryParse(label9.Text.Replace("TL", ""), out double sayi9))
                    {
                        // Label 7 ve Label 9'daki sayıları toplayın
                        double toplam = sayi7 + sayi9;

                        // Toplamı Label 1'e yazdırın
                        label1.Text = toplam.ToString("F2") + "TL"; // Sonucu iki ondalık basamaklı olarak göstermek için "F2" formatını kullanıyoruz
                    }
                    else
                    {
                        MessageBox.Show("Label 9'daki metin bir sayıya çevrilemiyor.");
                    }
                }
                else
                {
                    MessageBox.Show("Label 17'deki metin bir sayıya çevrilemiyor.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }

        }

        private void txtAsBarkodNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Eğer girdi bir sayı değilse ve kontrol karakteri de değilse, işlemi engelle
                e.Handled = true;
            }
        }

        private void txtUgBarkodNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Eğer girdi bir sayı değilse ve kontrol karakteri de değilse, işlemi engelle
                e.Handled = true;
            }
        }

        private void txtUgGirenMiktar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Eğer girdi bir sayı değilse ve kontrol karakteri de değilse, işlemi engelle
                e.Handled = true;
            }
        }

        private void txtUgAlisFiyati_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                // Eğer girdi bir sayı, virgül veya kontrol karakteri değilse, işlemi engelle
                e.Handled = true;
            }
        }

        private void txtUcBarkodNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Eğer girdi bir sayı değilse ve kontrol karakteri de değilse, işlemi engelle
                e.Handled = true;
            }
        }

        private void txtSeBarkodNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Eğer girdi bir sayı değilse ve kontrol karakteri de değilse, işlemi engelle
                e.Handled = true;
            }
        }

        private void txtSeSatisMiktari_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Eğer girdi bir sayı değilse ve kontrol karakteri de değilse, işlemi engelle
                e.Handled = true;
            }
        }

        private void txtSeSatisFiyati_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',')
            {
                // Eğer girdi bir sayı, virgül veya kontrol karakteri değilse, işlemi engelle
                e.Handled = true;
            }
        }
    }
}
