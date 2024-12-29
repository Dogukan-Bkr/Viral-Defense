using System.Collections;
using System.Collections.Generic;
using TMPro; // TextMeshPro UI elemanlar�n� kullanmak i�in gerekli
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player; // Oyuncu karakteri referans�
    public GameObject enemy; // D��man prefab referans�
    public GameObject hpBox; // Sa�l�k kutusu prefab referans�
    public GameObject victoryPanel; // Zafer ekran�n� g�stermek i�in kullan�lan panel
    public int score; // Oyuncunun toplad��� puan� saklayan de�i�ken
    public TextMeshProUGUI pointText; // Oyuncunun puan�n� UI'de g�stermek i�in
    public TextMeshProUGUI timeText; // S�reyi g�stermek i�in eklendi (geri say�m� UI'ye yans�tacak)
    public TextMeshProUGUI totalPointText; // Toplam puan� zafer ekran�nda g�stermek i�in
    public TextMeshProUGUI totalPointVictoryText; // Oyunun bitiminde toplam puan� zafer ekran�nda g�stermek i�in
    float timer = 2; // Sa�l�k kutular�n�n belirli aral�klarla spawn edilmesi i�in saya�
    float fixTimer; // Oyunun kalan s�resini geri say�m olarak tutar

    void Start()
    {
        Time.timeScale = 1; // Oyunun normal h�zda �al��mas�n� sa�lar
        fixTimer = Random.Range(30, 60); // Oyunun toplam s�resi, rastgele bir aral�kta ba�lat�l�r
        for (int i = 0; i < Random.Range(0, 100); i++) // Oyunun ba��nda rastgele say�da d��man olu�turulur
        {
            RandomEnemySpawner();
        }
    }

    void Update()
    {
        if (timer < Time.time) // Belirli bir s�re ge�tikten sonra sa�l�k kutusu olu�tur
        {
            RandomHpBoxSpawner();
            timer = Time.time + 2; // Saya� s�f�rlanarak bir sonraki sa�l�k kutusu zamanlan�r
        }
        FixVirus(); // S�re kontrol� ve geri say�m� yapar
    }

    public void RandomEnemySpawner()
    {
        Instantiate(enemy, Random.insideUnitCircle * 20, Quaternion.identity); // Rastgele bir pozisyonda d��man spawn eder
    }

    public void RandomHpBoxSpawner()
    {
        Instantiate(hpBox, Random.insideUnitCircle * 6, Quaternion.identity); // Rastgele bir pozisyonda sa�l�k kutusu spawn eder
    }

    void FixVirus()
    {
        fixTimer -= Time.deltaTime; // Oyunun s�resini azalt�r
        UpdateTimeText(); // Geriye kalan s�reyi UI'de g�nceller

        if (fixTimer < 0) // S�re doldu�unda oyunu durdur ve zafer ekran�n� g�ster
        {
            Time.timeScale = 0; // Oyunu durdurur
            victoryPanel.SetActive(true); // Zafer panelini aktif eder
            totalPointVictoryText.text = score.ToString(); // Puan� zafer ekran�nda g�sterir
        }
    }

    public void replayButton()
    {
        SceneManager.LoadScene(0); // Sahneyi yeniden y�kleyerek oyunu ba�tan ba�lat�r
    }
    public void ExitButton()
    {
        Application.Quit();
    }
    void UpdateTimeText()
    {
        int minutes = Mathf.FloorToInt(fixTimer / 60); // Kalan dakikay� hesaplar
        int seconds = Mathf.FloorToInt(fixTimer % 60); // Kalan saniyeyi hesaplar
        timeText.text = string.Format("{0:00}:{1:00}", Mathf.Max(0, minutes), Mathf.Max(0, seconds));
        // S�reyi "MM:SS" format�nda g�sterir, negatif de�erleri s�f�r olarak g�sterir
    }
}
