using Assets.Scripts.Capsule;
using UnityEngine;

namespace Assets.Scripts.Interaction.Capsule
{
    public class CapsuleDisplayInteraction : IInteraction
    {
        public GameObject SpaceShip;
        private CapsuleEngine engine;

        public void Start()
        {
            engine = SpaceShip.GetComponent<CapsuleEngine>();
        }

        public override string Name
        {
            get { return "Kapsel abdocken"; }
        }

        public override void Interact(GameObject interacter)
        {
            engine.StartEngine();
        }
    }
}
