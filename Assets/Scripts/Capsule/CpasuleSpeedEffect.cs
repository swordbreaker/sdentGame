using System;
using System.Collections;
using Assets.Script.Helpers;
using Assets.Scripts.Movement;
using Assets.Scripts.Visuals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Capsule
{
    public class CpasuleSpeedEffect : MonoBehaviour
    {
        [SerializeField]
        private CapsuleEngine capsuleEngine;

        [SerializeField]
        private float maxBlurStrength = 10f;

        [SerializeField]
        private float maxBlurWidth = 0.1f;

        [SerializeField]
        private float maxShakeStrength = 1f;

        [SerializeField]
        private float maxColorStrength = 1f;

        [SerializeField]
        private float minVelocity = 500;

        [SerializeField]
        private float maxtVelocity = 1500;

        [SerializeField]
        private float minVelocityFadeOut = 1500;

        [SerializeField]
        private float maxtVelocityFadeOut = 2500;

        [SerializeField]
        private MoveController playerMoveController;

        private RadialBlur radialBlur;
        private Shake shake;
        private ColorOverlay colorOverlay;

        private LerpHelper<Quaternion> playerLockLerp;
        private LerpHelper<Quaternion> playerCameraLockLerp;
        private bool playerLockLerpGoalReached;
        private bool playerCameraLockLerpGoalReached;

        public void Start()
        {
            radialBlur = GetComponent<RadialBlur>();
            shake = GetComponent<Shake>();
            colorOverlay = GetComponent<ColorOverlay>();
        }

        public void FixedUpdate()
        {
            var velocity = capsuleEngine.Velocity.magnitude;
            if (velocity > minVelocity)
            {
                if (playerMoveController.enabled)
                {
                    playerLockLerp = new LerpHelper<Quaternion>(playerMoveController.transform.rotation, Quaternion.LookRotation(Vector3.right, -Vector3.up), 2);
                    playerCameraLockLerp = new LerpHelper<Quaternion>(Camera.main.transform.rotation, Quaternion.LookRotation(Vector3.right, -Vector3.up), 2);
                    playerMoveController.enabled = false;
                } 
                else
                {
                    if (!playerLockLerpGoalReached)
                    {
                        playerMoveController.transform.rotation = playerLockLerp.CurrentValue(out playerLockLerpGoalReached);
                    }
                    if (!playerCameraLockLerpGoalReached)
                    {
                        Camera.main.transform.rotation = playerCameraLockLerp.CurrentValue(out playerCameraLockLerpGoalReached);
                    }
                }
                var relativeVelocity = Math.Min(velocity, maxtVelocity) - minVelocity;
                var relativeMaxVelocity = maxtVelocity - minVelocity;
                var strength = relativeVelocity / relativeMaxVelocity;
                radialBlur.BlurStrength = maxBlurStrength * strength;
                radialBlur.BlurWidth = maxBlurWidth * strength;
                shake.ShakeStrength = maxShakeStrength * strength;
            }
            if (velocity > minVelocityFadeOut)
            {
                var relativeVelocity = Math.Min(velocity, maxtVelocityFadeOut) - minVelocityFadeOut;
                var relativeMaxVelocity = maxtVelocityFadeOut - minVelocityFadeOut;
                var strength = relativeVelocity / relativeMaxVelocity;
                colorOverlay.ColorStrength = maxColorStrength * strength;
            }
        }
    }
}
