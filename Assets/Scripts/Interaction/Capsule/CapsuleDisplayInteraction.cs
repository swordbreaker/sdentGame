using System;
using Assets.Scripts.Capsule;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Interaction.Capsule
{
    internal class DisplayState
    {
        public delegate void Interaction();

        private readonly Interaction ineraction;
        private readonly DisplayState nextState;
        private readonly string name;

        public DisplayState(string name, Interaction ineraction, DisplayState nextState)
        {
            this.ineraction = ineraction;
            this.nextState = nextState;
            this.name = name;
        }

        public string Name()
        {
            return name;
        }

        public DisplayState Interact()
        {
            ineraction();
            return nextState;
        }
    }

    public class CapsuleDisplayInteraction : IInteraction
    {
        public delegate void Undock(CapsuleDisplayInteraction sender);
        public static event Undock OnUndock;

        public delegate void BoosterIgnition(CapsuleDisplayInteraction sender);
        public static event BoosterIgnition OnBoosterIgnition;

        private readonly DisplayState idleState;
        private readonly DisplayState undockState;
        private readonly DisplayState boosterIgnitionState;

        private DisplayState state;

        public CapsuleDisplayInteraction()
        {
            var self = this;
            idleState = new DisplayState("", () => { }, idleState);

            boosterIgnitionState = new DisplayState("Antrieb starten", () =>
            {
                Interactable = false;
                if (OnBoosterIgnition != null) OnBoosterIgnition(self);
            }, idleState);

            undockState = new DisplayState("Kapsel abdocken", () =>
            {
                Interactable = false;
                if (OnUndock != null) OnUndock(self);
            }, boosterIgnitionState);

            state = undockState;

            Interactable = false;
            CapsuleSeatInteraction.OnTakeCapsuleSeat += () => Interactable = true;
        }

        public override string Name
        {
            get { return state.Name(); }
        }

        public override void Interact(GameObject interacter)
        {
            state = state.Interact();
        }
    }
}
