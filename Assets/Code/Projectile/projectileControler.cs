using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace projectile {
public class projectileControler : MonoBehaviour
{
Rigidbody2D _rigidbody2D;
    void Start() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = transform.right*10f;
    }
    void OnCollisionEnter2D (Collision2D other) {
        Destroy(gameObject);
    }
}
}