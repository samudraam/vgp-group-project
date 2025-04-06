using UnityEngine;
using PlayerCode;

public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit: " + collision.name + " | Tag: " + collision.tag);

        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.RestoreHealth();
                Destroy(gameObject);
                Debug.Log("health picked up!");
            }
        }
    }
}
