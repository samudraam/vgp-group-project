using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Projectile;
using PlayerCode;
using UnityEngine.UI;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        // Start is called before the first frame update
        public Transform player;
        public float speed = 3f;
        public float chaseRange = 5f;
        public float stopRange = 1f;
        private Vector2 lastPosition;
        private bool isShooting = false;
        public float shootCooldown = 1f;
        public GameObject coinPrefab;


        private SpriteRenderer spriteRenderer;
        private Animator animator;
        private Rigidbody2D rb;


        private ProjectileController projectile;
        public PlayerHealth phealth;
        public float damage;


        private float damageCooldown = 0f;
        public float damageInterval = 1f;
        public GameObject slimeProjectilePrefab;
        public Transform firePoint;

        private Vector3 firePointOriginalLocalPos;

        float health;
        float maxHealth = 50f;
        public Slider slider;




        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            lastPosition = transform.position;
            rb = GetComponent<Rigidbody2D>();
            projectile = new ProjectileController();
            firePointOriginalLocalPos = firePoint.localPosition;
            health = maxHealth;
            UpdateHealthBar(health, maxHealth);
            player = GameObject.FindWithTag("Player").transform;


        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (player == null) return;

            float distance = Vector2.Distance(transform.position, player.position);
            float xDifference = player.position.x - transform.position.x;
            float directionX = Mathf.Sign(xDifference); 

            if (distance < chaseRange && distance > stopRange)
            {
                rb.velocity = new Vector2(directionX * speed, rb.velocity.y);

                if (Mathf.Abs(xDifference) > 0.1f) 
                {
                    spriteRenderer.flipX = directionX < 0;
                }

                firePoint.localPosition = new Vector3(
                    spriteRenderer.flipX ? -firePointOriginalLocalPos.x : firePointOriginalLocalPos.x,
                    firePointOriginalLocalPos.y,
                    firePointOriginalLocalPos.z
                );

                animator.SetFloat("moving", Mathf.Abs(rb.velocity.x));
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                animator.SetFloat("moving", 0);
            }

            if (distance <= chaseRange && !isShooting)
            {
                StartCoroutine(ShootRoutine());
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


        void ShootAtPlayer()
        {
            if (slimeProjectilePrefab == null || firePoint == null || player == null) return;

            Vector2 direction = (player.position - firePoint.position).normalized;

            GameObject slime = Instantiate(slimeProjectilePrefab, firePoint.position, Quaternion.identity);

            Rigidbody2D slimeRb = slime.GetComponent<Rigidbody2D>();
            if (slimeRb != null)
            {
                slimeRb.velocity = direction * 10f;
            }

        }



        IEnumerator ShootRoutine()
        {
            isShooting = true;

            ShootAtPlayer();

            yield return new WaitForSeconds(shootCooldown);

            isShooting = false;
        }
        public void TakeDamage()
        {

            health -= 10f;

            UpdateHealthBar(health, maxHealth);

            StartCoroutine(FlashRed());

            if (health <= 0f)
            {
                Die();
            }
        }

        void Die()
        {
            if (coinPrefab != null)
            {
                Instantiate(coinPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        IEnumerator FlashRed()
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            spriteRenderer.color = Color.white;
        }

        public void UpdateHealthBar(float currVal, float maxVal)
        {
            slider.value = currVal / maxVal;

        }
    }
}



