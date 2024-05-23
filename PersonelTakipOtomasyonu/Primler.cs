using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonelTakipOtomasyonu
{
    public class Primler
    {
        #region Kapsulleme
        private int _PrimId;
        private int _PersonelId;
        private int _KullaniciId;
        private string _Donem;
        private string _OdenmeDurumu;
        private decimal _PrimTutari;
        private string _Aciklama;
        private DateTime _Tarih;
        private string _Islem;

        public int PrimId { get => _PrimId; set => _PrimId = value; }
        public int PersonelId { get => _PersonelId; set => _PersonelId = value; }
        public int KullaniciId { get => _KullaniciId; set => _KullaniciId = value; }
        public string Donem { get => _Donem; set => _Donem = value; }
        public string OdenmeDurumu { get => _OdenmeDurumu; set => _OdenmeDurumu = value; }
        public string Aciklama { get => _Aciklama; set => _Aciklama = value; }
        public DateTime Tarih { get => _Tarih; set => _Tarih = value; }
        public decimal PrimTutari { get => _PrimTutari; set => _PrimTutari = value; }
        public string Islem { get => _Islem; set => _Islem = value; }

        #endregion
        public static SqlDataReader PersonelAdSoyadGetir(TextBox txtPersonelId,TextBox txtAdSoyad)
        {
            Veritabani.baglanti.Open();
            SqlCommand komut=new SqlCommand("Select* from Personeller where PersonelId='"+txtPersonelId.Text+"'",Veritabani.baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtAdSoyad.Text = dr[1] + " " + dr[2];

            }
            Veritabani.baglanti.Close();
            return dr;
        }

    }
    
}
