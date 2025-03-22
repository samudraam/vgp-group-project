using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerCode;

public class SlimeProjectile : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;

    public float damage = 1f;
    public float damageInterval = 1f;
    private float damageCooldown;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            animator.Play("Slime_Jump");
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.health -= damage;
                Debug.Log("Player hit by slime! Player Health: " + playerHealth.health);
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
