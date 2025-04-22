using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerCode;

public class CoinPickup : MonoBehaviour
{
    public AudioClip coinSound;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.numberOfCoins++; 
            PlayerPrefs.SetInt("Coins", playerController.numberOfCoins);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(coinSound,transform.position);
            Debug.Log("coin picked up!");
        }
    }
}
