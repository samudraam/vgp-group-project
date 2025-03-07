using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerCode;

public class CoinPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            // if (playerHealth != null && Time.time >= damageCooldown)
            // {
            //     playerHealth.health -= damage;
            //     Debug.Log("Player takes damage! Health: " + playerHealth.health);
            //     damageCooldown = Time.time + damageInterval; // Set cooldown
            // }
        }
    }
}
