using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Projectile
{
    public class ProjectileControler : MonoBehaviour
    {
        Rigidbody2D _rigidbody2D;
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            float direction = transform.localScale.x >= 0 ? 1 : -1;
            SetDirection(direction);
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
        public void SetDirection(float direction)
        {
            if (_rigidbody2D == null) return;
            _rigidbody2D.velocity = new Vector2(direction * 10f, 0);
        }
    }
}