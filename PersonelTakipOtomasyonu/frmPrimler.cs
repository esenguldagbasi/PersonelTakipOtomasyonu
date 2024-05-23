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
    public partial class frmPrimEkle : Form
    {
        public frmPrimEkle()
        {
            InitializeComponent();
        }

        private void frmPrimler_Load(object sender, EventArgs e)
        {
            int yil = int.Parse(DateTime.Now.Year.ToString());
            for (int i = yil; i >= (yil - 5); i--)
            {
                cmbYil.Items.Add(i);
                Veritabani.Listele_Ara(dataGridView1, "Select PersonelId, Adi, Soyadi, Maasi, GirisTarihi from Personeller");
            }
            
        }

        private void btnPrimEkle_Click(object sender, EventArgs e)
        {
            Primler p= new Primler();
            p.KullaniciId=Kullanicilar.kid;
            
            p.Donem=cmbAy.Text+"/"+cmbYil.Text;
            p.PrimTutari=decimal.Parse(txtPrimTutari.Text);
            p.Aciklama=txtAciklama.Text;
            p.Tarih=DateTime.Now;
            if(radioKisiyeOzel.Checked)
            {
                p.PersonelId = int.Parse(txtPersonelId.Text);
                string sql = "insert into Primler (KullaniciId, PersonelId, Donem, PrimTutari, Aciklama, Tarih) values ('"+p.KullaniciId+"','"+p.PersonelId+"','"+p.Donem+"',@PTutari,'"+p.Aciklama+"',@Tarih)";
                SqlCommand komut = new SqlCommand();
                komut.Parameters.Add("@PTutari",SqlDbType.Decimal).Value=p.PrimTutari;
                komut.Parameters.Add("@Tarih",SqlDbType.Date).Value=p.Tarih;
                Veritabani.ESG(komut,sql);
                MessageBox.Show("İşlem başarılı","Prim ekleme",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else if (radioTumPersoneller.Checked)
            {
                for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
                {
                    string sql = "insert into Primler (KullaniciId, PersonelId, Donem, PrimTutari, Aciklama, Tarih) values ('" + p.KullaniciId + "','" + p.PersonelId + "','" + p.Donem + "',@PTutari,'" + p.Aciklama + "',@Tarih)";
                    SqlCommand komut = new SqlCommand();
                    komut.Parameters.Add("@PTutari", SqlDbType.Decimal).Value = p.PrimTutari;
                    komut.Parameters.Add("@Tarih", SqlDbType.Date).Value = p.Tarih;
                    Veritabani.ESG(komut, sql);
                    MessageBox.Show("İşlem başarılı", "Prim ekleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                btnTemizle.PerformClick();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtPersonelId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString()+" "+ dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnPrimGoster_Click(object sender, EventArgs e)
        {
            frmPrimleriGoster frm = new frmPrimleriGoster();
            frm.ShowDialog();
        }

        private void btnPersoneleGorePrimler_Click(object sender, EventArgs e)
        {
            frmPersoneleGorePrimler frm = new frmPersoneleGorePrimler();
            frm.txtPersonelId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            frm.txtAdSoyad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString()+" " + dataGridView1.CurrentRow.Cells[2].Value.ToString();
            Veritabani.Listele_Ara(frm.dataGridView1, "select* from Primler where PersonelId='" + frm.txtPersonelId.Text + "'");

            frm.ShowDialog();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if(item is ComboBox)
                {
                    item.Text = "";
                }
                    
            }
        }
    }
}
