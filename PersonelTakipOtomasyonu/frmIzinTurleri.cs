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
    public partial class frmIzinTurleri : Form
    {
        public frmIzinTurleri()
        {
            InitializeComponent();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmIzınTurleri_Load(object sender, EventArgs e)
        {
            Izin.ListvieweKayitGetir(listView1);
        }
        private void Temizle()
        {
            IzinTurID.Text = "";
            IzinTur.Text = "";

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            
            try
            {
                Izin i = new Izin();
                i.IzinTuru=txtIzinTuru.Text;
                string sql = "insert into IzinTurleri values ('"+i.IzinTuru+"')";
                SqlCommand komut = new SqlCommand();
                Veritabani.ESG(komut,sql);
                MessageBox.Show("Yeni kayıt eklendi.","Kayıt Ekleme",MessageBoxButtons.OK, MessageBoxIcon.Information);
                Izin.ListvieweKayitGetir(listView1);
                Temizle();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Hata Türü");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                Izin i = new Izin();
                i.IzinTurId = int.Parse(txtIzinTurId.Text);
                i.IzinTuru = txtIzinTuru.Text;
                string sql = "update IzinTurleri set IzinTuru= '"+i.IzinTuru+"' where IzinTurId='"+i.IzinTurId+"'";
                SqlCommand komut = new SqlCommand();
                Veritabani.ESG(komut, sql);
                MessageBox.Show("Kayıt güncellendi.", "Kayıt Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Izin.ListvieweKayitGetir(listView1);
                Temizle();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Hata Türü");
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            txtIzinTurId.Text= listView1.SelectedItems[0].SubItems[0].Text;
            txtIzinTuru.Text= listView1.SelectedItems[0].SubItems[1].Text;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count>0)
            {
                Izin i = new Izin();
                i.IzinTurId = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
                
                string sql = "delete from IzinTurleri where IzinTurId='"+i.IzinTurId+"'";
                SqlCommand komut = new SqlCommand();
                Veritabani.ESG(komut, sql);
                MessageBox.Show("Kayıt silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Izin.ListvieweKayitGetir(listView1);
                Temizle();
            }
            else
            {
                MessageBox.Show("Önce kayıt seçilmelidir.", "Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
    }
}
