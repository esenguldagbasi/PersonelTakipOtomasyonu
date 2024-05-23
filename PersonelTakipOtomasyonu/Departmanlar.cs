using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PersonelTakipOtomasyonu
{
    public class Departmanlar
    {
        private int _DepartmanID;
        private string _Departman;
        private string _Aciklama;

        public int DepartmanID { get => _DepartmanID; set => _DepartmanID = value; }
        public string Departman { get => _Departman; set => _Departman = value; }
        public string Aciklama { get => _Aciklama; set => _Aciklama = value; }
        public static SqlDataReader DepartmanGetir(ListView lst)
        {
            lst.Items.Clear();
            Veritabani.baglanti.Open();
            SqlCommand komut = new SqlCommand("Select *from Departmanlar" ,Veritabani.baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read()) 
            {
            ListViewItem ekle = new ListViewItem();
                ekle.Text= dr[0].ToString();
                ekle.SubItems.Add(dr[1].ToString());
                ekle.SubItems.Add(dr[2].ToString());
                lst.Items.Add(ekle);

            }
            Veritabani.baglanti.Close();
            return dr;
        }
    }
}
