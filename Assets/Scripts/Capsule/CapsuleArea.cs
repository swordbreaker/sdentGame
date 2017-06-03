using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Capsule
{
    public class CapsuleArea : MonoBehaviour
    {
        [SerializeField]
        private AudioSource[] mainAudioSources;

        public void Start ()
        {
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && mainAudioSources != null)
            {
                foreach (var audioSource in mainAudioSources)
                {
                    StartCoroutine(FadeOut(audioSource));
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player" && mainAudioSources != null)
            {
                foreach (var audioSource in mainAudioSources)
                {
                    StartCoroutine(FadeIn(audioSource));
                }
            }
        }

        private IEnumerator FadeIn(AudioSource audio, float fadeDuration = 1.5f, float increaseInterval = 0.1f, float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            audio.volume = 0;
            audio.mute = false;
            audio.Play();
            var step = increaseInterval / fadeDuration;
            while (audio.volume < 1 - step)
            {
                audio.volume += step;
                yield return new WaitForSeconds(increaseInterval);
            }
            audio.volume = 1;
        }

        private IEnumerator FadeOut(AudioSource audio, float fadeDuration = 1.5f, float increaseInterval = 0.1f, float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            var step = increaseInterval / fadeDuration;
            while (audio.volume > 0 + step)
            {
                audio.volume -= step;
                yield return new WaitForSeconds(increaseInterval);
            }
            audio.volume = 0;
            audio.mute = true;
        }
    }
}
