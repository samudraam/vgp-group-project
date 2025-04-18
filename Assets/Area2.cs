using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Area2 : MonoBehaviour
{
    public GameObject barrierObject; 
    public TextMeshProUGUI tutorialText;
    [TextArea]
    public string message = "Aim with the mouse and click to shoot.\nDestroy all targets to continue.";

    public GameObject[] targetsToDestroy;

    private bool playerInside = false;
    private bool gateOpened = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            tutorialText.text = message;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            tutorialText.text = "";
        }
    }

    void Update()
    {
        if (!playerInside || gateOpened) return;

        bool allDestroyed = true;
        foreach (GameObject target in targetsToDestroy)
        {
            if (target != null)
            {
                allDestroyed = false;
                break;
            }
        }

        if (allDestroyed)
        {
            gateOpened = true;
            tutorialText.text = "Nice shot!";
            barrierObject.SetActive(false); 
        }
    }
}
