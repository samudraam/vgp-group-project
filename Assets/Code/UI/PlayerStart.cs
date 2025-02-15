using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStart : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject player;
    public void StartLevel()
    {
        if (startMenu != null)
        {
            startMenu.SetActive(false);
        }
        if (player != null)
        {
            player.SetActive(true);
        }

    }


}
