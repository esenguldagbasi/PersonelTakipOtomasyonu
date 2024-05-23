using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonelTakipOtomasyonu
{
    public partial class frmIzinHareketleri : Form
    {

        private StringBuilder sb;
        public frmIzinHareketleri()
        {
            InitializeComponent();
            sb = new StringBuilder();
        }
        Izin izin = new Izin();
        private void btnIzinTurleri_Click(object sender, EventArgs e)
        {
            frmIzinTurleri frm = new frmIzinTurleri();
            frm.ShowDialog();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            Izin i = new Izin();
            i.IzinHareketId = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            sb.Clear();
            sb.AppendFormat(@"DELETE FROM [dbo].[IzinHareketleri]
      WHERE [IzinHareketId]='{0}'",
      i.IzinHareketId);
            SqlCommand komut= new SqlCommand(sb.ToString());
            try
            {
                Veritabani.ESG(komut);
                Temizle();

                MessageBox.Show("İzin bilgileri silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Veritabani.Listele_Ara(dataGridView1, "select IzinHareketId, PersonelId, KullaniciId, tur.IzinTuru, IzinBaslangic, IzinBitis, Islem, Aciklama, Tarih, Saat from IzinHareketleri i, IzinTurleri tur where i.IzinTurId=tur.IzinTurId ");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Uyarı");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Izin i = new Izin();
            i.IzinHareketId =int.Parse(txtIzinHareketId.Text);
            i.PersonelID = int.Parse(txtPersonelId.Text);
            i.KullaniciId = Kullanicilar.kid;
            i.IzinTurId = (int)cmbIzinTurleri.SelectedValue;
            i.IzinBaslangic = dateTimePickerBaslangic.Value;
            i.IzinBitis = dateTimePickerBitis.Value;
            i.Aciklama = txtAciklama.Text;
            i.Tarih = DateTime.Now;
            i.Saat = DateTime.Now;
            i.Islem = i.IzinHareketId + " nolu izin bilgileri değiştirildi.";
            sb.Clear();
            sb.AppendFormat(@"UPDATE [dbo].[IzinHareketleri]
   SET [PersonelId] = '{0}'
      ,[KullaniciId] = '{1}'
      ,[IzinTurId] = '{2}'
      ,[IzinBaslangic] = '{3}'
      ,[IzinBitis] ='{4}'
      ,[Islem] ='{5}'
      ,[Aciklama] ='{6}'
      ,[Tarih] = '{7}'
      ,[Saat] = '{8}'
 WHERE [IzinHareketId]= '{9}' ",
            i.PersonelID,
            i.KullaniciId,
            i.IzinTurId,
            i.IzinBaslangic.ToString("yyyy-MM-dd"),
            i.IzinBitis.ToString("yyyy-MM-dd"),
            i.Islem,
            i.Aciklama,
            i.Tarih.ToString("yyyy-MM-dd"),
            i.Saat.ToString("yyyy-MM-dd HH:mm:ss"),
            i.IzinHareketId);
            
            SqlCommand komut = new SqlCommand(sb.ToString());
            //string sql = "Update IzinHareketleri set PersonelId='" + i.PersonelID + "', IzinTurId='"+i.IzinTurId+"', IzinBaslangic=@IzinBaslangic, IzinBitis=@IzinBitis, Islem='"+i.Islem+"', Aciklama='"+i.Aciklama+"', Tarih=@Tarih, Saat=@Saat where IzinHareketId='"+i.IzinHareketId+"'";
            //SqlCommand komut = new SqlCommand();
            //komut.Parameters.Add("@IzinBaslangic",SqlDbType.Date).Value=i.IzinBaslangic;
            //komut.Parameters.Add("@IzinBitis", SqlDbType.Date).Value=i.IzinBitis;
            //komut.Parameters.Add("@Tarih", SqlDbType.Date).Value=i.Tarih;
            //komut.Parameters.Add("@Saat", SqlDbType.DateTime).Value=i.Saat;
            try
            {
                Veritabani.ESG(komut);
                Temizle();

                MessageBox.Show("İzin bilgileri güncellendi.", "İzin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Veritabani.Listele_Ara(dataGridView1, "select IzinHareketId, PersonelId, KullaniciId, tur.IzinTuru, IzinBaslangic, IzinBitis, Islem, Aciklama, Tarih, Saat from IzinHareketleri i, IzinTurleri tur where i.IzinTurId=tur.IzinTurId ");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Uyarı");
            }
        }
        private void Temizle()
        {
            dateTimePickerBaslangic.Value = DateTime.Now;
            dateTimePickerBitis.Value = DateTime.Now;

            Personeller.ComboyaKayitGetir(cmbIzinTurleri);
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
            }
            
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            Izin i = new Izin();
            i.PersonelID=int.Parse(txtPersonelId.Text);
            i.KullaniciId = Kullanicilar.kid;
            i.IzinTurId=(int)cmbIzinTurleri.SelectedValue;
            i.IzinBaslangic = dateTimePickerBaslangic.Value;
            i.IzinBitis=dateTimePickerBitis.Value;
            i.Islem=(i.PersonelID +" - "+ txtPersonelAdSoyad.Text+ " için "+ cmbIzinTurleri.Text + " oluşturuldu.");
            i.Aciklama=txtAciklama.Text;
            i.Tarih=DateTime.Now;
            i.Saat=DateTime.Now;
            sb.Clear();
            sb.AppendFormat(@"INSERT INTO [dbo].[IzinHareketleri]
           ([PersonelId]
           ,[KullaniciId]
           ,[IzinTurId]
           ,[IzinBaslangic]
           ,[IzinBitis]
           ,[Islem]
           ,[Aciklama]
           ,[Tarih]
           ,[Saat])
     VALUES
            ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')",
            i.PersonelID,
            i.KullaniciId,
            i.IzinTurId,
            i.IzinBaslangic.ToString("yyyy-MM-dd"),
            i.IzinBitis.ToString("yyyy-MM-dd"),
            i.Islem,
            i.Aciklama,
            i.Tarih.ToString("yyyy-MM-dd"),
            i.Saat.ToString("yyyy-MM-dd HH:mm:ss")
            );


            SqlCommand komut = new SqlCommand(sb.ToString());

            try
            {
                Veritabani.ESG(komut);
                Temizle();

                MessageBox.Show("İzin kaydı oluşturuldu.","İzin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Veritabani.Listele_Ara(dataGridView1, "select IzinHareketId, PersonelId, KullaniciId, tur.IzinTuru, IzinBaslangic, IzinBitis, Islem, Aciklama, Tarih, Saat from IzinHareketleri i, IzinTurleri tur where i.IzinTurId=tur.IzinTurId ");

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Uyarı");
            }
        }

        private void frmIzinHareketleri_Load(object sender, EventArgs e)
        {
            Veritabani.Listele_Ara(dataGridView1, "select IzinHareketId, PersonelId, KullaniciId, tur.IzinTuru, IzinBaslangic, IzinBitis, Islem, Aciklama, Tarih, Saat from IzinHareketleri i, IzinTurleri tur where i.IzinTurId=tur.IzinTurId ");
            Personeller.ComboyaKayitGetir(cmbIzinTurleri);
        }

        private void txtPersonelId_TextChanged(object sender, EventArgs e)
        {
            Primler.PersonelAdSoyadGetir(txtPersonelId, txtPersonelAdSoyad);
            {
                if (txtPersonelId.Text=="")
                {
                    txtPersonelAdSoyad.Text = "";
                }

            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtIzinHareketId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtPersonelId.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cmbIzinTurleri.Text = dataGridView1.CurrentRow.Cells["IzinTuru"].Value.ToString();
            dateTimePickerBaslangic.Text = dataGridView1.CurrentRow.Cells["IzinBaslangic"].Value.ToString();
            dateTimePickerBitis.Text = dataGridView1.CurrentRow.Cells["IzinBitis"].Value.ToString();
            txtAciklama.Text = dataGridView1.CurrentRow.Cells["Aciklama"].Value.ToString();
        }

        private void btnExceleAktar_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application uyg= new Microsoft.Office.Interop.Excel.Application();   
            uyg.Visible = true;
            Microsoft.Office.Interop.Excel.Workbook kitap = uyg.Workbooks.Add(System.Reflection.Missing.Value);
            Microsoft.Office.Interop.Excel.Worksheet sayfa = (Microsoft.Office.Interop.Excel.Worksheet)kitap.Sheets[1];
            for (int i = 0; i<dataGridView1.Columns.Count; i++)
            {
                Microsoft.Office.Interop.Excel.Range range= (Microsoft.Office.Interop.Excel.Range)sayfa.Cells[1, i + 1];
                range.Value2 = dataGridView1.Columns[i].HeaderText;
                
            }
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Rows.Count; j++)
                {
                    Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)sayfa.Cells[j + 2, i + 1];
                    range.Value2 = dataGridView1[i, j].Value;
                    sayfa.Columns["B:B"].NumberFormat = "0,00";
                    sayfa.Columns["E:E"].NumberFormat = "gg.aa.yyyy";
                    sayfa.Columns["F:F"].NumberFormat = "gg.aa.yyyy";
                    sayfa.Columns["I:I"].NumberFormat = "gg.aa.yyyy";
                    sayfa.Columns["J:J"].NumberFormat = "gg.aa.yyyy ss:dd:nn";

                }

            }

        }
    }
}
