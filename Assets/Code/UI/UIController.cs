using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class UIController : MonoBehaviour
{
    //[SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject levelMenuUI;
    //[SerializeField] private GameObject overMenuUI;
    //[SerializeField] private GameObject player;
    //[SerializeField] private GameObject health;


    public void StartGame()
    {
        //startMenuUI.SetActive(false);
        //levelMenuUI.gameObject.SetActive(false);
        //overMenuUI.gameObject.SetActive(false);
        //player.SetActive(true);
        // health.gameObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackToMainMenu()
    {
        //startMenuUI.SetActive(true);
        //levelMenuUI.gameObject.SetActive(false);
        // overMenuUI.gameObject.SetActive(false);
        SceneManager.LoadScene(0); 
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void WinGame()
    {
        Debug.Log("You Win!");
        levelMenuUI.SetActive(true); // Temporary
    }
}
