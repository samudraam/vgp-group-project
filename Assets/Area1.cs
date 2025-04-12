using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Area1 : MonoBehaviour
{
    public GameObject barrierObject; // drag the gate/wall here
    public TextMeshProUGUI tutorialText;
    public string message = "Use A/D or Arrow Keys to move. Press W or Up to jump.";

    private bool playerInside = false;
    private bool hasMoved = false;
    private bool hasJumped = false;
    private bool gateOpened = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered trigger with: " + other.name);

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

        if (!hasMoved && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
        {
            hasMoved = true;
            Debug.Log("Player moved!");

        }

        if (!hasJumped && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
            hasJumped = true;

        if (hasMoved && hasJumped)
        {
            barrierObject.SetActive(false);
            tutorialText.text = "Nice!";
            gateOpened = true;
        }
    }
}
