using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerCode;

public class CoinPickup : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
           playerController.numberOfCoins++;
           Destroy(gameObject);
        }
    }
}
