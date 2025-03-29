using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player fell out of bounds! Restarting game...");

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

