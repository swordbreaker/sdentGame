using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts.Interaction.Capsule
{
    public class CapsuleSeatInteraction : IInteraction
    {
        public override string Name
        {
            get { return "Platz nehmen"; }
        }

        public override void Interact(GameObject interacter)
        {
            if (interacter.tag == "Player")
            {
                interacter.transform.position = this.transform.position;
                interacter.transform.forward = -this.transform.up;
                interacter.GetComponent<MoveController>().CanMove = false;
            }
        }
    }
}
