using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBoxController : MonoBehaviour
{
    
    void Start()
    {
        Destroy(gameObject,Random.Range(3,7)); // �retilen canlar�n belli bir s�re i�inde yok olmas�n� sa�lar
    }
}
