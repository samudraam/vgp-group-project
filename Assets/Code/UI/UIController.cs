using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject levelMenuUI;
    [SerializeField] private GameObject overMenuUI;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject health;

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        startMenuUI.SetActive(false);
        levelMenuUI.gameObject.SetActive(false);
        overMenuUI.gameObject.SetActive(false);
        player.SetActive(true);
        health.gameObject.SetActive(true);
    }

    public void BackToMenu()
    {
        startMenuUI.SetActive(true);
        levelMenuUI.gameObject.SetActive(false);
        overMenuUI.gameObject.SetActive(false);
    }
}
