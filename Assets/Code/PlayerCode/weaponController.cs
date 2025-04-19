using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    void Update()
    {
        HandleGunAiming();

        if (Input.GetMouseButton(0) && Time.time >= lastShotTime + fireRate)
        {
            Shoot();
            lastShotTime = Time.time;
        }
    }

    public void Shoot()
    {
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

}
