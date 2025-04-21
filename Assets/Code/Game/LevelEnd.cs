using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [Tooltip("Name of the scene to load next")]
    public string nextSceneName = "Level2";

    [Tooltip("Index of the level being unlocked (e.g. 2 for Level2)")]
    public int nextLevelIndex = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level complete!");

            UnlockNextLevel(nextLevelIndex);
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void UnlockNextLevel(int index)
    {
        int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (index > unlocked)
        {
            PlayerPrefs.SetInt("UnlockedLevel", index);
            PlayerPrefs.Save();
        }
    }
}


