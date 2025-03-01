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

        void Start()
        {
            health = maxHealth;
        }

        void Update()
        {

            float fillValue = health / maxHealth;
            Debug.Log("Health: " + health + " / " + maxHealth);
            if (!float.IsNaN(fillValue) && !float.IsInfinity(fillValue))
            {
                healthBar.fillAmount = Mathf.Clamp(fillValue, 0, 1);
            }

            if (health <= 0)
            {
                Debug.Log("Player has died!");
                Destroy(gameObject);
                overMenu.SetActive(true);
            }
        }


    }
}
