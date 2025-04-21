using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game
{
    public class LevelMenu : MonoBehaviour
    {
        public Button[] levelButtons; 

        void Start()
        {
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

            for (int i = 0; i < levelButtons.Length; i++)
            {
                levelButtons[i].interactable = (i < unlockedLevel);
            }
        }

        public void OpenLevel(int levelId)
        {
            string levelName = "Level" + levelId;
            SceneManager.LoadScene(levelName);
        }
    }
}
