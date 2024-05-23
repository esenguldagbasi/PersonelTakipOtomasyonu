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
    public partial class frmPersonelEkle : Form
    {
        public frmPersonelEkle()
        {
            InitializeComponent();
        }

        private void frmPersonelEkle_Load(object sender, EventArgs e)
        {
            Personeller.ComboyaDepartmanGetir(cmbDepartman);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void Temizle()
        {
            dtpGirisTarihi.Value=DateTime.Now;
            cmbDepartman.Text = "";
            foreach (Control item in Controls)
            {
                if (item is TextBox) 
                {
                    item.Text = "";
                }

            }
        }
        Personeller p = new Personeller();
        Kullanicilar k=new Kullanicilar();

        private void button1_Click(object sender, EventArgs e)
        {
            
            p.Adi=txtAdi.Text;
            p.Soyadi=txtSoyadi.Text;
            p.Telefon=txtTelefon.Text;
            p.Adres=txtAdres.Text;
            p.Email=txtEmail.Text;
            p.DepartmanID = (int)cmbDepartman.SelectedValue;
            p.Maasi=decimal.Parse(txtMaas.Text);
            p.GirisTarihi=dtpGirisTarihi.Value;
            p.Aciklama=txtAciklama.Text;

            string sorgu = "insert into Personeller (Adi,Soyadi,Telefon,Adres,Email,DepartmanId,Maasi,GirisTarihi,Aciklama) values('" + p.Adi + "', '" + p.Soyadi + "', '" + p.Telefon + "', '" + p.Adres + "', '" + p.Email + "', '" + p.DepartmanID + "', @Maasi, @GirisTarihi, '"+p.Aciklama+"')";
            SqlCommand komut = new SqlCommand();
            komut.Parameters.Add("@Maasi", SqlDbType.Decimal).Value = p.Maasi;
            komut.Parameters.Add("@GirisTarihi", SqlDbType.Date).Value = p.GirisTarihi;

            
            Veritabani.ESG(komut,sorgu);
            Personeller.PersonelIDSonKayit(p);
            p.Islem = p.PersonelID + "nolu yeni personel kaydı oluşturuldu.";
            p.Aciklama = "Yeni personel ekleme";
            Personeller.PersonelIslemEkle(p, k);
            Temizle();
            MessageBox.Show("İşlem başarılı.","Kayıt başarılı",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }
    }
}
