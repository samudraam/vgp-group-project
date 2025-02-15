using UnityEngine;
using UnityEngine.Events;

namespace Code.UI
{
    public class MenuController : MonoBehaviour
    {
        // Start is called before the first frame update
        public UnityEvent onPlay = new UnityEvent();
        public bool isPlaying = false;
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void StartGame()
        {
            onPlay.Invoke();
            isPlaying = true;
        }
    }
}
