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
    public partial class frmPrimleriGoster : Form
    {
        public frmPrimleriGoster()
        {
            InitializeComponent();
        }

        private void frmPrimleriGoster_Load(object sender, EventArgs e)
        {
            Veritabani.Listele_Ara(dataGridView1, "Select* from Primler");
            int yil = int.Parse(DateTime.Now.Year.ToString());
            for (int i = yil; i >= (yil - 5); i--)
            {
                cmbYil.Items.Add(i);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["OdenmeDurumu"].Value.ToString() == "Odenmedi")
            {
                txtPrimId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtPersonelId.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtPrimTutari.Text = dataGridView1.CurrentRow.Cells["PrimTutari"].Value.ToString();
                txtAciklama.Text = dataGridView1.CurrentRow.Cells["Aciklama"].Value.ToString();
            }


        }

        private void txtPersonelId_TextChanged(object sender, EventArgs e)
        {
            Primler.PersonelAdSoyadGetir(txtPersonelId, txtAdSoyad);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDonemDegistir_Click(object sender, EventArgs e)
        {
            Kullanicilar k=new Kullanicilar();
            k.KullaniciId = Kullanicilar.kid;
            Primler p = new Primler();
            p.Donem = cmbAy.Text + "/" + cmbYil.Text;
            p.PrimId=int.Parse(txtPrimId.Text);
            p.PersonelId=int.Parse(txtPersonelId.Text);
            p.Islem = "Dönem bilgisi değişti.";
            p.Aciklama = "Seçili kaydın dönem bilgisi değişti.";
            p.Tarih=DateTime.Now;

            string sorgu = "update Primler set donem='"+p.Donem+"' where PrimId='"+p.PrimId+"'";
            SqlCommand komut = new SqlCommand();
            Veritabani.ESG(komut,sorgu);
            string sorgu2 = "insert into PrimHareketleri values ('"+k.KullaniciId+"', '"+p.PersonelId+"', '"+p.PrimId+"', '"+p.Islem+"', '"+p.Aciklama+"', @Tarih)";
            SqlCommand komut2= new SqlCommand();
            komut2.Parameters.Add("@Tarih", SqlDbType.Date).Value = p.Tarih;
            Veritabani.ESG(komut2,sorgu2);  

            Veritabani.Listele_Ara(dataGridView1, "Select* from Primler");
            MessageBox.Show("Prim için dönem değişimi yapıldı.","Dönem Değişimi",MessageBoxButtons.OK,MessageBoxIcon.Information);
            btnTemizle.PerformClick();
        }

        private void btnTumPrimleriOde_Click(object sender, EventArgs e)
        {
            Primler p = new Primler();
            p.OdenmeDurumu = "Odendi";
            

            string sorgu = "update Primler set OdenmeDurumu='" + p.OdenmeDurumu + "' where OdenmeDurumu='Odenmedi'";
            SqlCommand komut = new SqlCommand();
            Veritabani.ESG(komut, sorgu);
            for(int i = 0; i < dataGridView1.Rows.Count-1;i++)
            {
                if(dataGridView1.Rows[i].Cells["OdenmeDurumu"].Value.ToString() == "Odenmedi")
                {
                    Kullanicilar k=new Kullanicilar();
                    k.KullaniciId=Kullanicilar.kid;
                    p.PersonelId = int.Parse(dataGridView1.Rows[i].Cells["PersonelId"].Value.ToString());
                    p.PrimId = int.Parse(dataGridView1.Rows[i].Cells["PrimId"].Value.ToString()) ;
                    p.Islem = "Tüm personellerin ödenmeyen primleri ödendi.";
                    p.Aciklama = "Tüm personellerin ödenmeyen primleri ödendi.";
                    string sorgu2 = "insert into PrimHareketleri values ('" + k.KullaniciId + "', '" + p.PersonelId + "', '" + p.PrimId + "', '" + p.Islem + "', '" + p.Aciklama + "', @Tarih)";
                    SqlCommand komut2 = new SqlCommand();
                    komut2.Parameters.Add("@Tarih", SqlDbType.Date).Value = p.Tarih;
                    Veritabani.ESG(komut2, sorgu2);
                }
            }

            Veritabani.Listele_Ara(dataGridView1, "Select* from Primler");
            MessageBox.Show("Ödenmeyen tüm primler ödendi.", "Prim Ödemeleri", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnTemizle.PerformClick();
        }

        private void btnPrimOde_Click(object sender, EventArgs e)
        {
            Primler p = new Primler();
            Kullanicilar k= new Kullanicilar();
            p.PersonelId =int.Parse(txtPersonelId.Text);
            p.Aciklama =txtAciklama.Text;
            p.Islem= p.PersonelId+ "nolu personel"+ txtAdSoyad.Text+"için ödeme yapıldı.";
            k.KullaniciId =Kullanicilar.kid;
            p.OdenmeDurumu = "Odendi";
            p.PrimId = int.Parse(txtPrimId.Text);
            p.Tarih=DateTime.Now;

            string sorgu = "update Primler set OdenmeDurumu='" + p.OdenmeDurumu + "' where PrimId='" + p.PrimId + "' ";
            SqlCommand komut = new SqlCommand();
            Veritabani.ESG(komut, sorgu);
            string sorgu2 = "insert into PrimHareketleri values ('"+k.KullaniciId+"','"+p.PersonelId+"','"+p.PrimId+"','"+p.Islem+"','"+p.Aciklama+"',@Tarih)";
            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.Add("@Tarih",SqlDbType.Date).Value=p.Tarih;
            Veritabani.ESG(komut2, sorgu2);
            Veritabani.Listele_Ara(dataGridView1, "Select* from Primler");
            MessageBox.Show("Seçili kayda göre prim ödendi.", "Prim Ödemeleri", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnTemizle.PerformClick();
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

            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrimGuncelle_Click(object sender, EventArgs e)
        {
            Kullanicilar k = new Kullanicilar();
            k.KullaniciId=Kullanicilar.kid;
            Primler p = new Primler();
            
            p.PrimId = int.Parse(txtPrimId.Text);
            p.PersonelId=int.Parse(txtPersonelId.Text);
            p.PrimTutari=decimal.Parse(txtPrimTutari.Text);
            p.Aciklama=txtAciklama.Text;
            p.Islem = p.PrimId + "bilgileri değiştirildi.";

            string sorgu = "update Primler set PersonelId='"+p.PersonelId+"', PrimTutari=@PrimTutari, Aciklama='"+p.Aciklama+"' where PrimId='" + p.PrimId + "' ";
            SqlCommand komut = new SqlCommand();
            komut.Parameters.AddWithValue("@PrimTutari",SqlDbType.Decimal).Value = p.PrimTutari;
            Veritabani.ESG(komut, sorgu);
            ////////////
            string sorgu2 = "insert into PrimHareketleri values ('" + k.KullaniciId + "','" + p.PersonelId + "','" + p.PrimId + "','" + p.Islem + "','" + p.Aciklama + "',@Tarih)";
            SqlCommand komut2 = new SqlCommand();
            komut2.Parameters.Add("@Tarih", SqlDbType.Date).Value = p.Tarih;
            Veritabani.ESG(komut2, sorgu2);
            ///////////
            Veritabani.Listele_Ara(dataGridView1, "Select* from Primler");
            MessageBox.Show("Seçili kayda göre prim bilgileri güncellendi.", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnTemizle.PerformClick();
        }

        private void btnPrimSil_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Kayıt silinsin mi,?","Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes) 
            {


                Kullanicilar k = new Kullanicilar();
                k.KullaniciId = Kullanicilar.kid;
                Primler p = new Primler();

                p.PrimId = int.Parse(dataGridView1.CurrentRow.Cells["PrimId"].Value.ToString());
                p.PersonelId = int.Parse(dataGridView1.CurrentRow.Cells["PersonelId"].Value.ToString());

                p.Aciklama = "Kayıt silindi";
                p.Islem = p.PrimId + "nolu prim kaydı silindi.";

               


                string sorgu = "delete from Primler where PrimId='"+p.PrimId+"'";
                SqlCommand komut = new SqlCommand();            
                Veritabani.ESG(komut, sorgu);

                ////////////
                string sorgu2 = "insert into PrimHareketleri values ('" + k.KullaniciId + "','" + p.PersonelId + "','" + p.PrimId + "','" + p.Islem + "','" + p.Aciklama + "',@Tarih)";
                SqlCommand komut2 = new SqlCommand();
                komut2.Parameters.Add("@Tarih", SqlDbType.Date).Value = p.Tarih;
                Veritabani.ESG(komut2, sorgu2);
                ///////////


                Veritabani.Listele_Ara(dataGridView1, "Select* from Primler");
                MessageBox.Show("Prim kaydı silindi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnTemizle.PerformClick();
            }
        }
    }
}
