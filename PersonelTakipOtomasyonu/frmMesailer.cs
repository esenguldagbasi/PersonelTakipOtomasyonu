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
    public partial class frmMesailer : Form
    {
        public frmMesailer()
        {
            InitializeComponent();
        }

        private void frmMesailer_Load(object sender, EventArgs e)
        {
            int yil = DateTime.Now.Year;
            for (int i = yil; i >= yil - 5; i--)
            {
                cmbYil.Items.Add(i);
            }
            Veritabani.Listele_Ara(dataGridView1, "Select * from Mesailer");

        }

        private void txtPersonelId_TextChanged(object sender, EventArgs e)
        {
            Primler.PersonelAdSoyadGetir(txtPersonelId, txtAdSoyad);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["OdenmeDurumu"].Value.ToString()=="Ödenmedi") 
            {
                txtMesaiId.Text = dataGridView1.CurrentRow.Cells["MesaiId"].Value.ToString();
                txtPersonelId.Text = dataGridView1.CurrentRow.Cells["PersonelId"].Value.ToString();
             
                txtAciklama.Text = dataGridView1.CurrentRow.Cells["Aciklama"].Value.ToString();

                string baslangic = dataGridView1.CurrentRow.Cells["BaslangicSaati"].Value.ToString();
                string bitis = dataGridView1.CurrentRow.Cells["BitisSaati"].Value.ToString();
                string donem = dataGridView1.CurrentRow.Cells["Donem"].Value.ToString();
                dateTimeBaslangic.Text = baslangic.Substring(0, 10);
                mskBaslangic.Text = baslangic.Substring(11);
                dateTimeBitis.Text = bitis.Substring(0, 10);
                mskBitis.Text = bitis.Substring(11);

                int say = donem.IndexOf("/");
                cmbAy.Text = donem.Substring(0, say);
                cmbYil.Text = donem.Substring(say + 1);
                txtMesaiSaatUcreti.Text = dataGridView1.CurrentRow.Cells["MesaiSaatUcreti"].Value.ToString();
            }
            
        }

        private void txtMesaiSaatUcreti_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string baslangic = dateTimeBaslangic.Text + " " + mskBaslangic.Text;
                string bitis = dateTimeBitis.Text + " " + mskBitis.Text;
                TimeSpan saatfarki = DateTime.Parse(bitis) - DateTime.Parse(baslangic);
                double MSaatUcreti = double.Parse(txtMesaiSaatUcreti.Text);
                double tutar = saatfarki.TotalHours * MSaatUcreti;
                txtTutar.Text = tutar.ToString("0.00");
            }
            catch
            {


            }
        }

        private void btnPersonelMesaileri_Click(object sender, EventArgs e)
        {
            frmPersonelMesaileri frm = new frmPersonelMesaileri();
            frm.ShowDialog();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";

                }
                if (item is MaskedTextBox)
                {
                    item.Text = "";
                }

            }
            dateTimeBaslangic.Value = DateTime.Now;
            dateTimeBitis.Value = DateTime.Now;
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTumMesaileriOde_Click(object sender, EventArgs e)
        {
            Mesailer m = new Mesailer();
            Kullanicilar k=new Kullanicilar();
            Personeller p= new Personeller();
            m.OdenmeDurumu = "Ödendi";
            string sql = "update Mesailer set OdenmeDurumu='" + m.OdenmeDurumu + "' where OdenmeDurumu='Ödenmedi'";
            SqlCommand komut = new SqlCommand();
            Veritabani.ESG(komut, sql);
            MessageBox.Show("Ödenmeyen tüm mesailer ödendi.", "Mesai Ödeme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnTemizle.PerformClick();
            
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                if (dataGridView1.Rows[i].Cells["OdenmeDurumu"].Value.ToString()== "Ödenmedi")
                {
                    m.MesaiID= int.Parse(dataGridView1.Rows[i].Cells["MesaiId"].Value.ToString());
                    p.PersonelID= int.Parse(dataGridView1.Rows[i].Cells["PersonelId"].Value.ToString());
                    m.Islem =m.MesaiID+ "nolu mesai ücreti ödendi.";
                    m.Aciklama = "Tüm mesaileri ödeme";
                    MesaiHareketleriEkle(k, m, p);
                }
            }
            btnTemizle.PerformClick();
            Veritabani.Listele_Ara(dataGridView1, "Select * from Mesailer");
        }
        void MesaiHareketleriEkle(Kullanicilar k, Mesailer m, Personeller p)
        {
            k.KullaniciId=Kullanicilar.kid;
            string sql = "insert into MesaiHareketleri values ('"+k.KullaniciId+"', '"+p.PersonelID+"', '"+m.MesaiID+"', '"+m.Islem+"', '"+m.Aciklama+"', @Tarih)";

            SqlCommand komut = new SqlCommand();
            komut.Parameters.Add("@Tarih",SqlDbType.Date).Value=DateTime.Now;
            Veritabani.ESG(komut,sql);
        }
        private void btnMesaiOde_Click(object sender, EventArgs e)
        {
            Mesailer m = new Mesailer();
            Kullanicilar k= new Kullanicilar(); 
            Personeller p= new Personeller();
            p.PersonelID=int.Parse(txtPersonelId.Text);

            m.OdenmeDurumu = "Ödendi";
            m.MesaiID = int.Parse(txtMesaiId.Text);
            m.Islem = m.MesaiID + "nolu mesai için ödeme yapıldı.";
            m.Aciklama = "Mesai Ödeme";

            string sql = "update Mesailer set OdenmeDurumu='" + m.OdenmeDurumu + "' where MesaiId='" + m.MesaiID + "'";
            SqlCommand komut = new SqlCommand();
            Veritabani.ESG(komut, sql);
            MessageBox.Show(m.MesaiID + " nolu mesai ücreti ödendi.", "Mesai Ödeme", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MesaiHareketleriEkle(k,m,p);
            btnTemizle.PerformClick();
            Veritabani.Listele_Ara(dataGridView1, "Select * from Mesailer");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            
            Mesailer m = new Mesailer();
            Kullanicilar k = new Kullanicilar();
            Personeller p = new Personeller();
            m.MesaiID=int.Parse(txtMesaiId.Text);
            p.PersonelID=int.Parse(txtPersonelId.Text);
            m.Islem = m.MesaiID + "mesaj kaydı silindi.";
            m.Aciklama = "Mesai Silme";
            if (MessageBox.Show("Bu kayıt silinsin mi?","Mesai Silme Uyarısı",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                string sql = "delete from Mesailer where MesaiId='" + m.MesaiID + "'";
                SqlCommand komut = new SqlCommand();
                Veritabani.ESG(komut, sql);
                MessageBox.Show(m.MesaiID + " nolu mesai kaydı silindi.", "Mesai Silme", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                MesaiHareketleriEkle(k,m,p);
                btnTemizle.PerformClick();
                Veritabani.Listele_Ara(dataGridView1, "Select * from Mesailer");
            }
            
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            Mesailer m=new Mesailer();
            Personeller p= new Personeller();
            Kullanicilar k= new Kullanicilar();
            p.PersonelID=int.Parse(txtPersonelId.Text);
            m.MesaiID=int.Parse(txtMesaiId.Text) ;
            m.BaslangicSaati = dateTimeBaslangic.Text + " " + mskBaslangic.Text;
            m.BitisSaati=dateTimeBitis.Text + " " + mskBitis.Text;
            m.MesaiSaatUcreti=decimal.Parse(txtMesaiSaatUcreti.Text);
            m.Tutar=decimal.Parse(txtTutar.Text);
            m.Donem=cmbAy.Text+"/"+ cmbYil.Text;
            m.Aciklama=txtAciklama.Text;
            string sql = "update Mesailer set PersonelId='"+p.PersonelID+"', BaslangicSaati='"+m.BaslangicSaati+"', BitisSaati='"+m.BitisSaati+"', MesaiSaatUcreti=@MSaatUcreti, Tutar=@Tutar, Donem='"+m.Donem+"', Aciklama='"+m.Aciklama+"' where MesaiId='"+m.MesaiID+"'";
            SqlCommand komut=new SqlCommand();
            komut.Parameters.Add("@MSaatUcreti",SqlDbType.Decimal).Value=m.MesaiSaatUcreti;
            komut.Parameters.Add("@Tutar", SqlDbType.Decimal).Value=m.Tutar;
            Veritabani.ESG(komut, sql);
            MessageBox.Show(m.MesaiID + " nolu mesai kaydı güncellendi.", "Mesai Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);

            m.Islem = m.MesaiID + "nolu mesai kaydı için güncelleme yapıldı.";
            m.Aciklama = "Mesai Güncelleme";
            MesaiHareketleriEkle(k,m,p);
            btnTemizle.PerformClick();
            Veritabani.Listele_Ara(dataGridView1, "Select * from Mesailer");

        }

        private void txtTutar_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
