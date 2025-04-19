using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CutsceneManager1 : MonoBehaviour
{
    public TextMeshProUGUI cutsceneText;
    public CanvasGroup fadeGroup;
    public float textDelay = 2f;
    public string[] cutsceneLines;
    public string nextSceneName = "Level1";

    public AudioSource audioSource;
    public AudioClip beepSound;

    void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        for (int i = 0; i < cutsceneLines.Length; i++)
        {
            cutsceneText.text = "";
            yield return StartCoroutine(TypeText(cutsceneLines[i]));
            yield return new WaitForSeconds(textDelay);
        }

        SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator TypeText(string line)
    {
        foreach (char c in line)
        {
            cutsceneText.text += c;
            if (!char.IsWhiteSpace(c) && beepSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(beepSound);
            }
            yield return new WaitForSeconds(0.03f);
        }
    }

    public void SkipCutscene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
