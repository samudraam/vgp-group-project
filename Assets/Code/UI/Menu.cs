using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Menu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadSceneAsync(1);

        }

        public void QuitGame()
        {
            Application.Quit();

        }
    }
}
