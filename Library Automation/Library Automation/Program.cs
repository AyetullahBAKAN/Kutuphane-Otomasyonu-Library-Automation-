using System;                          //Bursa Uludag Universitesi Veri Yapıları Dersi Kutuphane Otomasyonu Projesi(12.Grup)
using System.Collections.Generic;     //Mahsa Omidvar Gharehbaba(032290146),Ayetullah BAKAN(032090039),Dina Veladika(032290152)
using System.Linq;
//using System.Diagnostics;//1
//using static System.Runtime.MemoryFailPoint;//2
//using System.Security.Cryptography.X509Certificates;//3       1,2 ve 3 kutuphaneleri Algortitma analizi yapan fonksiyon için kullanıldı. 

//graph yapısı
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

    public object Timestamp { get; internal set; }
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
        newNode.Timestamp = DateTime.Now; //  timestamp ekledik
        top = newNode;
    }

    public string Pop(out DateTime timestamp)
    {
        if (IsEmpty())
        {
            Console.WriteLine("Stack boş, Pop işlemi gerçekleştirilemedi.");
            timestamp = DateTime.MinValue;
            return null;
        }
        string poppedData = top.Data;
        timestamp = (DateTime)top.Timestamp; //  timestamp geri döndürüyoz.
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
            Console.WriteLine($"[{current.Timestamp}] {current.Data}");
            current = current.Next;
            displayCount++;
        }
    }
}

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

    // Üyenin aldığı belirli bir kitabı iade etmesini sağlayan metot
    public int AldigiKitapSayisi()
    {
        int sayac = 0;
        Kitap temp = AldigiKitaplarBasi;
        while (temp != null)
        {
            sayac++;
            temp = temp.Sonraki;
        }
        return sayac;
    }
    public void KitapIadeEt(int kitapId)
    {
        Kitap temp = AldigiKitaplarBasi;
        Kitap onceki = null;

        while (temp != null && temp.Id != kitapId)
        {
            onceki = temp;
            temp = temp.Sonraki;
        }

        if (temp == null)
        {
            Console.WriteLine("Bu kitap üye tarafından alınmamış.");
            return;
        }

        if (onceki == null)
        {
            AldigiKitaplarBasi = temp.Sonraki;
        }
        else
        {
            onceki.Sonraki = temp.Sonraki;
        }
    }
}


// Kitap yapısı
public class Kitap
{
    public int Id;
    public int Adet;
    public string Baslik;
    public KitapTuru Turu; // Kitap türü (enum) eklendi
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

// BST için sınıf
public class BSTNode
{
    public Kitap Kitap;
    public BSTNode Sol;
    public BSTNode Sag;
    public KitapTuru Turu;
    public int Id;
    public string Baslik;

    public BSTNode(Kitap kitap)
    {
        Kitap = kitap;
        Sol = null;
        Sag = null;
    }
}
public class Hashtable
{
    private const int SIZE = 100; // Hashtable boyutu
    private Node[] buckets; // Node dizisi 

    public Hashtable()
    {
        buckets = new Node[SIZE];
    }

    // Hashing fonksiyonu
    private int HashFunction(int key)
    {
        return key % SIZE;
    }

    // Veri ekleme metodu
    public void Add(int key, string value)
    {
        int index = HashFunction(key); // Hashing ile indeks hesapla
        if (buckets[index] == null)
        {
            buckets[index] = new Node(key, value); // Yeni düğüm oluştur
        }
        else
        {
            Node current = buckets[index];
            while (current.Next != null)
            {
                current = current.Next; // Zinciri takip et
            }
            current.Next = new Node(key, value); // Yeni düğümü zincire ekle
        }
    }

    // Veri çıkarma metodu
    public void Remove(int key)
    {
        int index = HashFunction(key);
        if (buckets[index] == null)
        {
            return; // Anahtar bulunamadı
        }
        else if (buckets[index].Key == key)
        {
            buckets[index] = buckets[index].Next; // İlk düğümü kaldır
        }
        else
        {
            Node current = buckets[index];
            while (current.Next != null && current.Next.Key != key)
            {
                current = current.Next; // Zinciri takip et
            }
            if (current.Next != null)
            {
                current.Next = current.Next.Next; // Düğümü kaldır
            }
        }
    }

    // Veri getirme metodu
    public string Get(int key)
    {
        int index = HashFunction(key);
        Node current = buckets[index];
        while (current != null)
        {
            if (current.Key == key)
            {
                return current.Value; // Anahtarın değerini döndür
            }
            current = current.Next;
        }
        return null; // Anahtar bulunamadı isee
    }

