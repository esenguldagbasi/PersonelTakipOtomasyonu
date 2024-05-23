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
    public partial class frmPersonelMesaileri : Form
    {
        public frmPersonelMesaileri()
        {
            InitializeComponent();
        }

        private void frmPersonelMesaileri_Load(object sender, EventArgs e)
        {
            Veritabani.Listele_Ara(dataGridViewPersoneller,"Select PersonelId as ID, Adi as ADI, Soyadi as SOYADI from Personeller");

        }

        private void dataGridViewPersoneller_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string PersonelId = dataGridViewPersoneller.CurrentRow.Cells["ID"].Value.ToString();
            Veritabani.Listele_Ara(dataGridViewMesailer, "Select * from Mesailer where PersonelId='"+PersonelId+"'");
            txtPersonelId.Text = dataGridViewPersoneller.CurrentRow.Cells["ID"].Value.ToString();
            try
            {
                lblKayitSayisi.Text = "Toplam" + (dataGridViewMesailer.Rows.Count - 1) + "kayıt listelendi";
                decimal tutar = 0;
                for (int i = 0; i < dataGridViewMesailer.Rows.Count - 1; i++)
                {
                    tutar = tutar + (decimal.Parse(dataGridViewMesailer.Rows[i].Cells["Tutar"].Value.ToString()));
                }
                lblMesaiUcreti.Text = "Toplam mesai ücreti=" + tutar.ToString("0.00") + "TL.";
            }
            catch 
            {

               
            }
            
        }

        private void txtMesaiIDAra_TextChanged(object sender, EventArgs e)
        {
            Veritabani.Listele_Ara(dataGridViewMesailer, "Select * from Mesailer where MesaiId like '" + txtMesaiIDAra.Text + "'");
            if (txtMesaiIDAra.Text=="")
            {
                string PersonelId = txtPersonelId.Text;
                Veritabani.Listele_Ara(dataGridViewMesailer, "Select * from Mesailer where PersonelId='" + PersonelId + "'");
            }
        }

        private void txtPersonelId_TextChanged(object sender, EventArgs e)
        {
            Primler.PersonelAdSoyadGetir(txtPersonelId, txtPersonelAdiSoyadi);
        }
    }
}
