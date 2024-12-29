using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlayerController : MonoBehaviour
{
    public GameObject bullet; // Mermi prefab'ý
    public GameObject gameManager; // GameManager objesine eriþim saðlar (puan, oyun durumu yönetimi)
    public Image hpBar; // Oyuncunun saðlýk durumunu görsel olarak gösterecek UI barý
    public GameObject gameOverPanel; // Oyun bittiðinde gösterilecek panel (Game Over)
    Rigidbody2D rb; // Oyuncunun hareketini kontrol etmek için Rigidbody2D
    float x, y, hp; // x, y hareket koordinatlarý, hp oyuncunun saðlýðý

    void Start()
    {
        hp = 100f; // Oyuncunun baþlangýç saðlýðý
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D bileþenini alýr
        hpBar.fillAmount = hp / 100f; // Saðlýk çubuðunu baþlangýçta doldurur
    }

    void Update()
    {
        PlayerMovement(); // Oyuncunun hareketini kontrol eder
        fire(); // Ateþ etme fonksiyonunu kontrol eder
    }

    void PlayerMovement()
    {
        // Oyuncunun yatay ve dikey hareketlerini alýr
        x = Input.GetAxis("Horizontal"); // Yatay hareket (A/D veya ok tuþlarý)
        y = Input.GetAxis("Vertical"); // Dikey hareket (W/S veya ok tuþlarý)

        rb.AddForce(new Vector2(x, y) * 5); // Oyuncuya yön ve hýzda kuvvet uygular
    }

    void fire()
    {
        if (Input.GetMouseButtonDown(0)) // Sol fare tuþuna basýldýðýnda
        {
            // Mermi instantiate ile sahnede oluþturulur 
            Instantiate(bullet, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Çarpýþma gerçekleþtiðinde çalýþýr
        if (collision.tag == "WeakEnemy") // Eðer çarpan nesne "WeakEnemy" ise
        {
            hp -= 5; // Oyuncunun saðlýðýndan 5 puan kaybeder
            hpBar.fillAmount = hp / 100f; // Saðlýk barýný günceller
            if (hp <= 0) // Eðer saðlýk sýfýr veya altýna düþerse
            {
                playerDead(); // Oyuncu ölür
            }
        }
        else if (collision.tag == "hpBox" && hp <= 100) // Eðer çarpan nesne "hpBox" ise ve oyuncunun saðlýðý 100'den azsa
        {
            hp += 10; // Saðlýk kutusu ile saðlýðý artýrýr
            if (hp > 100) { hp = 100; } // Saðlýk 100'ü geçerse 100 olarak ayarlanýr
            hpBar.fillAmount = hp / 100f; // Saðlýk barýný günceller
            Destroy(collision.gameObject); // Saðlýk kutusunu yok eder
        }
    }

    void playerDead()
    {
        Time.timeScale = 0; // Oyunu durdurur (zamaný durdurur, yani tüm hareketler durur)
        gameOverPanel.SetActive(true); // Oyun bitti panelini aktif eder
        gameManager.GetComponent<GameManager>().totalPointText.text = gameManager.GetComponent<GameManager>().score.ToString();
        // GameManager'dan toplam puaný alýp Game Over panelinde gösterir
    }
}
