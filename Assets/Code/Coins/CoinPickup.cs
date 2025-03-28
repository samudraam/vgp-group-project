using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerCode;

public class CoinPickup : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {   

        Debug.Log("Hit: " + collision.name + " | Tag: " + collision.tag);
        
        if (collision.transform.tag == "Player")
        {
           playerController.numberOfCoins++;
           Destroy(gameObject);
           Debug.Log("coin picked up!");
        }
    }
}
