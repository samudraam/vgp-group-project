using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayerCode;
namespace weaponCode
{
    public class weaponController : MonoBehaviour
    {
        [Header("Projectile Settings")]
        public GameObject projectilePrefab;
        public Transform firePoint;
        public float fireRate = 0.5f;
        public float projectileSpeed = 10f;
        public int damage = 1;
        public Transform gunTransform;

        private float lastShotTime;

        [Header("Projectile Shoot")]
        public int numberOfProjectiles = 1;
        public float spreadAngle = 5f;

        [Header("Audio")]
        public AudioSource audioSource;
        public AudioClip shootSound;
        public AudioClip emptyGunSound;


        [Header("Ammo")]
        public int currentBullets = 30;
        public int maxBullets = 30;

        [Header("UI")]
        public TextMeshProUGUI ammoText;

        [Header("Player Controller Reference")]
        private playerController playerControllerRef;

        [Header("Ammo Prices")]
        public int rifleAmmoCost = 5;
        public int shotgunAmmoCost = 10;
        public int ammoPerPurchase = 5;

        void Start()
        {
            currentBullets = maxBullets;
            playerControllerRef = FindObjectOfType<playerController>();
            if (playerControllerRef == null)
            {
                Debug.LogWarning("Player Controller not found! Coin-based purchases won't work.");
            }
            UpdateAmmoUI();
        }


        void Update()
        {
            HandleGunAiming();
            if (Input.GetMouseButton(0) && Time.time >= lastShotTime + fireRate)
            {
                Shoot();
            }
        }

        public void Shoot()
        {
            if (currentBullets <= 0)
            {
                audioSource.PlayOneShot(emptyGunSound);
                return;
            }

            lastShotTime = Time.time;

            currentBullets--;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector2 baseDirection = (mousePosition - firePoint.position).normalized;

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                float angleOffset = Random.Range(-spreadAngle, spreadAngle);
                Vector2 spreadDirection = Quaternion.Euler(0, 0, angleOffset) * baseDirection;

                GameObject newProjectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                Rigidbody2D rb = newProjectile.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = spreadDirection * projectileSpeed;
                }

                Projectile.ProjectileController projectileScript = newProjectile.GetComponent<Projectile.ProjectileController>();
                if (projectileScript != null)
                {
                    projectileScript.SetDirection(spreadDirection);
                }
            }

            if (shootSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(shootSound);
            }

            UpdateAmmoUI();

        }

        private void HandleGunAiming()
        {
            if (gunTransform == null) return;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Vector3 direction = mousePosition - gunTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, -45f, 45f);
            gunTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void UpdateAmmoUI()
        {
            if (ammoText != null)
                ammoText.text = "" + currentBullets;
        }
        public bool BuyRifleAmmo()
        {
            if (playerControllerRef == null)
            {
                Debug.LogError("Player Controller reference is missing!");
                return false;
            }

            if (playerController.numberOfCoins >= rifleAmmoCost)
            {
                if (currentBullets >= maxBullets)
                {
                    Debug.Log("Ammo is already at max capacity!");
                    return false;
                }

                playerController.numberOfCoins -= rifleAmmoCost;
                PlayerPrefs.SetInt("Coins", playerController.numberOfCoins);
                PlayerPrefs.Save();

                currentBullets = Mathf.Min(currentBullets + ammoPerPurchase, maxBullets);

                UpdateAmmoUI();
                Debug.Log("Purchased rifle ammo! Current bullets: " + currentBullets);
                return true;
            }
            else
            {
                Debug.Log("Not enough coins to buy rifle ammo!");
                return false;
            }
        }

        public bool BuyShotgunAmmo()
        {
\            if (playerControllerRef == null)
            {
                Debug.LogError("Player Controller reference is missing!");
                return false;
            }

            if (playerController.numberOfCoins >= shotgunAmmoCost)
            {
                if (currentBullets >= maxBullets)
                {
                    Debug.Log("Ammo is already at max capacity!");
                    return false;
                }

                playerController.numberOfCoins -= shotgunAmmoCost;
                PlayerPrefs.SetInt("Coins", playerController.numberOfCoins);
                PlayerPrefs.Save();

                currentBullets = Mathf.Min(currentBullets + ammoPerPurchase, maxBullets);


                UpdateAmmoUI();
                Debug.Log("Purchased shotgun ammo! Current bullets: " + currentBullets);
                return true;
            }
            else
            {
                Debug.Log("Not enough coins to buy shotgun ammo!");
                return false;
            }
        }
    }

}