using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour


{
    [Tooltip("Name of the scene to load next")]
    public string nextSceneName = "Level1";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level complete!");
            SceneManager.LoadScene(nextSceneName);
        }
    }
}

