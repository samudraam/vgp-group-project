using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Projectile;
using PlayerCode;
using UnityEngine.UI;

namespace Enemy2
{
    public class Enemy2Controller : MonoBehaviour
    {
        public Transform player;
        public float moveSpeed = 3f;
        public float jumpForce = 8f;
        public float attackRange = 2f;
        public LayerMask groundLayer;

        private Rigidbody2D rb;
        private Animator animator;
        private SpriteRenderer sr;
        float health;
        float maxHealth = 100f;
        public GameObject coinPrefab;
        public GameObject healthCoinPrefab;
        public float damage;
        private bool isGrounded;
        public Transform groundCheck;
        public float groundCheckRadius = 0.2f;
        private float damageCooldown = 0f;
        public float damageInterval = 0.5f;
        public Slider slider;


        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
            health = maxHealth;
            UpdateHealthBar(health, maxHealth);

        }

        void Update()
        {
            if (player == null) return;

            HandleGroundCheck();
            MoveTowardsPlayer();
            TryAttack();
        }

        void HandleGroundCheck()
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        void MoveTowardsPlayer()
        {
            float xDistance = player.position.x - transform.position.x;
            float yDistance = Mathf.Abs(player.position.y - transform.position.y);

            if (yDistance < 1f)
            {
                if (Mathf.Abs(xDistance) > 0.1f)
                {
                    float direction = Mathf.Sign(xDistance);
                    sr.flipX = direction < 0;
                    rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }

                animator.SetFloat("moving", Mathf.Abs(rb.velocity.x));
            }

            if (isGrounded && !IsPathClear())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                animator.SetTrigger("jump");
            }
        }


        void TryAttack()
        {
            float distance = Vector2.Distance(transform.position, player.position);

            if (distance <= attackRange)
            {
                animator.SetTrigger("slash");

            }
        }

        bool IsPathClear()
        {
            Vector2 direction = sr.flipX ? Vector2.left : Vector2.right;
            return !Physics2D.Raycast(transform.position, direction, 0.5f, groundLayer);
        }


        public void TakeDamage()
        {

            health -= 5f;

            UpdateHealthBar(health, maxHealth);

            StartCoroutine(FlashRed());

            if (health <= 0f)
            {
                Die();
            }
        }

        IEnumerator FlashRed()
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            sr.color = Color.white;
        }


        void Die()
        {

            if (coinPrefab != null)
            {
                Instantiate(coinPrefab, transform.position + new Vector3(1f, 0f, 0f), Quaternion.identity);           // 1 unit to the right
                Instantiate(healthCoinPrefab, transform.position + new Vector3(0.2f, 0.5f, 0f), Quaternion.identity); // slightly up & right
            }
            Destroy(gameObject);
        }

        void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player") && Time.time >= damageCooldown)
            {
                PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage);
                    damageCooldown = Time.time + damageInterval;
                }
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<ProjectileController>())
            {
                if (health > 0)
                {
                    TakeDamage();
                }
                else
                {
                    Die();
                }
                Destroy(other.gameObject);

            }
        }

        public void UpdateHealthBar(float currVal, float maxVal)
        {
            slider.value = currVal / maxVal;

        }
    }
}
