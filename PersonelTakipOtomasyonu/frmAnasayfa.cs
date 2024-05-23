using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonelTakipOtomasyonu
{
    public partial class frmAnasayfa : Form
    {
        public frmAnasayfa()
        {
            InitializeComponent();
        }

        private void btnDepartmanlar_Click(object sender, EventArgs e)
        {
            frmDepartmanlar frm= new frmDepartmanlar();
            frm.ShowDialog();
            //panelIslemler.Width = 80;
        }

        private void btnPersonelEkle_Click(object sender, EventArgs e)
        {
            frmPersonelEkle frm= new frmPersonelEkle();
            frm.ShowDialog();
        }

        private void btnPersonelListele_Click(object sender, EventArgs e)
        {
            frmPersonelListele frm= new frmPersonelListele();
            frm.ShowDialog();
        }

        private void frmAnasayfa_Load(object sender, EventArgs e)
        {
            frmKullanici k= new frmKullanici();
            k.ShowDialog();
        }

        private void btnMaasZamlari_Click(object sender, EventArgs e)
        {
            frmYapilanZamlar frm= new frmYapilanZamlar();
            frm.ShowDialog();
        }

        private void btnPrimler_Click(object sender, EventArgs e)
        {
            frmPrimEkle frm= new frmPrimEkle();
            frm.ShowDialog();
        }

        private void btnMesaiEkle_Click(object sender, EventArgs e)
        {
            frmMesaiEkle frm= new frmMesaiEkle();
            frm.ShowDialog();
        }

        private void btnMesailer_Click(object sender, EventArgs e)
        {
            frmMesailer frm = new frmMesailer();
            frm.ShowDialog();
        }

      

        private void btnIzinHareketListele_Click(object sender, EventArgs e)
        {
            frmIzinHareketleri frm=new frmIzinHareketleri();
            frm.ShowDialog();

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            //if (panelIslemler.Width==170)
            //{
            //    panelIslemler.Width = 80;
            //}
            //else if (panelIslemler.Width==80)
            //{
            //    panelIslemler.Width = 170;
            //}
        }
    }
}
