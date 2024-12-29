using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rb; // Merminin fiziksel hareketlerini kontrol etmek i�in Rigidbody2D referans�
    Vector2 direction, mousePosition; // Hareket y�n�n� ve fare pozisyonunu tutmak i�in kullan�lan de�i�kenler
    GameObject gameManager; // GameManager nesnesine eri�im sa�lamak i�in

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Merminin Rigidbody2D bile�enini al�r
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        // GameManager objesini GameController etiketiyle bulur, skor ve d��man y�netimi i�in eri�im sa�lar

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Fare pozisyonunu d�nya koordinatlar�na �evirir
        direction = (mousePosition - (Vector2)transform.position).normalized;
        // Merminin hareket edece�i y�n� hesaplar

        rb.AddForce(direction.normalized * 35, ForceMode2D.Impulse);
        // Mermiyi belirlenen y�ne do�ru kuvvet uygular

        Destroy(gameObject, 4);
        // E�er herhangi bir vir�se �arpmaz ise mermiyi 4 saniye sonra sahneden temizler
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mermi bir nesneye �arpt���nda �al���r
        if ((collision.tag != "Player") && (collision.tag != "hpBox") && !collision.name.Equals("Border")) // Kullan�c�y� ve sa�l�k kutular�n� yok etmemesi i�in eklenen ko�ullar
        {
            // Merminin "Player" veya "hpBox" ile �arp��mas�n� dikkate almaz
            if (collision.tag == "WeakEnemy") // "WeakEnemy" tag� ileride farkl� tipde d��manlar eklenebilir 
            {
                // E�er �arp�lan nesne "WeakEnemy" ise:
                gameManager.GetComponent<GameManager>().RandomEnemySpawner();
                // Yeni bir d��man olu�turur
                gameManager.GetComponent<GameManager>().score += 10;
                // Skora 10 puan ekler
                gameManager.GetComponent<GameManager>().pointText.text = gameManager.GetComponent<GameManager>().score.ToString();
                // G�ncel skoru UI'de g�sterir
            }

            Destroy(collision.gameObject);
            // �arp�lan nesneyi yok eder
            Destroy(gameObject);
            // Mermiyi yok eder
        }
    }
}
