using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private UnityEvent onPlay = new UnityEvent();
    private bool isPlaying = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void StartGame() {
        onPlay.Invoke();
        isPlaying = true;
    }
}
