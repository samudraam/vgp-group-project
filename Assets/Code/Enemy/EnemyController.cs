using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Projectile;
using PlayerCode;
public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public float speed = 3f;
    public float chaseRange = 5f;
    public float stopRange = 1f;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector2 lastPosition;
    private ProjectileController projectile;
    public PlayerHealth phealth;
    public float damage;

    private float damageCooldown = 0f;
    public float damageInterval = 1f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
        projectile = new ProjectileController();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        float movementSpeed = ((Vector2)transform.position - lastPosition).magnitude / Time.deltaTime;

        if (distance < chaseRange && distance > stopRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            if (player.position.x > transform.position.x)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;
        }
        animator.SetFloat("isMoving", movementSpeed);

        lastPosition = transform.position;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<ProjectileController>())
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null && Time.time >= damageCooldown)
            {
                playerHealth.health -= damage;
                Debug.Log("Player takes damage! Health: " + playerHealth.health);
                damageCooldown = Time.time + damageInterval; // Set cooldown
            }
        }
    }
}
