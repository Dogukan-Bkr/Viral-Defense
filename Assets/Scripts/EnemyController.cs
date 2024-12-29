using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    GameObject player;
    float moveSpeed = 3.5f;
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
    }

    
    void Update()
    {
        MoveToPlayer();
    }
    void MoveToPlayer() // Kullan�c�y� takip etmesi i�in gereken fonksiyon
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.transform.position,
                moveSpeed * Time.deltaTime
            );
        }
    }
}
