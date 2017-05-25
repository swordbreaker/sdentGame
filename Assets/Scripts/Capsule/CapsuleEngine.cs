using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Capsule
{
    public class CapsuleEngine : MonoBehaviour
    {

        private AudioSource engineAudio;

        public void Start()
        {
            engineAudio = GetComponent<AudioSource>();
            engineAudio.mute = true;
            engineAudio.volume = 0;
        }

        public void FixedUpdate()
        {
            if (engineAudio.mute == false && engineAudio.volume < 1)
            {
                engineAudio.volume += 0.2f * Time.deltaTime;
            }
            else if (engineAudio.mute == false && engineAudio.volume > 0)
            {
                engineAudio.volume -= 0.2f * Time.deltaTime;
            }
        }

        public void StartEngine()
        {
            transform.DOMoveZ(1000, 60);
            engineAudio.mute = false;
        }
    }
}
