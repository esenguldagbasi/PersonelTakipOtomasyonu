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
    public partial class frmMesaiEkle : Form
    {
        public frmMesaiEkle()
        {
            InitializeComponent();
        }

        private void frmMesaiEkle_Load(object sender, EventArgs e)
        {
            int yil=DateTime.Now.Year;
            for(int i = yil; i >=yil-5; i--) 
            {
                cmbYil.Items.Add(i); 
            }
            YapilanZamlar.ComboyaPersonelGetir(cmbPersonelAdSoyad);
        }
        Label lbl;
        private void cmbPersonelAdSoyad_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl = new Label();
            YapilanZamlar.ComboSecilirsePersonelIdGetir(cmbPersonelAdSoyad,lbl);
            MessageBox.Show(lbl.Text);
        }

        private void btnMesaiEkle_Click(object sender, EventArgs e)
        {
            Personeller p = new Personeller();
            Kullanicilar k= new Kullanicilar();
            Mesailer m = new Mesailer();
            k.KullaniciId=Kullanicilar.kid;
            p.PersonelID=int.Parse(lbl.Text);
            m.BaslangicSaati=dateTimeBaslangic.Text+" "+ mskBaslangic.Text;
            m.BitisSaati=dateTimeBitis.Text+" "+ mskBitis.Text;
            m.MesaiSaatUcreti=decimal.Parse(txtMesaiSaatUcreti.Text);
            m.Tutar=decimal.Parse(txtTutar.Text);
            m.Donem = cmbAy.Text + "/" + cmbYil.Text;
            m.Aciklama=txtAciklama.Text;
            m.Tarih=DateTime.Now;
            string sql = "insert into Mesailer (KullaniciId, PersonelId, BaslangicSaati, BitisSaati, MesaiSaatUcreti, Tutar, Donem, Aciklama, Tarih) values ('"+k.KullaniciId+"','"+p.PersonelID+"', '"+m.BaslangicSaati+"', '"+m.BitisSaati+"', @MesaiSaatUcreti, @Tutar, '"+m.Donem+"', '"+m.Aciklama+"', @Tarih )";

            SqlCommand komut = new SqlCommand();
            komut.Parameters.Add("@MesaiSaatUcreti", SqlDbType.Decimal).Value = m.MesaiSaatUcreti;
            komut.Parameters.Add("@Tutar",SqlDbType.Decimal).Value=m.Tutar;
            komut.Parameters.Add("@Tarih", SqlDbType.Date).Value = m.Tarih;

            Veritabani.ESG(komut,sql);
            MessageBox.Show("Mesai bilgileri eklendi.","Mesailer",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void txtTutar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtMesaiSaatUcreti_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string baslangic= dateTimeBaslangic.Text+" "+mskBaslangic.Text;
                string bitis = dateTimeBitis.Text+" "+mskBitis.Text;
                TimeSpan saatfarki= DateTime.Parse(bitis) - DateTime.Parse(baslangic);
                double MSaatUcreti= double.Parse(txtMesaiSaatUcreti.Text);
                double Tutar= saatfarki.TotalHours*MSaatUcreti;
                txtTutar.Text = Tutar.ToString("0.00");

            }
            catch 
            {

                
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
