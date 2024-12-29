using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rb; // Merminin fiziksel hareketlerini kontrol etmek için Rigidbody2D referansý
    Vector2 direction, mousePosition; // Hareket yönünü ve fare pozisyonunu tutmak için kullanýlan deðiþkenler
    GameObject gameManager; // GameManager nesnesine eriþim saðlamak için

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Merminin Rigidbody2D bileþenini alýr
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        // GameManager objesini GameController etiketiyle bulur, skor ve düþman yönetimi için eriþim saðlar

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Fare pozisyonunu dünya koordinatlarýna çevirir
        direction = (mousePosition - (Vector2)transform.position).normalized;
        // Merminin hareket edeceði yönü hesaplar

        rb.AddForce(direction.normalized * 35, ForceMode2D.Impulse);
        // Mermiyi belirlenen yöne doðru kuvvet uygular

        Destroy(gameObject, 4);
        // Eðer herhangi bir virüse çarpmaz ise mermiyi 4 saniye sonra sahneden temizler
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mermi bir nesneye çarptýðýnda çalýþýr
        if ((collision.tag != "Player") && (collision.tag != "hpBox") && !collision.name.Equals("Border")) // Kullanýcýyý ve saðlýk kutularýný yok etmemesi için eklenen koþullar
        {
            // Merminin "Player" veya "hpBox" ile çarpýþmasýný dikkate almaz
            if (collision.tag == "WeakEnemy") // "WeakEnemy" tagý ileride farklý tipde düþmanlar eklenebilir 
            {
                // Eðer çarpýlan nesne "WeakEnemy" ise:
                gameManager.GetComponent<GameManager>().RandomEnemySpawner();
                // Yeni bir düþman oluþturur
                gameManager.GetComponent<GameManager>().score += 10;
                // Skora 10 puan ekler
                gameManager.GetComponent<GameManager>().pointText.text = gameManager.GetComponent<GameManager>().score.ToString();
                // Güncel skoru UI'de gösterir
            }

            Destroy(collision.gameObject);
            // Çarpýlan nesneyi yok eder
            Destroy(gameObject);
            // Mermiyi yok eder
        }
    }
}
