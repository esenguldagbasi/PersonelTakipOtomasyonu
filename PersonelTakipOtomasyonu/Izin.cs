using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonelTakipOtomasyonu
{
    class Izin : Personeller
    {
        public Izin()
        {
            Izin.sql = "select* from IzinTurleri";
            Izin.value = "IzinTurId";
            Izin.text = "IzinTuru";
        }

        private int _IzinHareketId;
        private int _IzinTurId;
        private int _KullaniciId;
        private string _IzinTuru;
        private DateTime _IzinBaslangic;
        private DateTime _IzinBitis;
        private DateTime _Saat;

        public int IzinHareketId { get => _IzinHareketId; set => _IzinHareketId = value; }
        public int IzinTurId { get => _IzinTurId; set => _IzinTurId = value; }
        public int KullaniciId { get => _KullaniciId; set => _KullaniciId = value; }
        public string IzinTuru { get => _IzinTuru; set => _IzinTuru = value; }
        public DateTime IzinBaslangic { get => _IzinBaslangic; set => _IzinBaslangic = value; }
        public DateTime IzinBitis { get => _IzinBitis; set => _IzinBitis = value; }
        public DateTime Saat { get => _Saat; set => _Saat = value; }

        public static SqlDataReader ListvieweKayitGetir(ListView lst)
        {
            lst.Items.Clear();
            Veritabani.baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from IzinTurleri", Veritabani.baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = dr[0].ToString();
                ekle.SubItems.Add(dr[1].ToString());
                lst.Items.Add(ekle);
                
            }
            Veritabani.baglanti.Close();
            return dr;
            
        }
    }
}
