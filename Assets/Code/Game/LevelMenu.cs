using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Game
{


    public class LevelMenu : MonoBehaviour
    {
        public void OpenLevel(int levelId)
        {
            string levelName = "Level" + levelId;
            SceneManager.LoadScene(levelName);
        }
    }
}
