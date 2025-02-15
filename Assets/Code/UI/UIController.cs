using UnityEngine;

namespace Code.UI
{
    public class UIController : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private GameObject startMenuUI;
        private MenuController mc;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void PlaybuttonHandler()
        {
            mc.StartGame();
            startMenuUI.SetActive(false);
        }
    }
}
