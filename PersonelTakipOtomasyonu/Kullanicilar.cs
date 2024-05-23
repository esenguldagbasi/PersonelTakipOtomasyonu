using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PersonelTakipOtomasyonu
{
    public class Kullanicilar
    {
        private int _KullaniciId;
        private string _KullaniciAdi;
        private string _Sifre;
        private string _AdiSoyadi;
        private string _Soru;
        private string _Cevap;
        private string _Aciklama;
        private DateTime _Tarih;

        public int KullaniciId { get => _KullaniciId; set => _KullaniciId = value; }
        public string KullaniciAdi { get => _KullaniciAdi; set => _KullaniciAdi = value; }
        public string Sifre { get => _Sifre; set => _Sifre = value; }
        public string AdiSoyadi { get => _AdiSoyadi; set => _AdiSoyadi = value; }
        public string Soru { get => _Soru; set => _Soru = value; }
        public string Cevap { get => _Cevap; set => _Cevap = value; }
        public string Aciklama { get => _Aciklama; set => _Aciklama = value; }
        public DateTime Tarih { get => _Tarih; set => _Tarih = value; }

        public static bool durum = false;
        public static int kid = 0;
        public static SqlDataReader KullaniciGirisi(string kullaniciadi, string sifre)
        {
            Kullanicilar k=new Kullanicilar();
            k._KullaniciAdi=kullaniciadi;
            k._Sifre=sifre;
            Veritabani.baglanti.Open();
            SqlCommand komut = new SqlCommand("Select* from Kullanicilar where KullaniciAdi='"+k._KullaniciAdi+"' and Sifre='"+k._Sifre+"'",Veritabani.baglanti);
            SqlDataReader dr= komut.ExecuteReader();
            if(dr.Read())
            {
                durum= true;
                kid = int.Parse(dr[0].ToString());
            }
            else
            {
                durum = false;
            }
            dr.Close();
            Veritabani.baglanti.Close();
            return dr;

        }
    }
}