    // Hashtable içeriğini listeleme metodu
    public void Display()
    {
        for (int i = 0; i < SIZE; i++)
        {
            if (buckets[i] != null)
            {
                Node current = buckets[i];
                while (current != null)
                {
                    Console.WriteLine($"Personel id: {current.Key}, Mesai Saatleri: {current.Value}");
                    current = current.Next;
                }
            }
        }
    }

    // Hashtable içinde kullanılacak düğüm yapısı
    private class Node
    {
        public int Key;
        public string Value;
        public Node Next;

        public Node(int key, string value)
        {
            Key = key;
            Value = value;
            Next = null;
        }
    }

}
class Program
{
       
    static Hashtable mesaiHashtable = new Hashtable();
    public static MyStack islemGecmisi = new MyStack();
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
    "Geleceği tahmin etmenin en iyi yolu onu yaratmaktır. - Peter Drucker",
    "Kitaplar, zamanın ruhunu konserveler gibi saklarlar. - Stephen King",
    "Bir kitap, insan ruhunu başka bir ruhla buluşturan sihirli bir anahtardır. - Helen Keller",
    "Bir kitap, bir insanın hayatını değiştirebilecek en sessiz ve en sürekli arkadaştır. - Charles W. Eliot",
    "Bir kitabın derinliklerinde kaybolmak, gerçek dünyadan kaçışın en güzel yoludur. - J.K. Rowling",
    "Kitaplar, düşüncelerin kanatlarıdır. - Henry Ward Beecher",
    "Bir kitap okumak, başka birinin hayal dünyasında gezintiye çıkmaktır. - Carl Sagan",
    "Bir kitap, insanın en iyi dostudur; en kötü zamanında bile seni yargılamaz ve sana sırtını dönmez. - Marcus Tullius Cicero",
    "Bir kitap, binlerce yaşamı bir araya getiren bir denizdir. - Jorge Luis Borges",
    "Bir kitap, karanlıkta bir ışık, yalnızlıkta bir dost, umutsuzlukta bir umuttur. - Victor Hugo",
    "Kitaplar, insanın bilgiye açılan pencereleridir. - Malala Yousafzai"
};
    static Dictionary<string, string> kullaniciBilgileri = new Dictionary<string, string>()
{
    {"kullanici1", "sifre1"},
    {"kullanici2", "sifre2"},
    {"kullanici3", "sifre3"},
    {"kullanici4", "sifre4"}
};

