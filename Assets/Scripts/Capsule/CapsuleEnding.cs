using UnityEngine;

namespace Assets.Scripts.Capsule
{
    public class CapsuleEnding : MonoBehaviour
    {
        [SerializeField] private CapsuleEngine engine;

        [SerializeField] private Vector3 capsuleVelocity = new Vector3(0, 0, 50f);

        public void Start ()
        {
            engine.Velocity = capsuleVelocity;
        }
    }
}
