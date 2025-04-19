using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("Sway Settings")]
    public float swayDistance = 1f;
    public float swaySpeed = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * swaySpeed) * swayDistance;
        transform.position = startPos + new Vector3(offset, 0f, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit by: " + other.name);

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
