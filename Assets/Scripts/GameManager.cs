using System.Collections;
using System.Collections.Generic;
using TMPro; // TextMeshPro UI elemanlarýný kullanmak için gerekli
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player; // Oyuncu karakteri referansý
    public GameObject enemy; // Düþman prefab referansý
    public GameObject hpBox; // Saðlýk kutusu prefab referansý
    public GameObject victoryPanel; // Zafer ekranýný göstermek için kullanýlan panel
    public int score; // Oyuncunun topladýðý puaný saklayan deðiþken
    public TextMeshProUGUI pointText; // Oyuncunun puanýný UI'de göstermek için
    public TextMeshProUGUI timeText; // Süreyi göstermek için eklendi (geri sayýmý UI'ye yansýtacak)
    public TextMeshProUGUI totalPointText; // Toplam puaný zafer ekranýnda göstermek için
    public TextMeshProUGUI totalPointVictoryText; // Oyunun bitiminde toplam puaný zafer ekranýnda göstermek için
    float timer = 2; // Saðlýk kutularýnýn belirli aralýklarla spawn edilmesi için sayaç
    float fixTimer; // Oyunun kalan süresini geri sayým olarak tutar

    void Start()
    {
        Time.timeScale = 1; // Oyunun normal hýzda çalýþmasýný saðlar
        fixTimer = Random.Range(30, 60); // Oyunun toplam süresi, rastgele bir aralýkta baþlatýlýr
        for (int i = 0; i < Random.Range(0, 100); i++) // Oyunun baþýnda rastgele sayýda düþman oluþturulur
        {
            RandomEnemySpawner();
        }
    }

    void Update()
    {
        if (timer < Time.time) // Belirli bir süre geçtikten sonra saðlýk kutusu oluþtur
        {
            RandomHpBoxSpawner();
            timer = Time.time + 2; // Sayaç sýfýrlanarak bir sonraki saðlýk kutusu zamanlanýr
        }
        FixVirus(); // Süre kontrolü ve geri sayýmý yapar
    }

    public void RandomEnemySpawner()
    {
        Instantiate(enemy, Random.insideUnitCircle * 20, Quaternion.identity); // Rastgele bir pozisyonda düþman spawn eder
    }

    public void RandomHpBoxSpawner()
    {
        Instantiate(hpBox, Random.insideUnitCircle * 6, Quaternion.identity); // Rastgele bir pozisyonda saðlýk kutusu spawn eder
    }

    void FixVirus()
    {
        fixTimer -= Time.deltaTime; // Oyunun süresini azaltýr
        UpdateTimeText(); // Geriye kalan süreyi UI'de günceller

        if (fixTimer < 0) // Süre dolduðunda oyunu durdur ve zafer ekranýný göster
        {
            Time.timeScale = 0; // Oyunu durdurur
            victoryPanel.SetActive(true); // Zafer panelini aktif eder
            totalPointVictoryText.text = score.ToString(); // Puaný zafer ekranýnda gösterir
        }
    }

    public void replayButton()
    {
        SceneManager.LoadScene(0); // Sahneyi yeniden yükleyerek oyunu baþtan baþlatýr
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(fixTimer / 60); // Kalan dakikayý hesaplar
        int seconds = Mathf.FloorToInt(fixTimer % 60); // Kalan saniyeyi hesaplar
        timeText.text = string.Format("{0:00}:{1:00}", Mathf.Max(0, minutes), Mathf.Max(0, seconds));
        // Süreyi "MM:SS" formatýnda gösterir, negatif deðerleri sýfýr olarak gösterir
    }
}
