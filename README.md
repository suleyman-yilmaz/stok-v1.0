# İndirme

1. Projeyi bilgisayarınıza klonlayınız:

    ```bash
    git clone https://github.com/suleyman-yilmaz/stok-v1.0.git
    ```
# Veri Tabanı Kurma
!!! Bilgisayarınızda bulunan Microsoft Sql Server Management Studio versiyonu v19.3 veya daha yüksek bir sürüm olmalı. Aynı zamanda SQLEXPRESS versiyonunuz ise "15.0.2000.5" veya daha yüksek bir sürüm olmalı.
1. İndirdiğiniz proje dosyasını masaüstüne klasör olarak çıkartın.
2. Microsoft Sql Server Management Studio yu çalıştırınız. Bağlantınızı gerçekleşitriniz.
3. Object Explorer kısmında bulunan "Databases" e sağ tıklayarak açılan menüde "Import Data-tier Application" a tıklayın.
4. Açılan menüde alt kısımda buluna "Next" butonuna tıklayın. Ve açılan ekranda sağ kısımda bulunan "Browse..." butonuna tıklayın.
5. Açılan ekranda sizden bir adet bacpac dosyası seçmenizi isteyecek masaüstüne çıkarttığınız "stok-v1.0-main" klasörünü bulun ve içerisinde bulunan "stok.bacpac" dosyasını seçin ardından "Next" butonuna tıklayın.
6. Açılan ekranda "Finish" butonuna tıklayarak veri tabanını bilgisayarımıza kurma işlemlerini bekleyin. İşlemler bitince "Close" butonuna tıklayarak işlemleri bitiriniz.

Artık veri tabanı bilgisayarımıza kurulmuş durumda şimdi programı çalıştırma adımlarına geçebiliriz.

# Programı Çalıştırma
1.	Masaüstündeki "stok-v1.0-main" klasörünün içerisinde bulunan "stok v1.0.sln" dosyasını çalıştırın.
2.	Proje dosyasını açtıktan sonra Visual Studio da programı bir kere "Start" komutu ile çalıştırıp kapatınız. Bu sayede "stok v1.0" klasörü içerisinde bin/Debug klasörleri oluşacaktır.
3.	Masaüstüne çıkarttığımız "stok-v1.0-main" klasörü içerisinde bulunan "satis.xlsx" excel dosyasını kesip veya kopyalayıp "stok-v1.0-main\stok v1.0\bin\Debug" dizinine atınız.
4.	Program artık kullanıma hazır.

# Program Kullanımı
1.	Program  Anlık Stok, Ürün Girişi, Ürün Çıkışı, Satış Ekranı ve Ürün Bilgi Girişi bölümlerinden oluşmaktadır.
2.	Öncelikle Ürün Bilgi Girişi bölümünden random bir şekilde bilgiler girmeniz gerekiyor. Mesela Barkod No : 1, Ürün Adı : Telefon, Birimi : AD olacak şekilde bir kayıt girebiliriz.
3.	Ürün Girişi bölümünden ürün girişi yaparken daha önceden Ürün Bilgi Girişi bölümünden  ürün tablosuna veri girilmiş olmalıdır yani ürün tablosunda olmayan bir kaydı Ürün Girişi bölümünde kullanamayız. Örnek olarak Barkod No : 1, Giren Miktar : 10	, Alış Fiyatı : 5750,75, Toplam Tutar otomatik hesaplanacaktır. Firma boş kalabilir tarih ise programı çalıştırdığınız günün tarihini almaktadır tarihi istediğiniz gibi değiştirebilirsiniz.
4.	Ürün Giriş bölümünden bir ürünün kaydını silmek için o ürünün bulunduğu satıra çift tıklayarak sol alt kısımda bulunan label de o ürüne ait ıd bilgisi gelecektir geldikten sonra isterseniz ürünün kaydını silebilir isterseniz de bilgilerini değitirdikten sonra düzenle butonuna basarak ürün bilgilerini güncelleyebilirsiniz.
5.	Ürün çıkışı yapmak içinde Satış Ekranında 4. maddedeki gibi bir ürün eklemelisiniz daha sonra "SATIŞ YAP" butonuna tıklayarak ürün çıkışı yapabilir ve "Ürün Çıkışı" bölümünde görüntüleyebilirsiniz.
6.	Satış Ekranında (sepetinizde) eklemiş olduğunuz ürünleri daha önceden hazırlanmış excel tablosuna aktarmak için "FİYAT VER" butonuna tıklamanız yeterli olacaktır. Sepetinize eklemiş olduğunuz tüm ürünlerin gerekli bilgilerini fiyat bilgileri de dahil olmak üzere excle tablosuna aktaracaktır.
7.	Yanlışlıkla yapılan ürün çıkışını da silmek için Ürün Çıkışı bölümüne gelerek silmek istediğiniz ürünün bulunduğu satıra çift tıklayarak "SİL" butonuna tıklamanız yeterli olacaktır.
8.	Anlık Stok bölümünde ise kaydı bulunan ürünlerinizden kaç adet giriş yapıldığı ve kaç adet çıkış yapıldığını görebilirsiniz. Aynı zamanda da mevcut miktarı da görüntüleyebilirsiniz.
