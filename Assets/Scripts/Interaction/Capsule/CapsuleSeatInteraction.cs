using Assets.Scripts.Movement;
using UnityEngine;

namespace Assets.Scripts.Interaction.Capsule
{
    public class CapsuleSeatInteraction : AbstractInteraction
    {
        public delegate void TakeCapsuleSeat();
        public static event TakeCapsuleSeat OnTakeCapsuleSeat;

        public override string Name
        {
            get { return "Platz nehmen"; }
        }

        public override void Interact(GameObject interacter)
        {
            if (interacter.tag == "Player")
            {
                Interactable = false;
                interacter.transform.parent = transform.parent.parent;
                interacter.transform.position = this.transform.position;
                interacter.transform.forward = -this.transform.up;
                interacter.GetComponent<MoveController>().CanMove = false;
                if (OnTakeCapsuleSeat != null) OnTakeCapsuleSeat();
            }
        }
    }
}
