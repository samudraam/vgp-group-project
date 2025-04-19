using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Area3 : MonoBehaviour
{
    public GameObject barrierObject;
    public TextMeshProUGUI tutorialText;
    [TextArea]
    public string message = "Collect the 2 coins to continue.";

    public int requiredCoins = 2;

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

        if (PlayerCode.playerController.numberOfCoins >= requiredCoins)
        {
            gateOpened = true;
            tutorialText.text = "Great job!";
            barrierObject.SetActive(false);
        }
    }
}
