using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBoxController : MonoBehaviour
{
    
    void Start()
    {
        Destroy(gameObject,Random.Range(3,7)); // Üretilen canlarýn belli bir süre içinde yok olmasýný saðlar
    }
}
