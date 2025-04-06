using UnityEngine;
using UnityEngine.UI;

namespace PlayerCode
{
    public class PlayerHealth : MonoBehaviour
    {
        public float health;
        public float maxHealth = 600;
        public Image healthBar;
        public GameObject overMenu;

        private playerController controller;

        void Start()
        {
            health = maxHealth;
            controller = GetComponent<playerController>();
        }

        void Update()
        {
            UpdateHealthBar();

            if (health <= 0)
            {
                Die();
            }
        }

        public void TakeDamage(float amount)
        {
            health -= amount;
            Debug.Log("Player takes damage! Health: " + health);

            controller.TakeDamage();
        }

        void UpdateHealthBar()
        {
            float fillValue = health / maxHealth;
            if (!float.IsNaN(fillValue) && !float.IsInfinity(fillValue))
            {
                healthBar.fillAmount = Mathf.Clamp(fillValue, 0, 1);
            }
        }

        public void RestoreHealth()
        {
            health = maxHealth;
            UpdateHealthBar();
            Debug.Log("Health fully restored!");
        }


        void Die()
        {
            Debug.Log("Player has died!");
            Destroy(gameObject);
            if (overMenu != null)
                overMenu.SetActive(true);
        }
    }
}
