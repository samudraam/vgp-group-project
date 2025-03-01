using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Projectile
{
    public class ProjectileControler : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private float speed = 10f;

        void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void SetDirection(Vector2 direction)
        {
            if (_rigidbody2D == null) return;

            _rigidbody2D.velocity = direction * speed;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
