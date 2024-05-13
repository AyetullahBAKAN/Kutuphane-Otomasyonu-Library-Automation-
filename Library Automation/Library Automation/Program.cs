using System;
using System.Collections.Generic;
using System.Linq;
public class GraphNode
{
    public Kitap Kitap;
    public GraphNode Next;

    public GraphNode(Kitap kitap)
    {
        Kitap = kitap;
        Next = null;
    }
}
// Node yapısı
public class Node
{
    public string Data;
    public Node Next;

    public Node(string data)
    {
        Data = data;
        Next = null;
    }
}

// Stack yapısı
public class MyStack
{
    private Node top;

    public MyStack()
    {
        top = null;
    }

    public void Push(string data)
    {
        Node newNode = new Node(data);
        newNode.Next = top;
        top = newNode;
    }

    public string Pop()
    {
        if (IsEmpty())
        {
            Console.WriteLine("Stack boş, Pop işlemi gerçekleştirilemedi.");
            return null;
        }
        string poppedData = top.Data;
        top = top.Next;
        return poppedData;
    }

    public bool IsEmpty()
    {
        return top == null;
    }

    public void Display(int count)
    {
        Node current = top;
        int displayCount = 0;

        Console.WriteLine($"Son {count} işlem:");

        while (current != null && displayCount < count)
        {
            Console.WriteLine(current.Data);
            current = current.Next;
            displayCount++;
        }
    }
}



// Kitap yapısı


// Personel yapısı
public class Personel
{
    public int Id;
    public string Isim;
    public Personel Sonraki;
}

// Üye yapısı
public class Uye
{
    public int Id;
    public string Isim;
    public Uye Sonraki;
    public Kitap AldigiKitaplarBasi; // Aldığı kitapların başını gösteren bağlı liste başlangıcı
}

// Kitap yapısı
// Kitap yapısı
public class Kitap
{
    public int Id;
    public string Baslik;
    public KitapTuru Turu; // Kitap türü eklendi
    public Kitap Sonraki;
}
public enum KitapTuru
{
    Roman,
    Hikaye,
    Masal,
    Korku,
    Bilimsel,
    Dram
}

// BST için ekstra sınıf
public class BSTNode
{
    public Kitap Kitap;
    public BSTNode Sol;
    public BSTNode Sag;

    public BSTNode(Kitap kitap)
    {
        Kitap = kitap;
        Sol = null;
        Sag = null;
    }
}

class Program
{
    public static MyStack islemGecmisi = new MyStack();

    static void IslemGecmisiEkle(string islem)
    {
        islemGecmisi.Push(islem);
    }

    static void IslemGecmisiGoster(int sayi)
    {
        islemGecmisi.Display(sayi);
    }

    static Dictionary<KitapTuru, GraphNode> grafDict = new Dictionary<KitapTuru, GraphNode>();


