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
    void MoveToPlayer() // Kullanýcýyý takip etmesi için gereken fonksiyon
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
