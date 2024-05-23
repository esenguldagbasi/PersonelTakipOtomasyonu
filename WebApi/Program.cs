using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var k = new List<Kullanicilar> {
    new Kullanicilar{Id=1, KullaniciAdi="Esengül", Sifre="111", AdiSoyadi="Esengül Daðbaþý", Soru="Yaþýnýz?", Cevap="25", Aciklama="deneme", Tarih=DateTime.Parse("2024-05-21")},
    new Kullanicilar{ Id=2, KullaniciAdi="Furkan", Sifre="222", AdiSoyadi="Furkan Kumbasar", Soru="En büyük takým?", Cevap="FENERBAHÇE", Aciklama="Adam", Tarih=DateTime.Parse("2024-05-20")},
    new Kullanicilar{Id =3, KullaniciAdi="Esoþ", Sifre="333", AdiSoyadi="Esoþ Baþkan", Soru="Favori renginiz?", Cevap="Sarý Lacivert", Aciklama="Reis", Tarih=DateTime.Parse("1999-04-14")}
};

app.MapGet("/kullanicilar", () => k);

app.MapGet("/kullanicilar/{id}", (int id) =>
{
    var kullanicilar = k.Find(b => b.Id == id);
    if (kullanicilar is null)
        return Results.NotFound("Böyle bir kullanýcý yok.");
    return Results.Ok(kullanicilar);
});

app.MapPost("/kullanicilar", (Kullanicilar kullanicilar) =>
{
    k.Add(kullanicilar);
    return Results.Created($"/kullanicilar/{kullanicilar.Id}", kullanicilar);
});

app.MapPut("/kullanicilar/{id}", (int id, Kullanicilar updatedKullanicilar) =>
{
    var kullanicilar = k.Find(b => b.Id == id);
    if (kullanicilar is null)
        return Results.NotFound("Böyle bir kullanýcý yok.");

    kullanicilar.AdiSoyadi = updatedKullanicilar.AdiSoyadi;
    kullanicilar.Aciklama = updatedKullanicilar.Aciklama;

    return Results.Ok(kullanicilar);
});

app.MapDelete("/kullanicilar/{id}", (int id) =>
{
    var kullanicilar = k.Find(b => b.Id == id);
    if (kullanicilar is null)
        return Results.NotFound("Böyle bir kullanýcý yok.");

    k.Remove(kullanicilar);
    return Results.Ok(kullanicilar);
});

app.MapDelete("/kullanicilar", () =>
{
    k.Clear();
    return Results.Ok();
});

app.Run();
public partial class Program
{
    class Kullanicilar
    {
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string AdiSoyadi { get; set; }
        public string Soru { get; set; }
        public string Cevap { get; set; }
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
    }



    static async Task Main(string[] args)
    {
        // Test GET request to fetch all users
        await GetKullanicilar();

        // Test GET request to fetch a user by ID
        await GetKullaniciById(1);

        // Test POST request to create a new user
        var yeniKullanici = new Kullanicilar
        {
            Id = 4,
            KullaniciAdi = "YeniKullanici",
            Sifre = "444",
            AdiSoyadi = "Yeni Kullanici",
            Soru = "Yeni Soru",
            Cevap = "Yeni Cevap",
            Aciklama = "Yeni Aciklama",
            Tarih = DateTime.Now
        };
        await CreateKullanici(yeniKullanici);

        // Test PUT request to update a user
        var guncelKullanici = new Kullanicilar
        {
            AdiSoyadi = "Güncellenmiþ Ad Soyad",
            Aciklama = "Güncellenmiþ Açýklama"
        };
        await UpdateKullanici(1, guncelKullanici);

        // Test DELETE request to delete a user by ID
        await DeleteKullanici(4);

        // Test DELETE request to delete all users
        await DeleteAllKullanicilar();
    }

    private static async Task GetKullanicilar()
    {
        var client = new RestClient("http://localhost:5138");
        var request = new RestRequest("/kullanicilar", Method.Get);
        var response = await client.ExecuteAsync<List<Kullanicilar>>(request);

        if (response.IsSuccessful)
        {
            var kullanicilar = response.Data;
            Console.WriteLine("Kullanicilar:");
            kullanicilar.ForEach(k => Console.WriteLine($"{k.Id}: {k.KullaniciAdi} - {k.AdiSoyadi}"));
        }
        else
        {
            Console.WriteLine("Kullanýcýlar alýnamadý: " + response.ErrorMessage);
        }
    }

    private static async Task GetKullaniciById(int id)
    {
        var client = new RestClient("http://localhost:5138");
        var request = new RestRequest($"/kullanicilar/{id}", Method.Get);
        var response = await client.ExecuteAsync<Kullanicilar>(request);

        if (response.IsSuccessful)
        {
            var kullanici = response.Data;
            Console.WriteLine($"Kullanici {id}: {kullanici.KullaniciAdi} - {kullanici.AdiSoyadi}");
        }
        else
        {
            Console.WriteLine("Kullanýcý alýnamadý: " + response.ErrorMessage);
        }
    }

    private static async Task CreateKullanici(Kullanicilar kullanici)
    {
        var client = new RestClient("http://localhost:5138");
        var request = new RestRequest("/kullanicilar", Method.Post);
        request.AddJsonBody(kullanici);
        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine("Kullanýcý oluþturuldu.");
        }
        else
        {
            Console.WriteLine("Kullanýcý oluþturulamadý: " + response.ErrorMessage);
        }
    }

    private static async Task UpdateKullanici(int id, Kullanicilar kullanici)
    {
        var client = new RestClient("http://localhost:5138");
        var request = new RestRequest($"/kullanicilar/{id}", Method.Put);
        request.AddJsonBody(kullanici);
        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine("Kullanýcý güncellendi.");
        }
        else
        {
            Console.WriteLine("Kullanýcý güncellenemedi: " + response.ErrorMessage);
        }
    }

    private static async Task DeleteKullanici(int id)
    {
        var client = new RestClient("http://localhost:5138");
        var request = new RestRequest($"/kullanicilar/{id}", Method.Delete);
        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine("Kullanýcý silindi.");
        }
        else
        {
            Console.WriteLine("Kullanýcý silinemedi: " + response.ErrorMessage);
        }
    }

    private static async Task DeleteAllKullanicilar()
    {
        var client = new RestClient("http://localhost:5138");
        var request = new RestRequest("/kullanicilar", Method.Delete);
        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful)
        {
            Console.WriteLine("Tüm kullanýcýlar silindi.");
        }
        else
        {
            Console.WriteLine("Tüm kullanýcýlar silinemedi: " + response.ErrorMessage);
        }
    }
}


//public class Kullanicilar
//{
//    public int Id { get; set; }
//    public string KullaniciAdi { get; set; }
//    public string Sifre { get; set; }
//    public string AdiSoyadi { get; set; }
//    public string Soru { get; set; }
//    public string Cevap { get; set; }
//    public string Aciklama { get; set; }
//    public DateTime Tarih { get; set; }
//}
