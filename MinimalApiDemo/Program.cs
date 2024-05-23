var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

var m = new List<Mesailer> {
    new Mesailer{ MesaiID=1, BaslangicSaati="09.00", BitisSaati="10.00", MesaiSaatUcreti=100, Tutar=100, Donem="05.2024", OdenmeDurumu="Ödenmedi", Aciklama="Deneme", Tarih=DateTime.Parse("22.05.2024"), Islem="Yapýldý"},
    new Mesailer{MesaiID=2, BaslangicSaati="10.00", BitisSaati="12.00", MesaiSaatUcreti=200, Tutar=400, Donem="06.2024", OdenmeDurumu="Ödenmedi", Aciklama="Deneme2", Tarih=DateTime.Parse("22.06.2024"), Islem="Yapýldý2"},
    new Mesailer{MesaiID=3, BaslangicSaati="11.00", BitisSaati="16.00", MesaiSaatUcreti=10, Tutar=50, Donem="07.2024", OdenmeDurumu="Ödenmedi", Aciklama="Deneme3", Tarih=DateTime.Parse("22.07.2024"), Islem="Yapýldý3"}
};
app.MapGet("/mesailer", () => m);


var p = new List<Personeller>
{
    new Personeller{ PersonelID=1, Adi="Esengül", Soyadi="Daðbaþý", Telefon="5384771874", Adres="Yeniçiftlik", DepartmanID=1, Maasi=70000, GirisTarihi=DateTime.Parse("14.02.2024"), Aciklama="Stajyer", Email="esengul@gmail.com" },
    new Personeller{ PersonelID=2, Adi="Furkan", Soyadi="Kumbasar", Telefon="1111111111", Adres="Site", DepartmanID=3, Maasi=1, GirisTarihi=DateTime.Parse("01.04.2024"), Aciklama="Köle", Email="crayzfurkan@gmail.com" }
};
app.MapGet("/Personeller", () => p);

app.MapGet("/personeller/{personelId}", (int personelId) =>
{
    var personeller = p.Find(b => b.PersonelID == personelId);
    if (personeller is null)
        return Results.NotFound("Böyle bir kullanýcý yok.");
    return Results.Ok(personeller);
});

app.MapPost("/personeller", (Personeller personeller) =>
{
    p.Add(personeller);
    return Results.Created($"/personeller/{personeller.PersonelID}", personeller);

});


app.MapPut("/personeller/{personelId}", (int personelId, Personeller updatedPersoneller) =>
{
    var personeller = p.Find(b => b.PersonelID == personelId);
    if (personeller is null)
        return Results.NotFound("Böyle bir kullanýcý yok.");

    personeller.Islem = updatedPersoneller.Islem;
    personeller.CikisTarihi = updatedPersoneller.CikisTarihi;


    return Results.Ok(personeller);
});



app.MapGet("/mesailer/{mesaiId}", (int mesaiId) =>
{
    var mesailer = m.Find(b => b.MesaiID == mesaiId);
    if (mesailer is null)
        return Results.NotFound("Böyle bir kullanýcý yok.");
    return Results.Ok(mesailer);
});


app.MapPost("/mesailer", (Mesailer mesailer) =>
{
    m.Add(mesailer);
    return Results.Created($"/mesailer/{mesailer.MesaiID}", mesailer);

});


app.MapPut("/mesailer/{mesaiId}", (int mesaiId, Mesailer updatedMesailer) =>
{
    var mesailer = m.Find(b => b.MesaiID == mesaiId);
    if (mesailer is null)
        return Results.NotFound("Böyle bir kullanýcý yok.");

    mesailer.OdenmeDurumu = updatedMesailer.OdenmeDurumu;
    mesailer.Aciklama = updatedMesailer.Aciklama;
    mesailer.Islem = updatedMesailer.Islem;

    return Results.Ok(mesailer);
});

app.MapDelete("/mesailer/{mesaiId}", (int mesaiId) =>
{
    var mesailer = m.Find(b => b.MesaiID == mesaiId);
    if (mesailer is null)
        return Results.NotFound("Böyle bir kullanýcý yok.");

    m.Remove(mesailer);
    return Results.Ok(mesailer);
});

app.MapDelete("/mesailer", () =>
{
    m.Clear();
    return Results.Ok();
});



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
class Mesailer
{
    public int MesaiID { get; set; }
    public string BaslangicSaati { get; set; }
    public string BitisSaati { get; set; }
    public decimal MesaiSaatUcreti { get; set; }
    public decimal Tutar { get; set; }
    public string Donem { get; set; }
    public string OdenmeDurumu { get; set; }
    public string Aciklama { get; set; }
    public DateTime Tarih { get; set; }
    public string Islem { get; set; }
}

class Personeller
{
    public int PersonelID { get; set; }
    public string Adi { get; set; }
    public string Soyadi { get; set; }
    public string Telefon { get; set; }
    public string Adres { get; set; }
    public int DepartmanID { get; set; }
    public decimal Maasi { get; set; }
    public DateTime GirisTarihi { get; set; }
    public string Aciklama { get; set; }
    public string Email { get; set; }
    public DateTime Tarih { get; set; }
    public string Islem { get; set; }
    public DateTime CikisTarihi { get; set; }
}