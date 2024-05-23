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
    public partial class frmYapilanZamlar : Form
    {
        public frmYapilanZamlar()
        {
            InitializeComponent();
            cmbPersoneller.SelectedIndex = 0;
            radioYuzde.Checked = true;
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmYapilanZamlar_Load(object sender, EventArgs e)
        {
            int yil= int.Parse(DateTime.Now.Year.ToString());
            for (int i = yil; i >= (yil - 5); i--)
            {
                cmbYil.Items.Add(i);
            }
            YapilanZamlar.ComboyaPersonelGetir(cmbPersoneller);
        }

        private void radioYuzde_CheckedChanged(object sender, EventArgs e)
        {
            txtFiyat.Enabled=false;
            txtYuzde.Enabled = true;
        }

        private void radioFiyat_CheckedChanged(object sender, EventArgs e)
        {
            txtYuzde.Enabled = false;
            txtFiyat.Enabled=true;
        }

        private void cmbPersoneller_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbPersoneller.SelectedIndex == 0)
            {
                lblPersonelId.Text = "0";
                return;
            }
            YapilanZamlar.ComboSecilirsePersonelIdGetir(cmbPersoneller, lblPersonelId);
        }

        private void btnOnay_Click(object sender, EventArgs e)
        {
            Kullanicilar k = new Kullanicilar();
            YapilanZamlar y=new YapilanZamlar();
            y.Donem = cmbAy.Text +"/"+ cmbYil.Text;
            y.Personel=cmbPersoneller.Text;
            k.KullaniciId=Kullanicilar.kid;
            
            y.Aciklama=txtAciklama.Text;
            y.Tarih=DateTime.Now;
           // decimal yuzde = decimal.Parse(txtYuzde.Text);
            //decimal fiyat= decimal.Parse(txtFiyat.Text);
            if (radioYuzde.Checked)
            {
                y.Yuzde = decimal.Parse(txtYuzde.Text);
                if (cmbPersoneller.SelectedIndex==0)
                {
                    string sql = "update Personeller set maasi=maasi+(maasi*'" + y.Yuzde + "')/100 ";
                    SqlCommand komut = new SqlCommand();
                    Veritabani.ESG(komut, sql);
                    MessageBox.Show("Tüm personellerin maaşına yüzde "+y.Yuzde+" zam yapıldı.","Maaş Zammı",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    string sql = "update Personeller set maasi=maasi+(maasi*'" + y.Yuzde + "')/100 where personelId='" + lblPersonelId.Text + "'";
                    SqlCommand komut = new SqlCommand();
                    Veritabani.ESG(komut, sql);
                    MessageBox.Show(""+cmbPersoneller.Text+" personelinin maaşına yüzde " + y.Yuzde + " zam yapıldı.", "Maaş Zammı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                string sql2 = "insert into YapilanZamlar (KullaniciId, Donem, Personeller, Yuzde, Aciklama, Tarih) values ('"+k.KullaniciId+"', '"+y.Donem+"','"+y.Personel+"',@Yuzde,'"+y.Aciklama+"',@Tarih)";

                SqlCommand komut2= new SqlCommand();
                komut2.Parameters.Add("@Yuzde",SqlDbType.Decimal).Value=y.Yuzde;
                komut2.Parameters.Add("@Tarih", SqlDbType.Date).Value = y.Tarih;
                Veritabani.ESG(komut2, sql2);
            }

            if(radioFiyat.Checked)
            {
                y.Fiyat = decimal.Parse(txtFiyat.Text);
                if (cmbPersoneller.SelectedIndex == 0)
                {
                    string sql = "update Personeller set maasi=maasi+'"+y.Fiyat+"'";
                    SqlCommand komut = new SqlCommand();
                    Veritabani.ESG(komut, sql);
                    MessageBox.Show("Tüm personellerin maaşına  " + y.Fiyat + " TL zam yapıldı.", "Maaş Zammı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string sql = "update Personeller set maasi=maasi+'" + y.Fiyat + "' where personelId='"+lblPersonelId.Text+"'";
                    SqlCommand komut = new SqlCommand();
                    Veritabani.ESG(komut, sql);
                    MessageBox.Show("" + cmbPersoneller.Text + " personelinin maaşına  " + y.Fiyat + " TL zam yapıldı.", "Maaş Zammı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                string sql2 = "insert into YapilanZamlar (KullaniciId, Donem, Personeller, Fiyat, Aciklama, Tarih) values ('" + k.KullaniciId + "', '" + y.Donem + "','" + y.Personel + "',@Fiyat,'" + y.Aciklama + "',@Tarih)";

                SqlCommand komut2 = new SqlCommand();
                komut2.Parameters.Add("@Fiyat", SqlDbType.Decimal).Value = y.Fiyat;
                komut2.Parameters.Add("@Tarih", SqlDbType.Date).Value = y.Tarih;
                Veritabani.ESG(komut2, sql2);
            }
        }
    }
}
