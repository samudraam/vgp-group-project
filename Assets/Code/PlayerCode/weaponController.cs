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
        public int rifleAmmoBaseCost = 5;
        public int shotgunAmmoBaseCost = 10;
        private int rifleAmmoCurrentCost;
        private int shotgunAmmoCurrentCost;

        public int ammoPerPurchase = 5;
        public int priceIncreasePerPurchase = 2;

        public int pistolAmmoBaseCost = 2;
        private int pistolAmmoCurrentCost;

        [Header("UI Price Text")]

        public TextMeshProUGUI pistolPriceText;
        public TextMeshProUGUI riflePriceText;
        public TextMeshProUGUI shotgunPriceText;




        void Start()
        {
            currentBullets = maxBullets;
            playerControllerRef = FindObjectOfType<playerController>();

            rifleAmmoCurrentCost = rifleAmmoBaseCost;
            shotgunAmmoCurrentCost = shotgunAmmoBaseCost;
            pistolAmmoCurrentCost = pistolAmmoBaseCost;

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

            bool isFacingLeft = transform.root.localScale.x < 0;

            if (isFacingLeft)
            {
                angle += 180f;
            }

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

            if (playerController.numberOfCoins >= rifleAmmoCurrentCost)
            {
                if (currentBullets >= maxBullets)
                {
                    Debug.Log("Ammo is already at max capacity!");
                    return false;
                }

                playerController.numberOfCoins -= rifleAmmoCurrentCost;
                rifleAmmoCurrentCost += priceIncreasePerPurchase;
                UpdateShopPriceUI();

                PlayerPrefs.SetInt("Coins", playerController.numberOfCoins);
                PlayerPrefs.Save();

                currentBullets = Mathf.Min(currentBullets + ammoPerPurchase, maxBullets);
                UpdateAmmoUI();

                Debug.Log("Purchased rifle ammo for " + (rifleAmmoCurrentCost - priceIncreasePerPurchase) + "! New cost: " + rifleAmmoCurrentCost);
                return true;
            }
            else
            {
                Debug.Log("Not enough coins! Need " + rifleAmmoCurrentCost);
                return false;
            }
        }


        public bool BuyShotgunAmmo()
        {
            if (playerControllerRef == null)
            {
                Debug.LogError("Player Controller reference is missing!");
                return false;
            }

            if (playerController.numberOfCoins >= shotgunAmmoCurrentCost)
            {
                if (currentBullets >= maxBullets)
                {
                    Debug.Log("Ammo is already at max capacity!");
                    return false;
                }

                playerController.numberOfCoins -= shotgunAmmoCurrentCost;
                shotgunAmmoCurrentCost += priceIncreasePerPurchase;
                UpdateShopPriceUI();

                PlayerPrefs.SetInt("Coins", playerController.numberOfCoins);
                PlayerPrefs.Save();

                currentBullets = Mathf.Min(currentBullets + ammoPerPurchase, maxBullets);
                UpdateAmmoUI();

                Debug.Log("Purchased shotgun ammo for " + (shotgunAmmoCurrentCost - priceIncreasePerPurchase) + "! New cost: " + shotgunAmmoCurrentCost);
                return true;
            }
            else
            {
                Debug.Log("Not enough coins! Need " + shotgunAmmoCurrentCost);
                return false;
            }
        }

        public bool BuyPistolAmmo()
        {
            if (playerControllerRef == null)
            {
                Debug.LogError("Player Controller reference is missing!");
                return false;
            }

            if (playerController.numberOfCoins >= pistolAmmoCurrentCost)
            {
                if (currentBullets >= maxBullets)
                {
                    Debug.Log("Ammo is already at max capacity!");
                    return false;
                }

                playerController.numberOfCoins -= pistolAmmoCurrentCost;
                pistolAmmoCurrentCost += priceIncreasePerPurchase;
                UpdateShopPriceUI();

                PlayerPrefs.SetInt("Coins", playerController.numberOfCoins);
                PlayerPrefs.Save();

                currentBullets = Mathf.Min(currentBullets + ammoPerPurchase, maxBullets);
                UpdateAmmoUI();

                Debug.Log("Purchased pistol ammo for " + (pistolAmmoCurrentCost - priceIncreasePerPurchase) + "! New cost: " + pistolAmmoCurrentCost);
                return true;
            }
            else
            {
                Debug.Log("Not enough coins! Need " + pistolAmmoCurrentCost);
                return false;
            }
        }
        public void UpdateShopPriceUI()
        {
            if (pistolPriceText != null)
                pistolPriceText.text = "-" + pistolAmmoCurrentCost.ToString();

            if (riflePriceText != null)
                riflePriceText.text = "-" + rifleAmmoCurrentCost.ToString();

            if (shotgunPriceText != null)
                shotgunPriceText.text = "-" + shotgunAmmoCurrentCost.ToString();
        }


    }

}