    static void Main(string[] args)
    {
        GirisMenu();
    }
    static void GirisMenu()
    {
        int secimUsers;
        do
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                 Bursa Uludag Universitesi Kütüphane Otomasyonu                  ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine("████████████████████████████████████████████████████████████████████████████████████");
            Console.WriteLine("█░░░░░░░░░░░░░░░░░░░░░░█████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
            Console.WriteLine("█░░███████░░░░░░░░░░███▒▒▒▒▒▒▒▒███░░░░░░░░░░░░░░░░░░░░░░░███████████████████████████");
            Console.WriteLine("█░░█▒▒▒▒▒▒█░░░░░░░███▒▒▒▒▒▒▒▒▒▒▒▒▒███░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
            Console.WriteLine("█░░░█▒▒▒▒▒▒█░░░░██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██░░░░░░░░░░░░░░░░░░░░░░░░█████████████████████");
            Console.WriteLine("█░░░░█▒▒▒▒▒█░░░██▒▒▒▒▒██▒▒▒▒▒▒██▒▒▒▒▒███░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
            Console.WriteLine("█░░░░░█▒▒▒█░░░█▒▒▒▒▒▒████▒▒▒▒████▒▒▒▒▒▒██░░░░░░░░░░░░░░░░░░░░░░░░░░█████████████████");
            Console.WriteLine("█░░░█████████████▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
            Console.WriteLine("█░░░█▒▒▒▒▒▒▒▒▒▒▒▒█▒▒▒▒▒▒▒▒▒█▒▒▒▒▒▒▒▒▒▒▒██░░░░░░░░░░░░░░░░░░░░░░░░░░█████████████████");
            Console.WriteLine("█░██▒▒▒▒▒▒▒▒▒▒▒▒▒█▒▒▒██▒▒▒▒▒▒▒▒▒▒██▒▒▒▒██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
            Console.WriteLine("███▒▒▒███████████▒▒▒▒▒██▒▒▒▒▒▒▒▒██▒▒▒▒▒██░░░░░░░░░░░░░░░░░░░░░░░░░░█████████████████");
            Console.WriteLine("██▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█▒▒▒▒▒▒████████▒▒▒▒▒▒▒██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
            Console.WriteLine("███▒▒▒▒▒▒▒▒▒▒▒▒▒▒█▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██░░░░░░░░░░░░░░░░░░░░░░░█████████████████████");
            Console.WriteLine("█░█▒▒▒███████████▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒██░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
            Console.WriteLine("█░██▒▒▒▒▒▒▒▒▒▒████▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒█░░░░░░░░░░░░░░░░░░░░░███████████████████████████");
            Console.WriteLine("█░░████████████░░░█████████████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
            Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                 Bursa Uludag Universitesi Kütüphane Otomasyonu                  ║");
            Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine("████████████████████████████████████████████████████████████████████████████████████");
            Console.WriteLine("█░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
            Console.WriteLine("████████████████████████████████████████░░░░░███████████████████████████████████████");
            Console.WriteLine("█░░░░░░░░░░░░░░░░░░    GIRIS YAP(1)     ░░░░░       KAPAT(0)      ░░░░░░░░░░░░░░░░░█");
            Console.WriteLine("████████████████████████████████████████░░░░░███████████████████████████████████████");
            Console.WriteLine("█░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
            Console.WriteLine("████████████████████████████████████████████████████████████████████████████████████");

            Console.Write("Seciminiz : ");
            secimUsers = isInt();

            switch (secimUsers)
            {
                case 1:
                    GirisYap();
                    break;
                case 0:
                    Console.WriteLine("Programdan çıkılıyor...");
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }
        } while (secimUsers != 0);


    }
    static void GirisYap()
    {
        Console.Write("Kullanıcı adı: ");
        string kullaniciAdi = Console.ReadLine();

        Console.Write("Şifre: ");
        string sifre = Console.ReadLine();

        // Kullanıcı adı ve şifreyi kontrol et
        if (kullaniciBilgileri.ContainsKey(kullaniciAdi))
        {
            if (kullaniciBilgileri[kullaniciAdi] == sifre)
            {
                Console.Clear();
                AnaProgram();
            }
            else
            {
                Console.WriteLine("Hatalı şifre.");
            }
        }
        else
        {
            Console.WriteLine("Geçersiz kullanıcı adı.");
        }
    }
    static void AnaProgram()
    {
        int secim;
        string isim;
        int id;
        do
        {
            MenuGoster();
            secim = isInt();


            switch (secim)
            {
                case 1:
                    Console.Write("Personel ID giriniz: ");
                    id = isInt();
                    Console.Write("Personel ismi giriniz: ");
                    isim = Console.ReadLine();
                    PersonelEkle(id, isim);
                    
                    break;
                case 2:
                    Console.Write("Silinecek personel ID giriniz: ");
                    id = isInt();
                    PersonelCikar(id);
                    break;
                case 3:
                    MesaiSaatleriMenu();
                    break;
                case 4:
                    Console.Write("Üye ID giriniz: ");
                    id = isInt();
                    Console.Write("Üye ismi giriniz: ");
                    isim = Console.ReadLine();
                    UyeEkle(id, isim);
                    break;
                case 5:
                    Console.Write("Silinecek üye ID giriniz: ");
                    id = isInt();
                    UyeCikar(id);
                    break;
                case 6:
                    Console.Write("Kitap ID giriniz: ");
                    id = isInt();
                    Console.Write("Kitap başlığı giriniz: ");
                    isim = Console.ReadLine();
                    Console.WriteLine("Kitap türünü giriniz (Roman, Hikaye, Masal, Korku, Bilimsel, Dram): ");
                    KitapTuru turu = (KitapTuru)Enum.Parse(typeof(KitapTuru), Console.ReadLine(), true);
                    Console.Write("Kaç adet eklemek istiyorsunuz? ");
                    int adet = isInt();
                    KitapEkle(id, isim, turu, adet);
                    break;

                case 7:
                    Console.Write("Çıkarılacak kitap başlığını giriniz: ");
                    string cikarilacakKitapBasligi = Console.ReadLine();
                    Console.Write("Çıkarılacak adeti giriniz: ");
                    int cikarilacakAdet = isInt();
                    KitapCikar(cikarilacakKitapBasligi, cikarilacakAdet);
                    break;
                case 8:
                    PersonelleriListele();
                    break;
                case 9:
                    UyeleriListele();
                    break;
                case 10:
                    KitaplariListele();
                    break;
                case 11:
                    Console.Write("Üye ismini giriniz: ");
                    isim = Console.ReadLine();
                    Console.Write("Kitap başlığını giriniz: ");
                    string baslik = Console.ReadLine();
                    Console.Write("istenen adeti giriniz: ");
                    int eklenecekadet = isInt();
                    KitapAlimi(isim, baslik, eklenecekadet);
                    break;
                case 12:
                    Console.Write("Kontrol edilecek üye ismini giriniz: ");
                    isim = Console.ReadLine();
                    UyeKontrolu(isim);
                    break;
                case 13:
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
                case 14:
                    Console.Write("Bulunacak kitap başlığını giriniz: ");
                    baslik = Console.ReadLine();
                    Kitap kitap = KitapBul(baslik);
                    if (kitap != null)
                    {
                        Console.WriteLine($"Kitap bulundu: ID: {kitap.Id}, Başlık: {kitap.Baslik}, Adet: {kitap.Adet}");
                    }
                    else
                    {
                        Console.WriteLine("Kitap bulunamadı.");
                    }
                    break;
                case 15:
                    Console.Write("Kitap tavsiyesi için üye ismini giriniz: ");
                    string uyeIsim = Console.ReadLine();
                    KitapTavsiye(uyeIsim);
                    break;
                case 16:
                    EtkinlikKuyruguMenu();
                    break;
                case 17:
                    Console.WriteLine("Aranacak kitap türünü giriniz (Roman, Hikaye, Masal, Korku, Bilimsel, Dram): ");
                    KitapTuru arananTuru = (KitapTuru)Enum.Parse(typeof(KitapTuru), Console.ReadLine(), true);
                    TureGoreKitaplariListele(arananTuru);
                    break;
                case 18:
                    Console.Write("son kaç işlem : ");
                    int sayi = isInt();

                    IslemGecmisiGoster(sayi);
                    break;
                case 19:
                    Console.Write("İade edilecek kitabın başlığını giriniz: ");
                    string iadeEdilecekKitapBaslik = Console.ReadLine();
                    KitapIade(iadeEdilecekKitapBaslik);
                    break;
                case 20:
                    Hazirlayanlar();
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
        Console.WriteLine("█░░░█░░██████████████████████████████░░█░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░█░░█████   ISLEM MENUSU   ███████░░█░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░█░░██████████████████████████████░░█░░░░░░░░░░░░░░░░░░████░░░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░█░░█ 1.  Personel Ekle          █░░█░░░░░░░░░░░░░░░░░███░██░░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░█░░█ 2.  Personel Çıkar         █░░█░░░░░░░░░░░░░░░░░██░░░█░░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░█░░█ 3   Personel MSY Sistemi   █░░█░░░░░░░░░░░░░░░░░██░░░██░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░█░░█ 4.  Üye Ekle               █░░█░░░░░░░░░░░░░░░░░░██░░░███░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░█░░█ 5.  Üye Çıkar              █░░█░░░░░░░░░░░░░░░░░░░██░░░░██░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░█░░█ 6.  Kitap Ekle             █░░█░░░░░░░░░░░░░░░░░░░██░░░░░███░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░█░░█ 7.  Kitap Çıkar            █░░█░░░░░░░░░░░░░░░░░░░░██░░░░░░██░░░░░░░░░░░░█");
        Console.WriteLine("█░░░█░░█ 8.  Personelleri Listele   █░░█░░░░░░░░░░░░░░░███████░░░░░░░██░░░░░░░░░░░█");
        Console.WriteLine("█░░░█░░█ 9.  Üyeleri Listele        █░░█░░░░░░░░░░░░█████░░░░░░░░░░░░░░███░██░░░░░█");
        Console.WriteLine("█░░░█░░█ 10. Kitapları Listele      █░░█░░░░░░░░░░░██░░░░░████░░░░░░░░░░██████░░░░█");
        Console.WriteLine("█░░░█░░█ 11. Kitap Alımı            █░░█░░░░░░░░░░░██░░████░░███░░░░░░░░░░░░░██░░░█");
        Console.WriteLine("█░░░█░░█ 12. Üye Kontrolü           █░░█░░░░░░░░░░░██░░░░░░░░███░░░░░░░░░░░░░██░░░█");
        Console.WriteLine("█░░░█░░█ 13. Üye Bul                █░░█░░░░░░░░░░░░██████████░███░░░░░░░░░░░██░░░█");
        Console.WriteLine("█░░░█░░█ 14. Kitap Bul              █░░█░░░░░░░░░░░░██░░░░░░░░████░░░░░░░░░░░██░░░█");
        Console.WriteLine("█░░░█░░█ 15. Kitap Tavsiye          █░░█░░░░░░░░░░░░███████████░░██░░░░░░░░░░██░░░█");
        Console.WriteLine("█░░░█░░█ 16. Etkinlik Kuyruğu       █░░█░░░░░░░░░░░░░░██░░░░░░░████░░░░░██████░░░░█");
        Console.WriteLine("█░░░█░░█ 17. Ture Gore Arama        █░░█░░░░░░░░░░░░░░██████████░██░░░░███░██░░░░░█");
        Console.WriteLine("█░░░█░░█ 18. Islem Gecmisi          █░░█░░░░░░░░░░░░░░░░░██░░░░░████░███░░░░░░░░░░█");
        Console.WriteLine("█░░░█░░█ 19. Kitap iade islemi      █░░█░░░░░░░░░░░░░░░░░█████████████░░░░░░░░░░░░█");
        Console.WriteLine("█░░░█░░█ 20. Hazırlayanlar          █░░█░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░█░░█ 0.  Çıkış                  █░░█░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("█░░░█░░██████████████████████████████░░█░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█");
        Console.WriteLine("███████████████████████████████████████████████████████████████████████████████████");
        Console.WriteLine("╔═════════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                 Bursa Uludag Universitesi Kütüphane Otomasyonu                  ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════════════════════════════╝");
    }
    static int isInt()// Bu fonksiyon ile input kontrolü yapıyoruz int-string hataları için.
    {
        int input;
        int sec;
        while (true)
        {
            //Console.Write("Lütfen bir sayı girin: ");
            string userInput = Console.ReadLine();

            // Kullanıcının girdisi integer mı diye kontrol edelim
            if (int.TryParse(userInput, out input))
            {
                // Eğer integer ise, giriş doğrudur
                sec = Convert.ToInt32(userInput);
                break; // Sonsuz döngüden çık
            }
            else
            {
                // Eğer integer değilse, yanlış giriş bildirimi yapalım
                Console.WriteLine("HATALI BIR DEGER GIRDINIZ LUTFEN BIR TAM SAYI DEGERI GIRINIZ");
            }
        }
        return sec;
    }
    // PERSONEL ISLEMLERI------------
    static void PersonelEkle(int id, string isim)
    {
        // Aynı ID'ye sahip bir personel var mı kontrol et
        if (PersonelVarMi(id))
        {
            Console.WriteLine("Hata: Bu ID'ye sahip bir personel zaten var.");
            return;
        }
        // Personel ekleyen fonksiyon
        Personel yeniPersonel = new Personel { Id = id, Isim = isim, Sonraki = personelBasi };//başa ekle mantığı
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
    //HASHTABLE YAPILI PERSONEL MSY SISTEMI
    static void MesaiSaatleriMenu()
    {
        int secim;
        do
        {
            Console.WriteLine("██████████████████████████████████████████████");
            Console.WriteLine("█░░█         Mesai Saatleri Yönetimi      █░░█");
            Console.WriteLine("█░░████████████████████████████████████████░░█");
            Console.WriteLine("█░░█1. Personelin Mesai Saatlerini Ekle   █░░█");
            Console.WriteLine("█░░█2. Personelin Mesai Saatlerini Çıkar  █░░█");
            Console.WriteLine("█░░█3. Personelin Mesai Saatlerini Listele█░░█");
            Console.WriteLine("█░░█0. Ana Menüye Dön                     █░░█");
            Console.WriteLine("██████████████████████████████████████████████");
            Console.Write("Seçiminiz: ");
            secim = isInt();
            switch (secim)
            {
                case 1:
                    MesaiSaatleriEkle();
                    break;
                case 2:
                    MesaiSaatleriCikar();
                    break;
                case 3:
                    MesaiSaatleriListele();
                    break;
                case 0:
                    Console.WriteLine("Ana Menüye Dönülüyor...");
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }
        } while (secim != 0);
    }
    static void MesaiSaatleriEkle()
    {
        if (personelBasi == null)
        {
            Console.WriteLine("Önce bir personel eklemelisiniz.");
            return;
        }
        Console.Write("Personel ID giriniz: ");
        int id = isInt();

        // ID kontrolü
        if (!PersonelVarMi(id))
        {
            Console.WriteLine("Girilen ID'ye sahip bir personel bulunamadı.");
            return;
        }

        // Daha önceden mesai saatleri kaydedilmiş mi kontrolü
        if (mesaiHashtable.Get(id) != null)
        {
            Console.WriteLine("Bu ID'ye sahip personelin mesai saatleri zaten kaydedilmiş.");
            return;
        }

        Console.Write("Mesai saatlerini giriniz: ");
        string saatler = Console.ReadLine();
        mesaiHashtable.Add(id, saatler);
        IslemGecmisiEkle($"Personel ID: {id}'ye mesai saatleri eklendi.");
    }

    static void MesaiSaatleriCikar()
    {
        if (personelBasi == null)
        {
            Console.WriteLine("Önce bir personel eklemelisiniz.");
            return;
        }

        Console.Write("Personel ID giriniz: ");
        int id = isInt();

        // ID kontrolü
        if (!PersonelVarMi(id))
        {
            Console.WriteLine("Girilen ID'ye sahip bir personel bulunamadı.");
            return;
        }

        // Kayıtlı mesai saatlerinin kontrolü
        if (mesaiHashtable.Get(id) == null)
        {
            Console.WriteLine("Bu ID'ye sahip personelin kayıtlı mesai saatleri bulunmamaktadır.");
            return;
        }

        mesaiHashtable.Remove(id);
        IslemGecmisiEkle($"Personel ID: {id}'nin mesai saatleri çıkarıldı.");
    }

    static void MesaiSaatleriListele()
    {
        Console.WriteLine("Tüm personel mesai saatleri:");
        mesaiHashtable.Display();
        IslemGecmisiEkle("Mesai saatleri listelendi.");
    }
    static bool PersonelVarMi(int id) // aynı id degerine sahip personel girisinin engellenmesi icin
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
    // UYE ISLEMLERI
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

        Uye temp = uyeBasi;

        if (uyeBasi == null)
        {
            Console.WriteLine("Listelenecek Uye bulunamadı.");
        }
        else
        {
            while (temp != null)
            {
                Console.WriteLine($"ID: {temp.Id}, Isim: {temp.Isim}");
                temp = temp.Sonraki;
            }
        }
        IslemGecmisiEkle("Uye Listelendi");
    }
    static void UyeKontrolu(string uyeIsim)
    {
        // Üyenin aldığı kitapları listeleme fonksiyonu
        Uye uye = UyeBul(uyeIsim);
        if (uye != null)
        {
            Console.WriteLine($"{uye.Isim} adlı üyenin aldığı kitaplar:");
            Kitap temp = uye.AldigiKitaplarBasi;
            string oncekiKitapBaslik = null; // Bir önceki kitabın başlığını saklamak için kullanılacak değişken
            while (temp != null)
            {
                // Eğer şu anki kitap bir önceki kitapla aynı değilse, yazdır
                if (temp.Baslik != oncekiKitapBaslik)
                {
                    int aldigiAdet = AldigiKitapAdetiniBul(uye.AldigiKitaplarBasi, temp.Id); // Kitabın adetini bul
                    Console.WriteLine($"- {temp.Baslik} ({aldigiAdet} adet)");
                    oncekiKitapBaslik = temp.Baslik; // Bir sonraki kitap için bir önceki kitabı güncelle
                }
                temp = temp.Sonraki;
            }
        }
        else
        {
            Console.WriteLine("Üye bulunamadı.");
        }
        IslemGecmisiEkle("Uye Kontrolu Yapildi");
    }
    static int AldigiKitapAdetiniBul(Kitap aldigiKitaplarBasi, int kitapId)
    {
        int adet = 0;
        Kitap temp = aldigiKitaplarBasi;
        while (temp != null)
        {
            if (temp.Id == kitapId)
            {
                adet++;
            }
            temp = temp.Sonraki;
        }
        return adet;
    }
    static Uye UyeBul(string isim)
    {
        // İsme göre üye bulma fonksiyonu
        IslemGecmisiEkle("Uye Bul Calistirldi ");
        Uye temp = uyeBasi;
        while (temp != null && temp.Isim != isim)
        {
            temp = temp.Sonraki;
        }
        return temp;
        
    }
    static bool UyeVarMi(int id) // aynı id degerine sahip UYE girisinin engellenmesi icin
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
    // KITAP ISLEMLERI
    static void Sil(Kitap silinecekKitap)
    {
        // Eğer silinecek kitap listenin başındaysa
        if (kitapBasi == silinecekKitap)
        {
            kitapBasi = silinecekKitap.Sonraki;
        }
        else
        {
            // Kitabı bul
            Kitap temp = kitapBasi;
            while (temp != null && temp.Sonraki != silinecekKitap)
            {
                temp = temp.Sonraki;
            }

            // Kitabı listeden çıkar
            if (temp != null)
            {
                temp.Sonraki = silinecekKitap.Sonraki;
            }
        }
    }
    static void KitapCikar(string kitapBaslik, int adet)
    {
        if (kitapBasi == null)
        {
            Console.WriteLine("Çıkarılacak kitap bulunamadı.");
            return;
        }

        // Kitabı bul
        Kitap temp = kitapBasi;
        while (temp != null && temp.Baslik != kitapBaslik)
        {
            temp = temp.Sonraki;
        }

        if (temp == null)
        {
            Console.WriteLine("Kitap bulunamadı.");
            return;
        }

        // Adet kontrolü ve işlem
        if (temp.Adet == adet)
        {
            // Eğer kitap sayısı, çıkarılacak adet kadar ise, kitabı listeden sil
            Sil(temp);
            Console.WriteLine($"{adet} adet {kitapBaslik} kitabı başarıyla çıkarıldı.");
        }
        else if (temp.Adet > adet)
        {
            // Eğer kitap sayısı çıkarılacak adetten fazla ise, sadece adet sayısını düşür
            temp.Adet -= adet;
            Console.WriteLine($"{adet} adet {kitapBaslik} kitabı başarıyla çıkarıldı.");
        }
        else
        {
            // Eğer kitap sayısı çıkarılacak adetten az ise, hata mesajı ver
            Console.WriteLine($"Hata: {kitapBaslik} kitabının stokta yeterli adeti bulunmamaktadır.");
        }

        IslemGecmisiEkle($"Kitap çıkarıldı: Kitap: {kitapBaslik}, Adet: {adet}");
    }
    // Kitap ekleme fonksiyonu
    static void KitapEkle(int id, string baslik, KitapTuru turu, int adet)
    {
        // Aynı ID'ye sahip bir kitap var mı kontrol et
        if (KitapVarMi(id))
        {
            Console.WriteLine("Hata: Bu ID'ye sahip bir kitap zaten var.");
            return;
        }
        if (KitapVarMi(baslik))
        {
            Console.WriteLine("Hata: Bu Baslıga sahip bir kitap zaten var.");
            return;
        }

        Kitap yeniKitap = new Kitap { Id = id, Baslik = baslik, Turu = turu, Adet = adet, Sonraki = kitapBasi };
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
    // Kitap iade işlemi
    static void KitapIade(string kitapBaslik)
    {
        Console.Write("Üye ismini giriniz: ");
        string uyeIsim = Console.ReadLine();

        Uye uye = UyeBul(uyeIsim);
        if (uye != null)
        {
            Kitap kitap = KitapBul(kitapBaslik);
            if (kitap != null)
            {
                // Üyeden iade miktarını al
                Console.Write($"Kaç tane \"{kitapBaslik}\" kitabı iade edeceksiniz?: ");
                int iadeMiktari = Convert.ToInt32(Console.ReadLine());

                // Üyeden iade edilecek kitabın sayısını kontrol et
                if (iadeMiktari <= uye.AldigiKitapSayisi() && uye.AldigiKitapSayisi() > 0)
                {
                    // Üyeden kitapları iade et
                    for (int i = 0; i < iadeMiktari; i++)
                    {
                        uye.KitapIadeEt(kitap.Id);
                    }

                    // Kütüphaneye kitapları ekle
                    kitap.Adet += iadeMiktari;

                    Console.WriteLine($"\"{kitapBaslik}\" kitabından {iadeMiktari} adet başarıyla iade edildi.");
                }
                if (uye.AldigiKitapSayisi() <= 0)
                {
                    Console.WriteLine("Bu kitap üye tarafından alınmamış");
                }
                else
                {
                    Console.WriteLine($"Üye \"{kitapBaslik}\" kitabından {uye.AldigiKitapSayisi()} adetten fazla iade edemez.");
                }
            }
            else
            {
                Console.WriteLine("Kitap bulunamadı.");
            }
        }
        else
        {
            Console.WriteLine("Üye bulunamadı.");
        }

        IslemGecmisiEkle($"Kitap iade edildi: Kitap: {kitapBaslik}");
    }

    // Kitapları listeleme işlemi
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
                Console.WriteLine($"ID: {temp.Id}, Başlık: {temp.Baslik}, Adet: {temp.Adet}");
                temp = temp.Sonraki;
            }
        }
        IslemGecmisiEkle("Kitaplar listelendi");
    }
    static bool KitapVarMi(string baslik) // kitap var mı fonks'nun string deger alan hali
    {
        Kitap temp = kitapBasi;
        while (temp != null)
        {
            if (temp.Baslik == baslik)
            {
                return true;
            }
            temp = temp.Sonraki;
        }
        return false;
    }
    static bool KitapVarMi(int id) // aynı id degerine sahip KITAP girisinin engellenmesi icin
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
    static void TureGoreKitaplariListele(KitapTuru tur)
    {
        if (kitapBasi == null)
        {
            Console.WriteLine("Listelenecek kitap bulunamadı.");
        }
        else
        {
            BSTNode temp = kitapBST;
            while (temp != null)
            {
                if (temp.Turu == tur)
                {
                    Console.WriteLine($"ID: {temp.Kitap.Id}, Başlık: {temp.Kitap.Baslik}, Türü: {temp.Turu}");
                }
                temp = temp.Sag;
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
    static void KitapTavsiye(string uyeIsim) // binary search tree kullandık
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
    static void KitapAlimi(string uyeIsim, string kitapBaslik, int adet)
    {
        Uye uye = UyeBul(uyeIsim);
        Kitap kitap = KitapBul(kitapBaslik);

        if (uye != null && kitap != null)
        {
            if (kitap.Adet >= adet)
            {
                for (int i = 0; i < adet; i++)
                {
                    Kitap yeniKitap = new Kitap { Id = kitap.Id, Baslik = kitap.Baslik, Sonraki = uye.AldigiKitaplarBasi };
                    uye.AldigiKitaplarBasi = yeniKitap;
                }

                kitap.Adet -= adet; // Kitap adetini azalt
                Console.WriteLine($"{adet} adet \"{kitapBaslik}\" kitabı \"{uyeIsim}\" üyesine başarıyla verildi.");
            }
            else
            {
                Console.WriteLine($"Üzgünüz, \"{kitapBaslik}\" kitabından yeterli miktarda bulunmamaktadır.");
            }
        }
        else
        {
            if (uye == null)
                Console.WriteLine("Üye bulunamadı.");
            if (kitap == null)
                Console.WriteLine("Kitap bulunamadı.");
        }
        IslemGecmisiEkle($"Kitap alındı: Üye: {uyeIsim}, Kitap: {kitapBaslik}, Adet: {adet}");
    }


    static Kitap KitapBul(string baslik)
    {
        // Başlığa göre kitap bulma fonksiyonu
        Kitap temp = kitapBasi;
        IslemGecmisiEkle("Kitap Bul Calistirldi ");
        while (temp != null && temp.Baslik != baslik)
        {
            temp = temp.Sonraki;
        }
        return temp;

    }
    //ETKINLIK ISLEMLERI-------
    static void EtkinlikKuyruguMenu()
    {
        int secim;
        do
        {
            Console.WriteLine("╔══════════════════════════════════════════════════╗");
            Console.WriteLine("║               Etkinlik Kuyrugu Menusu            ║");
            Console.WriteLine("╚══════════════════════════════════════════════════╝");
            Console.WriteLine("");
            Console.WriteLine("████████████████████████████████████████████████████");
            Console.WriteLine("█   1. Etkinlik Katılımcılarını Görüntüle          █");
            Console.WriteLine("█   2. Etkinliğe Katılım                           █");
            Console.WriteLine("█   3. Kuyruğu Sıfırla                             █");
            Console.WriteLine("█   0. Ana Menüye Dön                              █");
            Console.WriteLine("████████████████████████████████████████████████████");
            Console.Write("Seciminizi giriniz: ");

            secim = isInt();
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

        if (etkinlikKuyruguBasi == null)
        {
            Console.WriteLine("Listelenecek Katilimci Bulunamadi.");
        }
        else
        {
            EtkinlikKatilimci temp = etkinlikKuyruguBasi;
            while (temp != null)
            {
                Console.WriteLine($"ID: {temp.Id}, Isim: {temp.Isim}");
                temp = temp.Sonraki;
            }
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
    static void Hazirlayanlar()
    {
        Console.WriteLine("███████████████████████████████");
        Console.WriteLine("█   Projeyi Hazırlayanlar     █");
        Console.WriteLine("███████████████████████████████");
        Console.WriteLine("█ -> Dına Veladıka            █");
        Console.WriteLine("█ -> Ayetullah BAKAN          █");
        Console.WriteLine("█ -> Mahsa Omidvar Gharehbaba █");
        Console.WriteLine("███████████████████████████████");
        IslemGecmisiEkle("Hazirlayanlar Goruntulendi");

    }
    //ISLEM GECMISI ISLEMLERI
    static void IslemGecmisiEkle(string islem)
    {
        islemGecmisi.Push(islem);
    }
    static void IslemGecmisiGoster(int sayi)
    {
        islemGecmisi.Display(sayi);
    }
   /*public class PerformanceAnalyzer     --------->  //HOCAM BU FONKSİYON GEREKTİĞİ DURUMLARDA ALGORİTMA ANALİZİ YAPIP UYGUN YAPIYI SECMEMİZE YARDIMCI OLMASI İÇİN EKLEDİK SADECE TEST DURUMLARINDA KULLANDIK 
{                                                  //( PerformanceAnalyzer x=new PerformanceAnalyzer();x.Analyze(Hazirlayanlar);) --->KULLANIMA BİR ÖRNEKTİR
    public  void Analyze(Action targetFunction)
    {
        // Zaman ölçümü için Stopwatch başlatılıyor.
        Stopwatch stopwatch = Stopwatch.StartNew();

        // Hedef fonksiyon çalıştırılıyor.
        targetFunction.Invoke();

        // Stopwatch durduruluyor.
        stopwatch.Stop();

        // Çalışma süresi yazdırılıyor.
        Console.WriteLine($"Çalışma Süresi: {stopwatch.ElapsedMilliseconds} ms");

        // Bellek kullanımı ölçülüyor.
        long memoryUsageBefore = GC.GetTotalMemory(true);
        targetFunction.Invoke();
        long memoryUsageAfter = GC.GetTotalMemory(false);

        // Kullanılan bellek miktarı hesaplanıyor.
        long memoryUsed = memoryUsageAfter - memoryUsageBefore;

        // Bellek kullanımı yazdırılıyor.
        Console.WriteLine($"Yaklaşık Bellek Kullanımı: {memoryUsed} bytes");
    }
}
*/

}