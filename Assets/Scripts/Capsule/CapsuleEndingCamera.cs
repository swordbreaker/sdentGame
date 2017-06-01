using System.Collections;
using Assets.Scripts.Visuals;
using UnityEngine;

namespace Assets.Scripts.Capsule
{
    public class CapsuleEndingCamera : MonoBehaviour
    {
        [SerializeField] private float fadeInDuration = 5f;

        [SerializeField] private ColorOverlay colorOverlay;

        [SerializeField] private Transform target;

        public void Start ()
        {
            StartCoroutine(FadeOutColorOverlay(fadeInDuration));
        }
	
        public void FixedUpdate ()
        {
            transform.forward = target.position - transform.position;
        }

        private IEnumerator FadeOutColorOverlay(float fadeDuration, float increaseInterval = 0.1f, float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            var step = increaseInterval / fadeDuration;
            while (colorOverlay.ColorStrength > 0 + step)
            {
                colorOverlay.ColorStrength -= step;
                yield return new WaitForSeconds(increaseInterval);
            }
            colorOverlay.ColorStrength = 0;
        }
    }
}
