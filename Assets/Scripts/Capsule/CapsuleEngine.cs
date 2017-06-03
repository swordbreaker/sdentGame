using System;
using System.Collections;
using Assets.Scripts.Interaction.Capsule;
using Assets.Scripts.Visuals;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Capsule
{
    public class CapsuleEngine : MonoBehaviour
    {
        [SerializeField]
        private AudioSource engineAudio;

        private bool running;

        public Vector3 Velocity { get; set; }

        [SerializeField]
        private Vector3 acceleration = new Vector3(0, 0, 5);

        public Vector3 Acceleration
        {
            get { return acceleration; }
            set { acceleration = value; }
        }
        
        public CapsuleEngine()
        {
            Velocity = Vector3.zero;
        }

        public void Start()
        {
            engineAudio.mute = true;
            BoostInteraction.OnBoosterIgnition += StartEngine;
        }

        public void FixedUpdate()
        {
            if (transform.position.z > 10000)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 500);
            }
            if (running)
            {
                Velocity += Acceleration * Time.deltaTime;
            }
            transform.position += Velocity * Time.deltaTime;
        }

        public void StartEngine()
        {
            StartCoroutine(StartBoosterSound());
            running = true;
        }

        public void StopEngine()
        {
            StartCoroutine(StopBoosterSound());
            running = false;
        }

        private IEnumerator StartBoosterSound(float fadeDuration = 1.5f, float increaseInterval = 0.1f)
        {
            yield return null;
            engineAudio.volume = 0;
            engineAudio.mute = false;
            engineAudio.Play();
            var step = increaseInterval / fadeDuration;
            while (engineAudio.volume < 1 - step)
            {
                engineAudio.volume += step;
                yield return new WaitForSeconds(increaseInterval);
            }
            engineAudio.volume = 1;
        }


        private IEnumerator StopBoosterSound(float fadeDuration = 0.5f, float decreaseInterval = 0.01f)
        {
            yield return null;
            var step = decreaseInterval / fadeDuration;
            while (engineAudio.volume > 0 + step)
            {
                engineAudio.volume -= step;
                yield return new WaitForSeconds(decreaseInterval);
            }
            engineAudio.volume = 0;
            engineAudio.mute = true;
        }
    }
}
