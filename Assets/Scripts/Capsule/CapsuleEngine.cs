using System;
using System.Collections;
using Assets.Scripts.Visuals;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Capsule
{
    public class CapsuleEngine : MonoBehaviour
    {
        private GameObject player;
        private RadialBlur playerRadialBlur;
        private AudioSource engineAudio;
        private AudioSource playerAudio;
        [SerializeField] private AudioClip outroAudioClip;

        public void Start()
        {
            engineAudio = GetComponent<AudioSource>();
            engineAudio.mute = true;
            engineAudio.volume = 0;
            player = GameObject.FindGameObjectWithTag("Player");
            playerAudio = player.GetComponent<AudioSource>();
            playerRadialBlur = player.GetComponentInChildren<RadialBlur>();
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
            transform.DOMoveZ(100000, 360);
            engineAudio.mute = false;
            this.StartCoroutine(_invoke(3));

        }


        private IEnumerator _invoke(float delay)
        {
            yield return new WaitForSeconds(1);

            playerAudio.volume = 0.0f;
            playerAudio.PlayOneShot(outroAudioClip, 1);

            while (playerAudio.volume < 1)
            {
                playerAudio.volume += 0.01f;
                playerRadialBlur.BlurStrength += 0.05f;
                playerRadialBlur.BlurWidth += 0.02f;
                yield return new WaitForSeconds(0.1f);
            }

            yield return null;
        }
    }
}