    // bağlı liste başlangıçları
    static Personel personelBasi = null;
    static Uye uyeBasi = null;
    static Kitap kitapBasi = null;
    static BSTNode kitapBST = null; // BST başlangıcı
    public class EtkinlikKatilimci
    {
        public int Id;
        public string Isim;
        public EtkinlikKatilimci Sonraki;
    }
    static EtkinlikKatilimci etkinlikKuyruguBasi = null;
    // Özlü sözlerin listesi
    static List<string> ozluSozler = new List<string>
    {
        "Yapabileceğin en iyi şey, doğru olanı yapmaktır. İkinci en iyi şey yanlış olanı yapmaktır. En kötüsü ise hiçbir şey yapmamaktır. - Theodore Roosevelt",
        "Hayatımızı değiştirecek olan şeyler, biz farkında olmadan hayatımıza girer. - Haruki Murakami",
        "Bir insanın kaderi, o insanın karakterindedir. - Herakleitos",
        "Bilgi güçtür. - Francis Bacon",
        "Zihin her şeydir. Ne düşünürseniz o olursunuz. - Buddha",
        "Başarı asla tesadüf değildir. - Pele",
        "Hayat, yaşamaya değer olduğunu kanıtlamak için bir fırsattır. - Sigmund Freud",
        "Bir kitap, bir bahçeye, bir kitaplık ise bir cennete eşdeğerdir. - Cicero",
        "Kendinizi başkalarıyla kıyaslamayın. Eğer öyle yaparsanız, kendinizi küçümsemiş olursunuz. - Bill Gates",
        "Geleceği tahmin etmenin en iyi yolu onu yaratmaktır. - Peter Drucker"
    };
    static void Main(string[] args)
    {

        int secim;
        string isim;
        int id;


        do
        {
            MenuGoster();
            Console.Write("Seciminizi giriniz: ");
            secim = Convert.ToInt32(Console.ReadLine());
            switch (secim)
            {
                case 1:
                    Console.Write("Personel ID giriniz: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Personel ismi giriniz: ");
                    isim = Console.ReadLine();
                    PersonelEkle(id, isim);
                    break;
                case 2:
                    Console.Write("Silinecek personel ID giriniz: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    PersonelCikar(id);
                    break;
                case 3:
                    Console.Write("Üye ID giriniz: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Üye ismi giriniz: ");
                    isim = Console.ReadLine();
                    UyeEkle(id, isim);
                    break;
                case 4:
                    Console.Write("Silinecek üye ID giriniz: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    UyeCikar(id);
                    break;
                case 5:
                    Console.Write("Kitap ID giriniz: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Kitap başlığı giriniz: ");
                    isim = Console.ReadLine();
                    Console.WriteLine("Kitap türünü giriniz (Roman, Hikaye, Masal, Korku, Bilimsel, Dram): ");
                    KitapTuru turu = (KitapTuru)Enum.Parse(typeof(KitapTuru), Console.ReadLine(), true);
                    KitapEkle(id, isim, turu);
                    break;

                case 6:
                    Console.Write("Silinecek kitap ID giriniz: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    KitapCikar(id);
                    break;
                case 7:
                    PersonelleriListele();
                    break;
                case 8:
                    UyeleriListele();
                    break;
                case 9:
                    KitaplariListele();
                    break;
                case 10:
                    Console.Write("Üye ismini giriniz: ");
                    isim = Console.ReadLine();
                    Console.Write("Kitap başlığını giriniz: ");
                    string baslik = Console.ReadLine();
                    KitapAlimi(isim, baslik);
                    break;
                case 11:
                    Console.Write("Kontrol edilecek üye ismini giriniz: ");
                    isim = Console.ReadLine();
                    UyeKontrolu(isim);
                    break;
                case 12:
                    Console.Write("Bulunacak üye ismini giriniz: ");
                    isim = Console.ReadLine();
                    Uye uye = UyeBul(isim);
                    if (uye != null)
                    {
                        Console.WriteLine($"Üye bulundu: ID: {uye.Id}, Isim: {uye.Isim}");
                    }
                    else
                    {
                        Console.WriteLine("Üye bulunamadı.");
                    }
                    break;
                case 13:
                    Console.Write("Bulunacak kitap başlığını giriniz: ");
                    baslik = Console.ReadLine();
                    Kitap kitap = KitapBul(baslik);
                    if (kitap != null)
                    {
                        Console.WriteLine($"Kitap bulundu: ID: {kitap.Id}, Başlık: {kitap.Baslik}");
                    }
                    else
                    {
                        Console.WriteLine("Kitap bulunamadı.");
                    }
                    break;
                case 14:
                    Console.Write("Kitap tavsiyesi için üye ismini giriniz: ");
                    string uyeIsim = Console.ReadLine();
                    KitapTavsiye(uyeIsim);
                    break;
                case 15:
                    EtkinlikKuyruguMenu();
                    break;
                case 16:
                    Console.WriteLine("Aranacak kitap türünü giriniz (Roman, Hikaye, Masal, Korku, Bilimsel, Dram): ");
                    KitapTuru arananTuru = (KitapTuru)Enum.Parse(typeof(KitapTuru), Console.ReadLine(), true);
                    TureGoreKitaplariListele(arananTuru);
                    break;
                case 17:
                    Console.Write("son kaç işlem : ");
                    int sayi = Convert.ToInt32(Console.ReadLine());

                    IslemGecmisiGoster(sayi);
                    break;
                case 18:
                    Console.WriteLine("███████████████████████████████");
                    Console.WriteLine("█   Projeyi Hazırlayanlar     █");
                    Console.WriteLine("███████████████████████████████");
                    Console.WriteLine("█ -> Dına Valedıka            █");
                    Console.WriteLine("█ -> Ayetullah BAKAN          █");
                    Console.WriteLine("█ -> Mahsa Omidvar Gharehbaba █");
                    Console.WriteLine("█ -> Zeynep Alperen           █");
                    Console.WriteLine("███████████████████████████████");
                    break;
                case 0:
                    Console.WriteLine("Çıkış yapılıyor...");
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }
        } while (secim != 0);
    }

    static void MenuGoster()
    {

        Console.ForegroundColor = ConsoleColor.DarkYellow;

        Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                 Bursa Uludag Universitesi Kütüphane Otomasyonu                  ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════╝");
        // Rastgele bir özlü söz göster
        Random rnd = new Random();
        int index = rnd.Next(ozluSozler.Count);
        Console.WriteLine($"{ozluSozler[index]}");
        Console.WriteLine("\n");
        Console.WriteLine("███████████████████████████████████████████████████████████████████████████████████");
        Console.WriteLine("█░░░░░░██████████████████████████████░░░░░░░░░░░░░░░░░░░░░████░░░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░░░░█████   ISLEM MENUSU   ███████░░░░░░░░░░░░░░░░░░░░███░██░░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░░░░██████████████████████████████░░░░░░░░░░░░░░░░░░░░██░░░█░░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░░░░█ 1.  Personel Ekle          █░░░░░░░░░░░░░░░░░░░░██░░░██░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░░░░█ 2.  Personel Çıkar         █░░░░░░░░░░░░░░░░░░░░░██░░░███░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░░░░█ 3.  Üye Ekle               █░░░░░░░░░░░░░░░░░░░░░░██░░░░██░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░░░░█ 4.  Üye Çıkar              █░░░░░░░░░░░░░░░░░░░░░░██░░░░░███░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░░░░█ 5.  Kitap Ekle             █░░░░░░░░░░░░░░░░░░░░░░░██░░░░░░██░░░░░░░░░░░░█");
        Console.WriteLine("█░░░░░░█ 6.  Kitap Çıkar            █░░░░░░░░░░░░░░░░░░███████░░░░░░░██░░░░░░░░░░░█");
        Console.WriteLine("█░░░░░░█ 7.  Personelleri Listele   █░░░░░░░░░░░░░░░█████░░░░░░░░░░░░░░███░██░░░░░█");
        Console.WriteLine("█░░░░░░█ 8.  Üyeleri Listele        █░░░░░░░░░░░░░░██░░░░░████░░░░░░░░░░██████░░░░█");
        Console.WriteLine("█░░░░░░█ 9.  Kitapları Listele      █░░░░░░░░░░░░░░██░░████░░███░░░░░░░░░░░░░██░░░█");
        Console.WriteLine("█░░░░░░█ 10. Kitap Alımı            █░░░░░░░░░░░░░░██░░░░░░░░███░░░░░░░░░░░░░██░░░█");
        Console.WriteLine("█░░░░░░█ 11. Üye Kontrolü           █░░░░░░░░░░░░░░░██████████░███░░░░░░░░░░░██░░░█");
        Console.WriteLine("█░░░░░░█ 12. Üye Bul                █░░░░░░░░░░░░░░░██░░░░░░░░████░░░░░░░░░░░██░░░█");
        Console.WriteLine("█░░░░░░█ 13. Kitap Bul              █░░░░░░░░░░░░░░░███████████░░██░░░░░░░░░░██░░░█");
        Console.WriteLine("█░░░░░░█ 14. Kitap Tavsiye          █░░░░░░░░░░░░░░░░░██░░░░░░░████░░░░░██████░░░░█");
        Console.WriteLine("█░░░░░░█ 15. Etkinlik Kuyruğu       █░░░░░░░░░░░░░░░░░██████████░██░░░░███░██░░░░░█");
        Console.WriteLine("█░░░░░░█ 16. Ture Gore Arama        █░░░░░░░░░░░░░░░░░░░░██░░░░░████░███░░░░░░░░░░█");
        Console.WriteLine("█░░░░░░█ 17. Islem Gecmisi          █░░░░░░░░░░░░░░░░░░░░█████████████░░░░░░░░░░░░█");
        Console.WriteLine("█░░░░░░█ 18. Hazırlayanlar          █░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░░░░█ 0.  Çıkış                  █░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░░░░██████████████████████████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░░░░██████████████████████████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("███████████████████████████████████████████████████████████████████████████████████");
        Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                 Bursa Uludag Universitesi Kütüphane Otomasyonu                  ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════╝");
    }

    // Personel fonksiyonlarının implementasyonları
    static void PersonelEkle(int id, string isim)
    {
        // Aynı ID'ye sahip bir personel var mı kontrol et
        if (PersonelVarMi(id))
        {
            Console.WriteLine("Hata: Bu ID'ye sahip bir personel zaten var.");
            return;
        }

        // Personel ekleyen fonksiyon
        Personel yeniPersonel = new Personel { Id = id, Isim = isim, Sonraki = personelBasi };
        personelBasi = yeniPersonel;
        IslemGecmisiEkle("Personel Eklendi");
    }


    static void PersonelCikar(int id)
    {
        // Personel çıkaran fonksiyon
        Personel temp = personelBasi, onceki = null;
        while (temp != null && temp.Id != id)
        {
            onceki = temp;
            temp = temp.Sonraki;
        }
        if (temp == null) return;
        if (onceki == null)
        {
            personelBasi = temp.Sonraki;
        }
        else
        {
            onceki.Sonraki = temp.Sonraki;
        }
        IslemGecmisiEkle("Personel Cikarildi");
    }

    static void PersonelleriListele()
    {
        // Personelleri listeleme fonksiyonu
        Personel temp = personelBasi;
        while (temp != null)
        {
            Console.WriteLine($"ID: {temp.Id}, Isim: {temp.Isim}");
            temp = temp.Sonraki;
        }
        IslemGecmisiEkle("Personel Listelendi");
    }

    // Üye fonksiyonlarının implementasyonları
    static void UyeEkle(int id, string isim)
    {
        // Aynı ID'ye sahip bir üye var mı kontrol et
        if (UyeVarMi(id))
        {
            Console.WriteLine("Hata: Bu ID'ye sahip bir üye zaten var.");
            return;
        }

        // Üye ekleyen fonksiyon
        Uye yeniUye = new Uye { Id = id, Isim = isim, Sonraki = uyeBasi };
        uyeBasi = yeniUye;
        IslemGecmisiEkle("Uye Eklendi");
    }

    static void UyeCikar(int id)
    {
        // Üye çıkaran fonksiyon
        Uye temp = uyeBasi, onceki = null;
        while (temp != null && temp.Id != id)
        {
            onceki = temp;
            temp = temp.Sonraki;
        }
        if (temp == null) return;
        if (onceki == null)
        {
            uyeBasi = temp.Sonraki;
        }
        else
        {
            onceki.Sonraki = temp.Sonraki;
        }
        IslemGecmisiEkle("Uye Cikarildi");
    }

    static void UyeleriListele()
    {
        // Üyeleri listeleme fonksiyonu
        Uye temp = uyeBasi;
        while (temp != null)
        {
            Console.WriteLine($"ID: {temp.Id}, Isim: {temp.Isim}");
            temp = temp.Sonraki;
        }
        IslemGecmisiEkle("Uye Listelendi");
    }

    // Kitap fonksiyonlarının implementasyonları
    static void KitapEkle(int id, string baslik, KitapTuru turu)
    {
        // Aynı ID'ye sahip bir kitap var mı kontrol et
        if (KitapVarMi(id))
        {
            Console.WriteLine("Hata: Bu ID'ye sahip bir kitap zaten var.");
            return;
        }

        Kitap yeniKitap = new Kitap { Id = id, Baslik = baslik, Turu = turu, Sonraki = kitapBasi };
        kitapBasi = yeniKitap;

        // Grafı güncelle
        if (!grafDict.ContainsKey(turu))
        {
            grafDict[turu] = new GraphNode(yeniKitap);
        }
        else
        {
            GraphNode temp = grafDict[turu];
            while (temp.Next != null)
            {
                temp = temp.Next;
            }
            temp.Next = new GraphNode(yeniKitap);
        }

        kitapBST = KitapBSTEkle(kitapBST, yeniKitap);
        IslemGecmisiEkle("Kitap Eklendi");
    }

    static void TureGoreKitaplariListele(KitapTuru tur)
    {
        if (kitapBasi == null)
        {
            Console.WriteLine("Listelenecek kitap bulunamadı.");
        }
        else
        {
            Kitap temp = kitapBasi;
            while (temp != null)
            {
                if (temp.Turu == tur)
                {
                    Console.WriteLine($"ID: {temp.Id}, Başlık: {temp.Baslik}, Türü: {temp.Turu}");
                }
                temp = temp.Sonraki;
            }
        }
        IslemGecmisiEkle("Ture Gore Kitap Listelendi");
    }



    // BST'ye kitap ekleyen fonksiyon
    static BSTNode KitapBSTEkle(BSTNode node, Kitap kitap)
    {
        if (node == null)
        {
            return new BSTNode(kitap);
        }

        if (kitap.Id < node.Kitap.Id)
        {
            node.Sol = KitapBSTEkle(node.Sol, kitap);
        }
        else if (kitap.Id > node.Kitap.Id)
        {
            node.Sag = KitapBSTEkle(node.Sag, kitap);
        }

        return node;
    }

    static void KitapTavsiye(string uyeIsim)
    {
        Uye uye = UyeBul(uyeIsim);
        if (uye == null)
        {
            Console.WriteLine("Üye bulunamadı.");
            return;
        }

        var tavsiyeEdilecekKitaplar = new List<Kitap>();
        TavsiyeEdilenKitaplariBul(kitapBST, uye.AldigiKitaplarBasi, tavsiyeEdilecekKitaplar);

        // Rastgele 3 kitap seçmek için tavsiye listesini karıştır
        var random = new Random();
        var rastgeleTavsiyeler = tavsiyeEdilecekKitaplar.OrderBy(x => random.Next()).Take(3).ToList();

        if (rastgeleTavsiyeler.Count == 0)
        {
            Console.WriteLine("Yeterli sayıda yeni kitap bulunamadı.");
            return;
        }

        Console.WriteLine("Kitap Tavsiyeleri:");
        foreach (var kitap in rastgeleTavsiyeler)
        {
            Console.WriteLine($"- {kitap.Baslik}");
        }
        IslemGecmisiEkle("Kitap Tavsiyesi Alındı");
    }

    // Tavsiye edilen kitapları bulan fonksiyon (limit parametresi kaldırıldı)
    static void TavsiyeEdilenKitaplariBul(BSTNode node, Kitap aldigiKitaplarBasi, List<Kitap> list)
    {
        if (node == null)
            return;

        // Üyenin aldığı kitaplar listesinde olmayan kitapları bul
        if (!AldigiKitaplardaVarMi(aldigiKitaplarBasi, node.Kitap.Id))
        {
            list.Add(node.Kitap);
        }

        TavsiyeEdilenKitaplariBul(node.Sol, aldigiKitaplarBasi, list);
        TavsiyeEdilenKitaplariBul(node.Sag, aldigiKitaplarBasi, list);
    }


    // Üyenin aldığı kitaplar listesinde belirli bir kitabın olup olmadığını kontrol eden fonksiyon
    static bool AldigiKitaplardaVarMi(Kitap aldigiKitaplarBasi, int kitapId)
    {
        Kitap temp = aldigiKitaplarBasi;
        while (temp != null)
        {
            if (temp.Id == kitapId)
            {
                return true;
            }
            temp = temp.Sonraki;
        }
        return false;
    }

    static void KitapCikar(int id)
    {
        // Kitap çıkaran fonksiyon
        Kitap temp = kitapBasi, onceki = null;
        while (temp != null && temp.Id != id)
        {
            onceki = temp;
            temp = temp.Sonraki;
        }
        if (temp == null) return;
        if (onceki == null)
        {
            kitapBasi = temp.Sonraki;
        }
        else
        {
            onceki.Sonraki = temp.Sonraki;
        }
        IslemGecmisiEkle("Kitap Cikarildi");
    }

    static void KitaplariListele()
    {
        if (kitapBasi == null)
        {
            Console.WriteLine("Listelenecek kitap bulunamadı.");
        }
        else
        {
            Kitap temp = kitapBasi;
            while (temp != null)
            {
                Console.WriteLine($"ID: {temp.Id}, Başlık: {temp.Baslik}");
                temp = temp.Sonraki;
            }
        }
        IslemGecmisiEkle("Kitap Listelendi");
    }

    static void KitapAlimi(string uyeIsim, string kitapBaslik)
    {
        // Üyeye kitap verme fonksiyonu
        Uye uye = UyeBul(uyeIsim);
        Kitap kitap = KitapBul(kitapBaslik);
        if (uye != null && kitap != null)
        {
            Kitap yeniKitap = new Kitap { Id = kitap.Id, Baslik = kitap.Baslik, Sonraki = uye.AldigiKitaplarBasi };
            uye.AldigiKitaplarBasi = yeniKitap;
            Console.WriteLine("Kitap alımı başarılı.");
        }
        else
        {
            if (uye == null)
                Console.WriteLine("Üye bulunamadı.");
            if (kitap == null)
                Console.WriteLine("Kitap bulunamadı.");
        }
        IslemGecmisiEkle("Kitap Alimi Yapildi");
    }

    static void UyeKontrolu(string uyeIsim)
    {
        // Üyenin aldığı kitapları listeleme fonksiyonu
        Uye uye = UyeBul(uyeIsim);
        if (uye != null)
        {
            Console.WriteLine($"{uye.Isim} adlı üyenin aldığı kitaplar:");
            Kitap temp = uye.AldigiKitaplarBasi;
            while (temp != null)
            {
                Console.WriteLine($"- {temp.Baslik}");
                temp = temp.Sonraki;
            }
        }
        else
        {
            Console.WriteLine("Üye bulunamadı.");
        }
        IslemGecmisiEkle("Uye Kontrolu Yapildi");
    }

    // Yardımcı fonksiyonlar
    static Uye UyeBul(string isim)
    {
        // İsme göre üye bulma fonksiyonu
        Uye temp = uyeBasi;
        while (temp != null && temp.Isim != isim)
        {
            temp = temp.Sonraki;
        }
        return temp;
        IslemGecmisiEkle("Uye Bul Calistirldi ");
    }

    static Kitap KitapBul(string baslik)
    {
        // Başlığa göre kitap bulma fonksiyonu
        Kitap temp = kitapBasi;
        while (temp != null && temp.Baslik != baslik)
        {
            temp = temp.Sonraki;
        }
        return temp;
        IslemGecmisiEkle("Kitap Bul Calistirldi ");
    }
    static void EtkinlikKuyruguMenu()
    {
        int secim;
        do
        {
            Console.WriteLine("╔══════════════════════════════════════════════════╗");
            Console.WriteLine("║               Etkinlik Kuyrugu Menusu            ║");
            Console.WriteLine("╚══════════════════════════════════════════════════╝");
            Console.WriteLine("");
            Console.WriteLine("1. Etkinlik Katılımcılarını Görüntüle");
            Console.WriteLine("2. Etkinliğe Katılım");
            Console.WriteLine("3. Kuyruğu Sıfırla");
            Console.WriteLine("0. Ana Menüye Dön");
            Console.Write("Seciminizi giriniz: ");

            secim = Convert.ToInt32(Console.ReadLine());
            switch (secim)
            {
                case 1:
                    EtkinlikKatilimcilariniGoruntule();
                    break;
                case 2:
                    Console.Write("Etkinliğe katılmak isteyen üyenin ismini giriniz: ");
                    string isim = Console.ReadLine();
                    EtkinligeKatilimEkle(isim);
                    break;
                case 3:
                    KuyruguSifirla();
                    Console.WriteLine("Etkinlik kuyruğu sıfırlandı.");
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }
        } while (secim != 0);
        IslemGecmisiEkle("Etkinlik İslemleri Yapildi ");
    }

    static void EtkinlikKatilimcilariniGoruntule()
    {
        EtkinlikKatilimci temp = etkinlikKuyruguBasi;
        while (temp != null)
        {
            Console.WriteLine($"ID: {temp.Id}, Isim: {temp.Isim}");
            temp = temp.Sonraki;
        }
    }
    static void KuyruguSifirla()
    {
        etkinlikKuyruguBasi = null; // Kuyruğun başını null yaparak kuyruğu sıfırlar
    }

    static void EtkinligeKatilimEkle(string isim)
    {
        Uye uye = UyeBul(isim);
        if (uye != null)
        {
            EtkinlikKatilimci yeniKatilimci = new EtkinlikKatilimci { Id = uye.Id, Isim = uye.Isim };
            if (etkinlikKuyruguBasi == null)
            {
                etkinlikKuyruguBasi = yeniKatilimci;
            }
            else
            {
                EtkinlikKatilimci temp = etkinlikKuyruguBasi;
                while (temp.Sonraki != null)
                {
                    temp = temp.Sonraki;
                }
                temp.Sonraki = yeniKatilimci;
            }
            Console.WriteLine($"{isim} etkinliğe katılım listesine eklendi.");
        }
        else
        {
            Console.WriteLine("Üye bulunamadı veya etkinliğe zaten kayıtlı.");
        }
    }
    static bool PersonelVarMi(int id)
    {
        Personel temp = personelBasi;
        while (temp != null)
        {
            if (temp.Id == id)
            {
                return true;
            }
            temp = temp.Sonraki;
        }
        return false;
    }

    static bool UyeVarMi(int id)
    {
        Uye temp = uyeBasi;
        while (temp != null)
        {
            if (temp.Id == id)
            {
                return true;
            }
            temp = temp.Sonraki;
        }
        return false;
    }

    static bool KitapVarMi(int id)
    {
        Kitap temp = kitapBasi;
        while (temp != null)
        {
            if (temp.Id == id)
            {
                return true;
            }
            temp = temp.Sonraki;
        }
        return false;
    }

}