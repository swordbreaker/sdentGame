using UnityEngine;

namespace Assets.Scripts.Sound
{
    public class EndingMusicPlayer : MonoBehaviour
    {
        public static EndingMusicPlayer Instance { get; private set; }

        public void Awake ()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void FixedUpdate()
        {
            transform.position = Camera.main.transform.position;
        }
    }
}
