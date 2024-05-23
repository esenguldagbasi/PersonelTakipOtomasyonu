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
    public partial class frmPersonelListele : Form
    {
        public frmPersonelListele()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void frmPersonelListele_Load(object sender, EventArgs e)
        {
            YenileListele();
            Personeller.ComboyaDepartmanGetir(cmbDepartman);

        }

        private void YenileListele()
        {
            Veritabani.Listele_Ara(dataGridView1, "Select p.PersonelId, p.Adi, p.Soyadi, p.Telefon, p.Adres, p.Email, d.Departman, p.Durumu, p.Maasi, p.GirisTarihi, p.Aciklama From Personeller p, Departmanlar d where p.DepartmanId=d.DepartmanId");
            lblToplamKayit.Text= "Toplam" + (dataGridView1.Rows.Count - 1) + "kayıt listelendi.";
            decimal toplammaas = 0;
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                toplammaas += decimal.Parse(dataGridView1.Rows[i].Cells["Maasi"].Value.ToString());
            }
            lblToplamMaas.Text = "Toplam maaş tutarı" + toplammaas.ToString("0.00") + "TL.";
        }






        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            Personeller p = new Personeller();
            p.PersonelID = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
            //string sorgu = "Delete from Personeller where PersonelId='" + p.PersonelID + "'";
            //SqlCommand komu = new SqlCommand();
            //Veritabani.ESG(komut, sorgu);
            string sorgu2 = "update personeller set durumu='Pasif',CikisTarihi=@CikisTarihi where PersonelId='"+p.PersonelID+"'";
            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.Add("@CikisTarihi", SqlDbType.Date).Value = p.CikisTarihi;
            Veritabani.ESG(komut2,sorgu2);
            p.Islem = p.PersonelID + "nolu personel işten çıkarıldı.";
            p.Aciklama = "İşten çıkarma";

            Personeller.PersonelIslemEkle(p, k);
            Temizle();
            MessageBox.Show("İşlem başarılı.", "Sil", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            YenileListele();

        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPersonelID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAdi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtSoyadi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            cmbDepartman.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtMaas.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            dtpGirisTarihi.Value =DateTime.Parse(dataGridView1.CurrentRow.Cells[9].Value.ToString());
            txtAciklama.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            
            
        }

        private void txtPersonelIdAra_TextChanged(object sender, EventArgs e)
        {
            Veritabani.Listele_Ara(dataGridView1, "Select p.PersonelId, p.Adi, p.Soyadi, p.Telefon, p.Adres, p.Email, d.Departman, p.Durumu, p.Maasi, p.GirisTarihi, p.Aciklama From Personeller p, Departmanlar d where p.DepartmanId=d.DepartmanId and PersonelId like '%"+txtPersonelIdAra.Text+"%'");
        }

        private void txtPersonelAdiAra_TextChanged(object sender, EventArgs e)
        {
            Veritabani.Listele_Ara(dataGridView1, "Select p.PersonelId, p.Adi, p.Soyadi, p.Telefon, p.Adres, p.Email, d.Departman, p.Durumu, p.Maasi, p.GirisTarihi, p.Aciklama From Personeller p, Departmanlar d where p.DepartmanId=d.DepartmanId and Adi like '%" + txtPersonelAdiAra.Text + "%'");
        }

        private void txtPersonelSoyadiAra_TextChanged(object sender, EventArgs e)
        {
            Veritabani.Listele_Ara(dataGridView1, "Select p.PersonelId, p.Adi, p.Soyadi, p.Telefon, p.Adres, p.Email, d.Departman, p.Durumu, p.Maasi, p.GirisTarihi, p.Aciklama From Personeller p, Departmanlar d where p.DepartmanId=d.DepartmanId and Soyadi like '%" + txtPersonelSoyadiAra.Text + "%'");
        }

        private void txtPersonelTelefonAra_TextChanged(object sender, EventArgs e)
        {
            Veritabani.Listele_Ara(dataGridView1, "Select p.PersonelId, p.Adi, p.Soyadi, p.Telefon, p.Adres, p.Email, d.Departman, p.Durumu, p.Maasi, p.GirisTarihi, p.Aciklama From Personeller p, Departmanlar d where p.DepartmanId=d.DepartmanId and Telefon like '%" + txtPersonelTelefonAra.Text + "%'");
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Personeller.TariheGoreAra(dateTimePicker1, dataGridView1);
        }
        void Temizle()
        {
            dtpGirisTarihi.Value = DateTime.Now;
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


        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            
            p.PersonelID = int.Parse(txtPersonelID.Text);
            p.Adi = txtAdi.Text;
            p.Soyadi = txtSoyadi.Text;
            p.Telefon = txtTelefon.Text;
            p.Adres = txtAdres.Text;
            p.Email = txtEmail.Text;
            p.DepartmanID = (int)cmbDepartman.SelectedValue;
            p.Maasi = decimal.Parse(txtMaas.Text);
            p.GirisTarihi = dtpGirisTarihi.Value;
            p.Aciklama = txtAciklama.Text;



            string sorgu = "update personeller set adi='" + p.Adi + "', soyadi='" + p.Soyadi + "', telefon='" + p.Telefon + "', adres='" + p.Adres + "', email='" + p.Email + "', departmanId='" + p.DepartmanID + "', maasi=@Maasi, giristarihi=@giristarihi, aciklama='" + p.Aciklama + "' where personelId='" + p.PersonelID + "'   ";
            SqlCommand komut = new SqlCommand();
            komut.Parameters.Add("@Maasi", SqlDbType.Decimal).Value = p.Maasi;
            komut.Parameters.Add("@GirisTarihi", SqlDbType.Date).Value = p.GirisTarihi;
            Veritabani.ESG(komut, sorgu);
            p.Islem = p.PersonelID + "nolu personelin bilgileri güncellendi.";
            p.Aciklama = "Personel güncelleme";
            Personeller.PersonelIslemEkle(p, k);
            Temizle();
            MessageBox.Show("İşlem başarılı.", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            YenileListele();
        }
    }
}
