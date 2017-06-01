using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Interaction.Capsule;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Capsule
{
    public class CapsuleEndingTransition : MonoBehaviour
    {
        [SerializeField]
        private AudioSource endingAudio;

        [SerializeField]
        private ParticleSystem starParticles;

        public void Start()
        {
            CapsuleDisplayInteraction.OnUndock += interaction =>
            {
                interaction.Interactable = true;
            };
            CapsuleDisplayInteraction.OnBoosterIgnition += interaction =>
            {
                StartCoroutine(FadeIn(endingAudio, 5, 0.1f, 18)); //12
                StartCoroutine(FadeOutSounds(5, 0.1f, 22));
                StartCoroutine(SwitchScene("CapsuleEnding", 27));
                starParticles.Play();
            };
        }

        private IEnumerator FadeOutSounds(float fadeDuration = 1.5f, float increaseInterval = 0.1f, float delay = 0)
        {
            var audios = FindObjectsOfType(typeof(AudioSource)).Where(a => a != endingAudio).Cast<AudioSource>();
            yield return new WaitForSeconds(delay);
            foreach (var audioSource in audios)
            {
                StartCoroutine(FadeOut(audioSource, fadeDuration, increaseInterval));
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

        private IEnumerator SwitchScene(string sceneName, float delay, LoadSceneMode mode = LoadSceneMode.Single)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(sceneName, mode);
        }

    }
}
