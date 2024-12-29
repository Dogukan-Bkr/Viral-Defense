using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerController : MonoBehaviour
{
    public GameObject bullet; // Mermi prefab'�
    public GameObject gameManager; // GameManager objesine eri�im sa�lar (puan, oyun durumu y�netimi)
    public Image hpBar; // Oyuncunun sa�l�k durumunu g�rsel olarak g�sterecek UI bar�
    public GameObject gameOverPanel; // Oyun bitti�inde g�sterilecek panel (Game Over)
    Rigidbody2D rb; // Oyuncunun hareketini kontrol etmek i�in Rigidbody2D
    float x, y, hp; // x, y hareket koordinatlar�, hp oyuncunun sa�l���

    void Start()
    {
        hp = 100f; // Oyuncunun ba�lang�� sa�l���
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D bile�enini al�r
        hpBar.fillAmount = hp / 100f; // Sa�l�k �ubu�unu ba�lang��ta doldurur
    }

    void Update()
    {
        PlayerMovement(); // Oyuncunun hareketini kontrol eder
        fire(); // Ate� etme fonksiyonunu kontrol eder
    }

    void PlayerMovement()
    {
        // Oyuncunun yatay ve dikey hareketlerini al�r
        x = Input.GetAxis("Horizontal"); // Yatay hareket (A/D veya ok tu�lar�)
        y = Input.GetAxis("Vertical"); // Dikey hareket (W/S veya ok tu�lar�)

        rb.AddForce(new Vector2(x, y) * 5); // Oyuncuya y�n ve h�zda kuvvet uygular
    }

    void fire()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare tu�una bas�ld���nda
        {
            // Mermi instantiate ile sahnede olu�turulur 
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �arp��ma ger�ekle�ti�inde �al���r
        if (collision.tag == "WeakEnemy") // E�er �arpan nesne "WeakEnemy" ise
        {
            hp -= 5; // Oyuncunun sa�l���ndan 5 puan kaybeder
            hpBar.fillAmount = hp / 100f; // Sa�l�k bar�n� g�nceller
            if (hp <= 0) // E�er sa�l�k s�f�r veya alt�na d��erse
            {
                playerDead(); // Oyuncu �l�r
            }
        }
        else if (collision.tag == "hpBox" && hp <= 100) // E�er �arpan nesne "hpBox" ise ve oyuncunun sa�l��� 100'den azsa
        {
            hp += 10; // Sa�l�k kutusu ile sa�l��� art�r�r
            if (hp > 100) { hp = 100; } // Sa�l�k 100'� ge�erse 100 olarak ayarlan�r
            hpBar.fillAmount = hp / 100f; // Sa�l�k bar�n� g�nceller
            Destroy(collision.gameObject); // Sa�l�k kutusunu yok eder
        }
    }

    void playerDead()
    {
        Time.timeScale = 0; // Oyunu durdurur (zaman� durdurur, yani t�m hareketler durur)
        gameOverPanel.SetActive(true); // Oyun bitti panelini aktif eder
        gameManager.GetComponent<GameManager>().totalPointText.text = gameManager.GetComponent<GameManager>().score.ToString();
        // GameManager'dan toplam puan� al�p Game Over panelinde g�sterir
    }
}
