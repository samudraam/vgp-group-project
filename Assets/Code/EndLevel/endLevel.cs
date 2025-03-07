using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class endLevel : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player finished the level!");
            //will load next level when it is made
            SceneManager.LoadScene(0);
        }
    }
    
}
